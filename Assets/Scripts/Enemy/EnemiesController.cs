using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public GameObject playerObj;
    public int nc = 1, nca = 1, nca2 = 0, nca3 = 0;
    public static Vector3 pos;
    public int animStateEnemy;
    public int animStatePublic;
    private Vector3 startPosition;
    public List<Vector3> positionList;
    public int CombatPower;
    public int Life, giantLife, enormLife;
    private GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetChild(0).gameObject;
        text.transform.parent.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        pos = transform.position;
        animStateEnemy = 0;
        startPosition = transform.position;
        CombatPower = 1;
        giantLife = 20;
        enormLife = 100;
        text.GetComponent<UnityEngine.UI.Text>().text = CombatPower.ToString();
        Debug.Log("Calculating positions for enemies");
        calculatePositions();
        growArmy(LevelController.sections * LevelController.lvl);
    }

    //UPDATE//
    void Update()
    {
        pos = transform.position;
        if (Input.GetKeyDown(KeyCode.Q) && LevelController.godmode)
        {
            growArmy(20);
        }
        animStatePublic = animStateEnemy;
        CombatPower = nca + nca2 * 20 + nca3 * 100;
        Life = CombatPower - (20 - giantLife) - (100 - enormLife);
        if(Life > 0) text.GetComponent<UnityEngine.UI.Text>().text = Life.ToString();
        else text.GetComponent<UnityEngine.UI.Text>().text = "";
    }

    public void growArmy(int c)
    {
        nc += c;
        while (nca < nc)
        {
            if (nca + 1 == 20)
            {
                nca++;
                Evolve();
                nc -= 20;
                nca -= 20;
                nca2 += 1;
                if (nca2 == 5)
                {
                    Evolve2();
                    nca2 -= 5;
                    nca3 += 1;
                }
            }
            else
            {
                Debug.Log("voy a clonar un enemigo, nc = " + nc);
                if (!Clone()) nc -= (nc - nca);
                else nca++;
            }
        }
    }

    private void calculatePositions()
    {
        positionList.Add(startPosition);
        for (int i = 1; i < 5; i++)
        {
            positionList.AddRange(calculatePositionsAux(1.5f * i, 25 / (5 - i)));
        }
    }

    private void setPositions()
    {
        int it = 0;
        bool first = true;
        foreach (Transform child in transform.GetComponentInChildren<Transform>())
        {
            if (first)  first = false;
            else {
                child.position = positionList[it];
                it++;
            }
        }
    }

    private List<Vector3> calculatePositionsAux(float distance, int count)
    {
        List<Vector3> auxposlist = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            float angle = i * (360f / count);
            Vector3 dir = Quaternion.Euler(0, angle, 0) * (new Vector3(1, 0));
            Vector3 position = startPosition + dir * (distance + Random.value);
            auxposlist.Add(position);
        }
        return auxposlist;
    }

    private bool Clone()
    {
        Debug.Log("clonando enemigo " + (nca + nca2 + nca3) + " " + (positionList.Count - 1));
        if ((nca + nca2 + nca3) > (positionList.Count - 1))
        {
            Debug.Log("no more clones can be added");
            return false;
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(playerObj, positionList[nca + nca2 + nca3], playerObj.transform.rotation);
            obj.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log(nca);
            obj.transform.parent = transform;
            return true;
        }
    }

    private void Evolve()
    {
        int ChildsToDestroy = transform.childCount - nca;
        for (int i = transform.childCount - 1; i > ChildsToDestroy; i--)
        {
            Debug.Log("Destroying copy " + i);
            GameObject child = transform.GetChild(i).gameObject;
            child.transform.parent = null;
            Destroy(child);
        }
        GameObject EvolvingChild = (GameObject)Instantiate(playerObj, positionList[nca + nca2 + nca3], playerObj.transform.rotation);
        EvolvingChild.transform.parent = transform;
        EvolvingChild.GetComponent<OneEnemy>().big = 1;
        EvolvingChild.transform.localScale *= 1.5f;
        setPositions();
    }

    private void Evolve2()
    {
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<OneEnemy>().big == 1)
            {
                child.transform.parent = null;
                Destroy(child);
            }
        }
        GameObject EvolvingChild = (GameObject)Instantiate(playerObj, positionList[nca + nca2 + nca3], playerObj.transform.rotation);
        EvolvingChild.transform.parent = transform;
        EvolvingChild.GetComponent<OneEnemy>().big = 2;
        EvolvingChild.transform.localScale *= 2f;
        setPositions();
    }

    public void getDamage()
    {
        if (nca != 0)
        {
            GameObject g = transform.GetChild(transform.childCount - 1).gameObject;
            g.transform.parent = null;
            Destroy(g);
            nc -= 1;
            nca -= 1;
        }
        else if (nca2 != 0)
        {
            giantLife -= 1;
            if(giantLife == 0)
            {
                GameObject g = transform.GetChild(transform.childCount - 1).gameObject;
                g.transform.parent = null;
                Destroy(g);
                nca2 -= 1;
                giantLife = 20;
            }
        }
        else if (nca3 != 0)
        {
            enormLife -= 1;
            if(enormLife == 0)
            {
                GameObject g = transform.GetChild(transform.childCount - 1).gameObject;
                g.transform.parent = null;
                Destroy(g);
                nca3 -= 1;
                enormLife = 100;
            }
        }
    }
}
