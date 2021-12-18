using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrow;
    public float speed = 30.0f;
    float shootInterval = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootInterval -= Time.deltaTime;
        if (shootInterval <= 0.0f)
        {
            shootInterval = 1.5f;
            GameObject obj = Instantiate(arrow, transform.position + new Vector3(1.0f, 0.0f, 0.0f),
                arrow.transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0.0f, 0.0f);
        }
        
    }
}
