using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlEnd : MonoBehaviour
{
    LevelController lvlCont;
    bool end = false;
    // Start is called before the first frame update
    void Start()
    {
       lvlCont = GameObject.FindObjectOfType<LevelController>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (lvlCont != null && !end)
        {
            end = true;
            lvlCont.NextLevel();     
        } 
        else
            Debug.Log("empty");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
