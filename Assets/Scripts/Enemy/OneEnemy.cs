using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEnemy : MonoBehaviour
{
    private Animator anim;
    public float animState;
    public int big = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        animState = anim.GetFloat("Animation");
    }

    //UPDATE//
    void Update()
    {
        if (anim.GetFloat("Animation") != transform.parent.GetComponent<EnemiesController>().animStateEnemy)
        {
            anim.SetFloat("Animation", transform.parent.GetComponent<EnemiesController>().animStateEnemy);
        }
    }
}
