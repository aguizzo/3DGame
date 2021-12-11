using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    public void destroyRecruits()
    {
        GameObject recruit = transform.GetChild(transform.childCount - 1).gameObject;
        recruit.transform.parent = null;
        Destroy(recruit);
        recruit = transform.GetChild(transform.childCount - 1).gameObject;
        recruit.transform.parent = null;
        Destroy(recruit);
    }
}
