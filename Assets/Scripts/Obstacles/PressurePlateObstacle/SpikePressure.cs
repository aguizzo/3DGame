using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePressure : MonoBehaviour
{
    public float speed = 8f;

    private Transform spikesTransform;
    public bool disabled = false;
    // Start is called before the first frame update
    void Start()
    {
        spikesTransform = transform.GetChild(0).gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            spikesTransform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
        }
        
    }

    public void DisableTrap()
    {
        disabled = true;
    }
}
