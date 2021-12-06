using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    float direction = 1.0f;
    public float speed = 20.0f;
    public float limitDown = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            transform.Rotate(0.0f, 0.0f, direction * speed * Time.deltaTime * Mathf.PI);
            if (direction == 1.0f && transform.eulerAngles.z > limitDown)
            {
                direction = -1.0f;
            }

            if (direction == -1.0f && transform.eulerAngles.z >= 355.0f)
            {
                direction = 1.0f;
            }
        }
    }
}
