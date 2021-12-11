using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    private bool start = true;
    private GameObject cam;
    private float battlePosZ;

    private void Start()
    {
        cam = transform.GetChild(1).gameObject;
        battlePosZ = transform.position.z + 10;
    }

    //UPDATE//
    void Update()
    {
        if (LevelController.inBattle) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, battlePosZ), 10 * Time.deltaTime);
            speed = 0f;
        }
        else battlePosZ = transform.position.z + 10;

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && start == true)
        {
            speed = 15f;
            start = false;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
    }

    public void Move()
    {
        speed = 15f;
    }
}
