using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    float direction = 1.0f;
    float speed = 3.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, direction * speed * Time.deltaTime, 0.0f);
        if(direction == 1.0f && transform.position.y > 15.0f) 
        {
            direction = -1.0f;
        }

        if (direction == -1.0f && transform.position.y <= 5.0f)
        {
            direction = 1.0f;
        }
    }
}
