using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharsController : MonoBehaviour
{
    public GameObject playerObj;
    [SerializeField] private float lateralSpeed;
    private Animator anim;
    public int nc = 1, nca = 1, nca2 = 0;
    public static Vector3 pos;
    public static int animState;
    public int animStatePublic;
    private Vector3 startPosition;
    public List<Vector3> positionList;
    public int CombatPower;
    private bool start;
    private bool inBattle;
    private float battlePosZ;
    private int giantLife;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = transform.GetChild(0).gameObject;
        pos = playerObj.transform.position;
        startPosition = new Vector3(0,pos.y, 0);
        anim = GetComponentInChildren<Animator>();
        animState = 0;
        setPositions();
        CombatPower = 1;
        lateralSpeed = 0;
        start = true;
        inBattle = false;
        giantLife = 25;
    }

    //UPDATE//
    void Update()
    {
        pos = playerObj.transform.position;
        if (LevelController.inBattle)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), 10 * Time.deltaTime);
            if(Mathf.Round(pos.z) == Mathf.Round(battlePosZ)) animState = 2;
        }
        else
        {
            battlePosZ = pos.z + 10;
            
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && start == true)
            {
                animState = 1;
                lateralSpeed = 10f;
                start = false;
            }
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !inBattle)
            {
                moveLeft();
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !inBattle)
            {
                moveRight();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            growArmy(7);
        }
        animStatePublic = animState;
        CombatPower = nca + nca2 * 25;
    }

    private void moveLeft()
    {
        if (this.gameObject.transform.position.x > LevelController.leftSide)
        {
            transform.Translate(Vector3.left * Time.deltaTime * lateralSpeed, Space.World);
        }
    }

    private void moveRight()
    {
        if (this.gameObject.transform.position.x < LevelController.rightSide)
        {
            transform.Translate(Vector3.right * Time.deltaTime * lateralSpeed, Space.World);
        }
    }
    private void growArmy(int c)
    {
        nc+=c; //cambiar por numero del obstaculo
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
                if(!Clone()) nc -= (nc-nca);
                else nca++;
            }
        }
    }

    private void setPositions()
    {
        for (int i = 1; i < 5; i++)
        {
            positionList.AddRange(setPositionsAux(1.5f * i, 25/(5-i)));
        }
    }

    private List<Vector3> setPositionsAux(float distance, int count)
    {
        List<Vector3> auxposlist = new List<Vector3>();
        for(int i = 0; i<count; i++)
        {
            float angle = i * (360f / count);
            Vector3 dir = Quaternion.Euler(0, angle, 0) * (new Vector3(1, 0));
            Vector3 position = startPosition + dir * (distance + Random.value);
            auxposlist.Add(position);
        }
        return auxposlist;
    }

    private bool Clone() {
        if ((nca - 1 + nca2) > (positionList.Count - 1))
        {
            Debug.Log("no more clones can be added");
            return false;
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(playerObj, (new Vector3(transform.position.x, 0, transform.position.z)) + positionList[nca - 1 + nca2], playerObj.transform.rotation);
            obj.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log(nca);
            obj.transform.parent = transform;
            return true;
        }
    }

    private void Evolve()
    {
        int ChildsToDestroy = transform.childCount - nca;
        for (int i = transform.childCount - 1; i > ChildsToDestroy; i-- ) {
            Debug.Log("Destroying copy " + i);
            GameObject child = transform.GetChild(i).gameObject;
            child.transform.parent = null;
            Destroy(child);
        }
        GameObject EvolvingChild = transform.GetChild(transform.childCount - 1).gameObject;
        Debug.Log("evoluciona hijo numero: " + (transform.childCount));
        EvolvingChild.transform.localScale *= 1.5f;
    }

    public void destroyLastChild()
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
            if (giantLife != 0) giantLife -= 1;
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
    public void Move()
    {
        animState = 1;
    }
}
