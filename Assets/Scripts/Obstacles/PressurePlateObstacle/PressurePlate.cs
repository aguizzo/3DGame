using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool activated = false;
    public float speed;
    private Transform rockTransform;
    SpikePressure trapScript;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        rockTransform = transform.GetChild(1).gameObject.transform;
        trapScript = transform.parent.GetComponent<SpikePressure>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && rockTransform.position.y >= 1.5f)
        {
            rockTransform.Translate(0.0f, -speed * Time.deltaTime, 0.0f );
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (!activated)
        {
            FindObjectOfType<AudioManager>().Play("Pressure");
            activated = true;
            trapScript.DisableTrap();
        }
    }
}
