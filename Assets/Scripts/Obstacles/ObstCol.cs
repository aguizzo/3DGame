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
            Destroy(collision.collider.gameObject);
            Debug.Log("collision");
       // }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "littleswordfighter" || collision.gameObject.name == "littleswordfighter(Clone)") 
        {
            oneCharacter.Die();
            Destroy(collision.GetComponent<Collider>().gameObject);
            Debug.Log("collision");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}