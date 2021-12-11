using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<GameObject> prefabs;
    public static int ActualLevel;
    private int sections;
    private List<GameObject> Enemies;
    private int NextEnemyIndex;
    private GameObject actualEnemy;
    public static bool inBattle;
    private float startTime;
    private float time = 0f;
    private float interpolationPeriod = 0.5f;

    GameObject pv, units;

    public static float leftSide = -8f;
    public static float rightSide = 8f;

    private int[] level = { 0, 0, 0, 1, 0, 1, 2,3,4,5,6,7,8,9};

    // Start is called before the first frame update
    void Start()
    {
        pv = GameObject.Find("PlayerView");
        units = pv.transform.GetChild(0).gameObject;
        ActualLevel = 1;
        sections = 0;
        NextEnemyIndex = 0;
        inBattle = false;
        Enemies = new List<GameObject>();
        FindObjectOfType<AudioManager>().Play("LevelTheme");
        loadStartLevel1();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log(CharsController.pos.z);
        if (CharsController.pos.z > (sections * 28) - 5 * 28)
        {
            Debug.Log("numero de secciones: " + sections);
            Debug.Log("resta " + ((sections * 28) - 5 * 28));
            Debug.Log("Instanciando");
            GameObject a = (GameObject)Instantiate(prefabs[level[sections]], new Vector3(0, 0, 28 * sections), prefabs[level[sections]].transform.rotation);
            a.transform.parent = transform;
            if (level[sections] == 1) { Enemies.Add(a.transform.GetChild(3).gameObject); }
            sections++;
        }

        if (!inBattle && Enemies.Count != 0 && pv.GetComponentInChildren<Transform>().position.z > Enemies[NextEnemyIndex].transform.position.z - 10)
        {
            Debug.Log("inicio de la batalla");
            inBattle = true;
            actualEnemy = Enemies[NextEnemyIndex];
            startTime = Time.time;
            time = 0;
        }

        if (inBattle && time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;
            if (Enemies[NextEnemyIndex].transform.childCount != 0 && units.transform.childCount != 0)
            {
                Enemies[NextEnemyIndex].GetComponent<EnemiesController>().destroyLastChild();
                units.GetComponent<CharsController>().destroyLastChild();
            }
            else if (Enemies[NextEnemyIndex].transform.childCount == 0) {
                Debug.Log("destruyendo grupo de enemigos");
                GameObject en = Enemies[NextEnemyIndex];
                Enemies.Remove(en);
                en.transform.parent = null;
                Destroy(en);
                Debug.Log("final de la batalla");
                inBattle = false;
                pv.GetComponent<GroupMovement>().Move();
                units.GetComponent<CharsController>().Move();
            }
            else
            {
                Debug.Log("Derrota");
            }
        }
    }

    void loadStartLevel1()
    {
        for(int i = 0; i<5; i++)
        {
            GameObject a = (GameObject)Instantiate(prefabs[level[sections]], new Vector3(0, 0, 28 * sections), prefabs[level[sections]].transform.rotation);
            a.transform.parent = transform;
            Debug.Log("Created platform");
            if (level[sections] == 1) { Enemies.Add(a.transform.GetChild(3).gameObject); }
            sections++;
        }
    }
}
