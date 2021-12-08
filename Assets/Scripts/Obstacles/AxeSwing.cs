using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    float direction = 1.0f;
    public float speed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            transform.Rotate(0.0f, 0.0f, direction * speed * Time.deltaTime * Mathf.PI);
            if (direction == 1.0f && transform.eulerAngles.z >= 50 && transform.eulerAngles.z <= 60)
            {
                direction = -1.0f;
            }

            if (direction == -1.0f && transform.eulerAngles.z >= 300.0f && transform.eulerAngles.z <= 310.0f)
            {
                direction = 1.0f;
            }
        }
    }
}
