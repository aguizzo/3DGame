using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    public GameObject playerObj;
    [SerializeField] private float lateralSpeed = 10.0f;
    private Animator anim;
    private int nc = 0, nca = 0;
    public static Vector3 pos;
    public static int animState;
    public int animStatePublic;
    private Vector3 startPosition;
    public List<Vector3> positionList;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("littleswordfighter");
        pos = playerObj.transform.position;
        anim = GetComponentInChildren<Animator>();
        animState = 0;
        startPosition = playerObj.transform.position;
        setPositions();
    }

    // Update is called once per frame
    void Update()
    {
        pos = playerObj.transform.position;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animState = 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > Boundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * lateralSpeed, Space.World);
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < Boundary.rightSide)
            {
                transform.Translate(Vector3.right * Time.deltaTime * lateralSpeed, Space.World);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            nc++; //cambiar por numero del obstaculo
            while (nca < nc)
            {
                Clone();
                nca++;
            }
        }
        animStatePublic = animState;
    }

    private void setPositions()
    {
        for (int i = 1; i < 5; i++)
        {
            positionList.AddRange(setPositionsAux(1.2f * i, 25/(5-i)));
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

    private void Clone() {
        GameObject obj = (GameObject)Instantiate(playerObj, (new Vector3(0,0,playerObj.transform.position.z)) + positionList[nca], playerObj.transform.rotation);
        Debug.Log(nca);
        obj.transform.parent = transform;
    }


    private void OnBecameInvisible()
    {
        Destroy(this);
    }
}
