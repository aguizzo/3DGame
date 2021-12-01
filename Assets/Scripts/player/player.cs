using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject playerObj;
    [SerializeField] private float lateralSpeed = 10.0f;
    private Animator anim;
    private int nc = 0, nca = 0;
    public static Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = this.gameObject;
        pos = transform.position;
        anim = GetComponentInChildren<Animator>();
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Run();
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
    }

    private void Clone() {
        GameObject obj = (GameObject)Instantiate(playerObj, transform.position + new Vector3(1,0,0), playerObj.transform.rotation);
        obj.transform.parent = transform;
    }

    private void Idle() {
        anim.SetFloat("Animation",0);
    }
    private void Run()
    {
        anim.SetFloat("Animation", 1);
    }
    private void Attack()
    {
        anim.SetFloat("Animation", 2);
    }
    private void Win()
    {
        anim.SetFloat("Animation", 3);
    }
    private void Dead()
    {
        anim.SetFloat("Animation", 4);
    }

    private void OnBecameInvisible()
    {
        Destroy(this);
    }
}
