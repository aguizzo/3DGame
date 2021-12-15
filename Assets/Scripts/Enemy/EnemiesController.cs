using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public GameObject playerObj;
    public int nc = 1, nca = 1, nca2 = 0;
    public static Vector3 pos;
    public static int animStateEnemy;
    public int animStatePublic;
    private Vector3 startPosition;
    public List<Vector3> positionList;
    public int CombatPower;
    public int giantLife;
    private GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetChild(0).gameObject;
        text.transform.parent.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        pos = transform.position;
        animStateEnemy = 0;
        startPosition = transform.position;
        setPositions();
        CombatPower = 1;
        giantLife = 25;
        text.GetComponent<UnityEngine.UI.Text>().text = CombatPower.ToString();
    }

    //UPDATE//
    void Update()
    {
        pos = transform.position;
        if (Input.GetKeyDown(KeyCode.F))
        {
            growArmy(7);
        }
        animStatePublic = animStateEnemy;
        CombatPower = nca + nca2 * 25;

        if (LevelController.inBattle)
        {
            animStateEnemy = 2;
        }
        text.GetComponent<UnityEngine.UI.Text>().text = CombatPower.ToString();
    }

    private void growArmy(int c)
    {
        nc += c; //cambiar por numero del obstaculo
        while (nca < nc)
        {
            if (nca + 1 == 25)
            {
                Evolve();
                nc -= 25;
                nca -= 24;
                nca2 += 1;
            }
            else
            {
                if (!Clone()) nc -= (nc - nca);
                else nca++;
            }
        }
    }

    private void setPositions()
    {
        Debug.Log("setting positions for enemies");
        for (int i = 1; i < 5; i++)
        {
            positionList.AddRange(setPositionsAux(1.5f * i, 25 / (5 - i)));
        }
    }

    private List<Vector3> setPositionsAux(float distance, int count)
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
        if ((nca - 1 + nca2) > (positionList.Count - 1))
        {
            Debug.Log("no more clones can be added");
            return false;
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(playerObj, positionList[nca - 1 + nca2], playerObj.transform.rotation);
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
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<OneCharacter>().big == 1)
            {
                ChildsToDestroy--;
            }
            else
            {
                child.transform.parent = null;
                Destroy(child);
            }
        }
        GameObject EvolvingChild = transform.GetChild(transform.childCount - 1).gameObject;
        Debug.Log("evoluciona hijo numero: " + (transform.childCount));
        EvolvingChild.transform.localScale *= 1.5f;
    }

    public void getDamage()
    {
        if(nca != 0)
        {
            GameObject g = transform.GetChild(transform.childCount - 1).gameObject;
            Debug.Log("enemy child count: " + (transform.childCount - 1));
            g.transform.parent = null;
            Destroy(g);
            nc -= 1;
            nca -= 1;
        }
        else if(nca2 != 0)
        {
            if (giantLife != 0)
            {
                giantLife -= 1;
                Debug.Log("gigante pierde una vida, vida restante: " + giantLife);
            }
            else
            {
                GameObject g = transform.GetChild(transform.childCount - 1).gameObject;
                g.transform.parent = null;
                Destroy(g);
                nca2 -= 1;
                if (nca2 != 0) giantLife = 10;
            }
        }
    }
}
