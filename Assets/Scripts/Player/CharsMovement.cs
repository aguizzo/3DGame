using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharsMovement : MonoBehaviour
{
    private bool start;
    private bool inBattle;
    private float battlePosZ;
    public static Vector3 pos;
    private float lateralSpeed;
    private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(1).GetChild(0).gameObject;
        text.GetComponent<UnityEngine.UI.Text>().text = CharsController.Life.ToString();
        start = true;
        inBattle = false;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        if (LevelController.inBattle)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), 10 * Time.deltaTime);
            if (Mathf.Round(pos.z) == Mathf.Round(battlePosZ)) CharsController.animState = 2;
        }
        else
        {
            battlePosZ = pos.z + 10;
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && start == true)
            {
                CharsController.animState = 1;
                lateralSpeed = 10f;
                start = false;
            }
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !inBattle)
            {
                moveLeft();
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !inBattle)
            {
                moveRight();
            }
        }
        text.GetComponent<UnityEngine.UI.Text>().text = CharsController.Life.ToString();
    }
    private void moveLeft()
    {
        if (this.gameObject.transform.position.x > LevelController.leftSide)
        {
            transform.Translate(Vector3.left * Time.deltaTime * lateralSpeed, Space.World);
        }
    }

    private void moveRight()
    {
        if (this.gameObject.transform.position.x < LevelController.rightSide)
        {
            transform.Translate(Vector3.right * Time.deltaTime * lateralSpeed, Space.World);
        }
    }
}
