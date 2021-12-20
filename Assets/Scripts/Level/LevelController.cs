using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<GameObject> prefabsM;
    public List<GameObject> prefabsF;
    public List<GameObject> prefabsT;
    public List<GameObject> prefabs;
    public static int sections;
    private int sectionCount;
    private List<GameObject> Enemies;
    private int NextEnemyIndex;
    public static GameObject actualEnemy;
    public static bool inBattle;
    private float startTime;
    public float time = 0f, endtimer = 0f, dietimer = 0f;
    private float interpolationPeriod = 1f;
    private float swordClash = 0.0f;
    private int swordSound = 1;
    private Vector3 initialPos;
    public bool final = false, end = false, lost = false, lostimer = false;
    public static bool godmode = false, inv = false;

    GameObject pv, units;

    public static float leftSide = -8f;
    public static float rightSide = 8f;
    private readonly int[] level1 = { 0, 0, 2, 9, 10, 6, 7, 0, 1, 0, 9, 8, 5, 2, 10, 9, 0, 1, 3 };
    private readonly int[] level2 = { 0, 0, 2, 5, 15, 2, 1, 8, 9, 10, 15, 5, 2, 1, 0, 8, 5, 0, 3 };
    private readonly int[] level3 = { 0, 0, 2, 4, 2, 1, 5, 8, 2, 7, 5, 4, 2, 1, 0, 8, 7, 0, 3 };
    private readonly int[] level4 = { 0, 0, 2, 5, 8, 2, 1, 0, 7, 6, 4, 4, 5, 8, 2, 1, 7, 0, 3 };
    private readonly int[] level5 = { 0, 0, 2, 2, 1, 4, 4, 2, 1, 0, 2, 8, 9, 5, 7, 2, 4, 2, 1, 0, 3 };
    private int[] level;
    public static int lvl;

    // Start is called before the first frame update
    void Start()
    { 
        lvl = 1;
        level = level1;
        pv = GameObject.Find("PlayerView");
        units = pv.transform.GetChild(0).GetChild(0).gameObject;
        initialPos = pv.transform.position;
        sections = 0;
        sectionCount = 0;
        NextEnemyIndex = 0;
        inBattle = false;
        Enemies = new List<GameObject>();
        prefabs = prefabsM;
        loadStartLevel();
        PoppingMenu.start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PoppingMenu.gamePaused)
        {
            time += Time.deltaTime;
            if (end) endtimer += Time.deltaTime;
            if (lostimer) dietimer += Time.deltaTime;
            //Debug.Log(CharsController.pos.z);
            if (!final && CharsController.pos.z > (sections * 28) - 5 * 28)
            {
                GameObject a = (GameObject)Instantiate(prefabs[level[sections]], new Vector3(0, 0, 28 * sections), prefabs[level[sections]].transform.rotation);
                a.transform.SetParent(transform);
                if (level[sections] == 1)
                {
                    Enemies.Add(a.transform.GetChild(3).gameObject);
                }
                else if (level[sections] == 3) final = true;
                sections++;
                sectionCount++;
            }

            if (sectionCount == 8)
            {
                GameObject d = transform.GetChild(0).gameObject;
                d.transform.parent = null;
                Destroy(d);
                --sectionCount;
            }

            if (!inBattle && !lost && Enemies.Count != 0 && pv.GetComponentInChildren<Transform>().position.z > Enemies[NextEnemyIndex].transform.position.z - 10)
            {
                inBattle = true;
                actualEnemy = Enemies[NextEnemyIndex];
                interpolationPeriod = 5f / actualEnemy.transform.GetComponent<EnemiesController>().CombatPower;
                time = 0;
            }

            if (inBattle && time >= interpolationPeriod && CharsController.animState == 2)
            {
                time = 0f;
                if (swordClash <= 0.0f)
                {
                    if (swordSound == 1)
                        {
                            FindObjectOfType<AudioManager>().Play("SwordClash");
                            swordSound++;
                        }
                    else 
                        {
                            FindObjectOfType<AudioManager>().Play("SwordClash2");
                            swordSound = 1;
                        }
                        
                    swordClash = 0.8f;
                }
                if (actualEnemy.transform.childCount != 1 && units.transform.childCount != 0)
                {
                    actualEnemy.GetComponent<EnemiesController>().getDamage();
                    if (!inv) units.GetComponent<CharsController>().getDamage();
                }
                else if (actualEnemy.transform.childCount == 1 && units.transform.childCount != 0)
                {
                    Enemies.Remove(actualEnemy);
                    actualEnemy.transform.SetParent(null);
                    Destroy(actualEnemy);
                    inBattle = false;
                    pv.GetComponent<GroupMovement>().Move();
                    units.GetComponent<CharsController>().Move();
                }
                else
                {
                    inBattle = false;
                    actualEnemy.GetComponent<EnemiesController>().animStateEnemy = 4;
                }
            }

            if (CharsController.Life == 0 && !lost && !GroupMovement.start)
            {
                lost = true;
                lostimer = true;
                GroupMovement.speed = 0f;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                godmode = !godmode;
                inv = false;
            }

            if (godmode && Input.GetKeyDown(KeyCode.I))
            {
                inv = !inv;
            }

            if (endtimer >= 5f)
            {
                SetLevel(0);
                end = false;
                endtimer = 0f;
            }

            if (dietimer >= 2f)
            {
                dietimer = 0f;
                lostimer = false;
                PoppingMenu.DeathMenu.gameObject.SetActive(true);
            }
        }
        swordClash -= Time.deltaTime;
    }

    void loadStartLevel()
    {
        Debug.Log("cargando nivel");
        FindObjectOfType<AudioManager>().Play("LevelTheme");
        for(int i = 0; i<5; i++)
        {
            GameObject a = (GameObject)Instantiate(prefabs[level[sections]], new Vector3(0, 0, 28 * sections), prefabs[level[sections]].transform.rotation);
            a.transform.parent = transform;
            if (level[sections] == 1)
            {
                Enemies.Add(a.transform.GetChild(3).gameObject);
            }
            else if (level[sections] == 3) final = true;
            sections++;
            sectionCount++;
        }
    }

    public void SetLevel(int l)
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t != transform)
            {
                t.SetParent(null);
                Destroy(t.gameObject);
            }
        }
        if (l == 0) lvl++;
        else lvl = l;
        Debug.Log("estas en el nivel " + lvl);
        switch (lvl)
        {
            case 1:
                level = level1;
                prefabs = prefabsM;
                break;
            case 2:
                level = level2;
                prefabs = prefabsM;
                break;
            case 3:
                level = level3;
                prefabs = prefabsF;
                break;
            case 4:
                level = level4;
                prefabs = prefabsF;
                break;
            case 5:
                level = level5;
                prefabs = prefabsT;
                break;
            case 6:
                FindObjectOfType<AudioManager>().Play("MenuTheme");
                SceneManager.LoadScene("Credits");
                return;
        }
        units.transform.GetComponent<CharsController>().reset();
        pv.transform.position = initialPos;
        pv.GetComponent<GroupMovement>().reset();
        units.transform.parent.GetComponent<CharsMovement>().reset();
        CharsController.animState = 0;
        sections = 0;
        NextEnemyIndex = 0;
        inBattle = false;
        Enemies = new List<GameObject>();
        loadStartLevel();
        sectionCount = 0;
        final = false;
        lost = false;
        lostimer = false;
        PoppingMenu.start = true;
    }

    public void EndLevel()
    {
        FindObjectOfType<AudioManager>().StopPlaying("LevelTheme");
        FindObjectOfType<AudioManager>().Play("Victory");
        GroupMovement.speed = 0f;
        CharsController.animState = 4;
        end = true;
    }
}
