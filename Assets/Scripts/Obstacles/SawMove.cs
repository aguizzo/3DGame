using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMove : MonoBehaviour
{
    // Start is called before the first frame update
    float direction = 1.0f;
    public float speed = 3.0f;
    public float limLeft = -10.0f;
    public float limRight = 15.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, 0.0f, direction * speed * Time.deltaTime);
        if (direction == 1.0f && transform.position.x > limLeft)
        {
            direction = -1.0f;
        }

        if (direction == -1.0f && transform.position.x < limRight)
        {
            direction = 1.0f;
        }
    }
}
