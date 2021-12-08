using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public List<GameObject> prefabs;
    public static int ActualLevel;
    public int FloorSizeZ;
    private int sections;
    // Start is called before the first frame update
    void Start()
    {
        ActualLevel = 1;
        FloorSizeZ = 0;
        sections = 0;
        loadLevel1();
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log(CharsController.pos.z);
        if (CharsController.pos.z > (sections * 28) - 5 * 28)
        {
         //   Debug.Log("Instanciando");
            GameObject nf = (GameObject)Instantiate(prefabs[0], new Vector3(0, 0, 28 * sections), prefabs[0].transform.rotation);
            nf.transform.parent = transform;
            sections++;
            
        }
    }

    void loadLevel1()
    {
        for(int i = 0; i<3; i++)
        {
            GameObject a = (GameObject)Instantiate(prefabs[0], new Vector3(0, 0, 28 * sections), prefabs[0].transform.rotation);
            a.transform.parent = transform;
            sections++;
        }
        GameObject b = (GameObject)Instantiate(prefabs[1], new Vector3(0, 0, 28 * sections), prefabs[0].transform.rotation);
        b.transform.parent = transform;
        sections++;
    }
}
