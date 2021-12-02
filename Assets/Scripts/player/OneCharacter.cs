using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneCharacter : MonoBehaviour
{
    private Animator anim;
    public float animState;

    void Start()
    {
        anim = GetComponent<Animator>();
        animState = anim.GetFloat("Animation");
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetFloat("Animation") != CharactersController.animState)
        {
            anim.SetFloat("Animation", CharactersController.animState);
        }
    }
}
