using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstCol : MonoBehaviour
{
    // Start is called before the first frame update

    OneCharacter oneCharacter;


    void Start()
    {
        oneCharacter = GameObject.FindObjectOfType<OneCharacter>();
    }

    void OnCollisionEnter(Collision collision)
    {
       // if (collision.gameObject.name == "littleswordfighter")
      //  {
        oneCharacter.Die();
        GameObject a = collision.collider.gameObject;
        a.transform.parent = null;
        Destroy(a);
        Debug.Log("collision");
       // }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Player(Clone)") 
        {
            oneCharacter.Die();
            GameObject a = collision.GetComponent<Collider>().gameObject;
            a.transform.parent.GetComponent<CharsController>().ChildDeath(a);
            a.transform.parent = null;
            Destroy(a);
            Debug.Log("collision");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}