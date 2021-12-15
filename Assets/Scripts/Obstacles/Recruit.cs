using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruit : MonoBehaviour
{
    private int[] adds = {5,10,20,30};
    private int[] mults = {2,3,4};
    private int bon;
    private float r;
    private bool am;
    private GameObject text;
    private bool used;
    // Start is called before the first frame update
    void Start()
    {
        used = false;
        text = transform.GetChild(0).GetChild(0).gameObject;
        text.transform.parent.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        r = Random.value;
        if (r > 0.6)
        {
            bon = mults[Mathf.RoundToInt(Random.Range(0, 3))];
            am = true;
            text.GetComponent<UnityEngine.UI.Text>().text = 'x' + bon.ToString();
        }
        else
        {
            bon = adds[Mathf.RoundToInt(Random.Range(0, 4))];
            am = false;
            text.GetComponent<UnityEngine.UI.Text>().text = '+' + bon.ToString();
        }
        Debug.Log("Created recruits: " + "multiplication = " + am + " | Value = "+bon);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            Debug.Log("Trigger");
            other.transform.parent.GetComponent<CharsController>().growArmy(bon, am);
            transform.parent.GetComponent<RecruitDestroyer>().destroyRecruits();
            used = true;
        }
    }
}
