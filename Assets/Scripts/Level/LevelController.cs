using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<GameObject> prefabs;
    private int sections, sectionCount;
    private List<GameObject> Enemies;
    private int NextEnemyIndex;
    private GameObject actualEnemy;
    public static bool inBattle;
    private float startTime;
    private float time = 0f;
    private float interpolationPeriod = 0.2f;
    private float swordClash = 0.0f;
    private int swordSound = 1;
    private Vector3 initialPos;
    private bool final = false, start = true;

    GameObject pv, units;

    public static float leftSide = -8f;
    public static float rightSide = 8f;
    private int[] level1 = { 0, 1, 0, 1, 0, 10, 2, 0, 10, 5, 6, 7, 8, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11 };
    private int[] level2 = { 0, 0, 0, 2, 2, 3, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] level3 = { 0, 0, 0, 10, 10, 10, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] level4 = { 0, 0, 0, 10, 10, 10, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] level5 = { 0, 0, 0, 10, 10, 10, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] level;
    private int lvl;

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
        loadStartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log(CharsController.pos.z);
        if (!final && CharsController.pos.z > (sections * 28) - 5 * 28)
        {
            GameObject a = (GameObject)Instantiate(prefabs[level[sections]], new Vector3(0, 0, 28 * sections), prefabs[level[sections]].transform.rotation);
            Debug.Log("nueva seccion");
            a.transform.parent = transform;
            if (level[sections] == 1) Enemies.Add(a.transform.GetChild(3).gameObject);
            else if (level[sections] == 11) final = true;
            sections++;
            sectionCount++;
            if(sectionCount == 8)
            {
                GameObject d = transform.GetChild(0).gameObject;
                d.transform.parent = null;
                Destroy(d);
                --sectionCount;
            }
        }

        if (!inBattle && Enemies.Count != 0 && pv.GetComponentInChildren<Transform>().position.z > Enemies[NextEnemyIndex].transform.position.z - 10)
        {
            inBattle = true;
            actualEnemy = Enemies[NextEnemyIndex];
            startTime = Time.time;
            time = 0;
        }

        if (inBattle && time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;
            if (Enemies[NextEnemyIndex].transform.childCount != 1 && units.transform.childCount != 0)
            {
                Enemies[NextEnemyIndex].GetComponent<EnemiesController>().getDamage();
                units.GetComponent<CharsController>().getDamage();
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
            }
            else if (Enemies[NextEnemyIndex].transform.childCount == 1) {
                GameObject en = Enemies[NextEnemyIndex];
                Enemies.Remove(en);
                en.transform.parent = null;
                Destroy(en);
                inBattle = false;
                pv.GetComponent<GroupMovement>().Move();
                units.GetComponent<CharsController>().Move();
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
            if (level[sections] == 1) { Enemies.Add(a.transform.GetChild(3).gameObject); }
            sections++;
            sectionCount++;
        }
    }

    public void NextLevel()
    {
        foreach (Transform t in transform.GetComponentInChildren<Transform>())
        {
            
            
            t.transform.parent = null;
           Destroy(t.gameObject);
        }
        lvl++;
        Debug.Log("estas en el nivel " + lvl);
        switch (lvl)
        {
            case 2:
                level = level2;
                break;
            case 3:
                level = level3;
                break;
            case 4:
                level = level4;
                break;
            case 5:
                level = level5;
                break;
        }
        pv.transform.position = initialPos;
        sections = 0;
        NextEnemyIndex = 0;
        inBattle = false;
        Enemies = new List<GameObject>();
        sectionCount = 0;
        final = false;
        loadStartLevel();
    }
}
