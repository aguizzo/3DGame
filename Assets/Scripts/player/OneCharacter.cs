using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneCharacter : MonoBehaviour
{
    private Animator anim;
    public float animState;
    private bool alive = true;
    public bool big = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        animState = anim.GetFloat("Animation");
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
            return;
        else 
        {
            if (anim.GetFloat("Animation") != CharsController.animState)
            {
                anim.SetFloat("Animation", CharsController.animState);
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        //Debug.Log("hola");
       // Die();
    }

    public void Die()
    {
        alive = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
