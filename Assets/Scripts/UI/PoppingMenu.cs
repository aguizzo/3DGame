using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoppingMenu : MonoBehaviour
{
    private LevelController lvlCont;
    private Transform image, LevelSelector;
    public GameObject pauseMenu;
    private Text text, levelName; 
    public static Transform DeathMenu;
    private bool lvlselact = false, fadingup = false, fadingdown = false;
    public static bool start = false, gamePaused = false;
    private float timer = 0f;
    public Button b1, b2, b3, b4, b5, b6, b7, ResumeB, MenuB, ExitB;
    // Start is called before the first frame update
    void Start()
    {
        lvlCont = GameObject.FindObjectOfType<LevelController>();
        text = transform.GetChild(1).GetComponent<UnityEngine.UI.Text>();
        image = transform.GetChild(0);
        LevelSelector = transform.GetChild(2);
        LevelSelector.gameObject.SetActive(false);
        DeathMenu = transform.GetChild(3);
        DeathMenu.gameObject.SetActive(false);
        levelName = transform.GetChild(4).GetComponent<UnityEngine.UI.Text>();
        levelName.text = "";
        levelName.color = new Color(levelName.color.r, levelName.color.g, levelName.color.b, 0);
        levelName.gameObject.SetActive(false);
        b1.onClick.AddListener(() => LvlSel(1));
        b2.onClick.AddListener(() => LvlSel(2));
        b3.onClick.AddListener(() => LvlSel(3));
        b4.onClick.AddListener(() => LvlSel(4));
        b5.onClick.AddListener(() => LvlSel(5));
        b6.onClick.AddListener(() => DMSel(false));
        b7.onClick.AddListener(() => DMSel(true));

    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            fadingup = true;
            timer = 0f;
            start = false;
            levelName.gameObject.SetActive(true);
            levelName.text = "Level " + LevelController.lvl;
            levelName.color = new Color(levelName.color.r, levelName.color.g, levelName.color.b, 0);
        }


        if (fadingup)
        {
            timer += Time.deltaTime/3f;
            if (levelName.color.a > 1)
            {
                fadingup = false;
                fadingdown = true;
            }
            else levelName.color = new Color(levelName.color.r, levelName.color.g, levelName.color.b, timer);
        }
        else if (fadingdown)
        {
            timer -= Time.deltaTime/3f;
            if (levelName.color.a < 0) fadingdown = false; 
            else levelName.color = new Color(levelName.color.r, levelName.color.g, levelName.color.b, timer);
        }

        if (LevelController.godmode)
        {
            string s = ("God Mode Activated\nU  to add units\nI  for invincibility\nL  to select level\n\nLevel " + (LevelController.lvl) + "\nInvincibility " + (LevelController.inv ? "ON" : "OFF")).ToString();
            text.text = s;
            image.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.L))
            {
                lvlselact = !lvlselact;
                LevelSelector.gameObject.SetActive(lvlselact);
            }
        }
        else
        {
            text.text = "";
            image.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void LvlSel(int b)
    {
        lvlselact = !lvlselact;
        LevelSelector.gameObject.SetActive(lvlselact);
        lvlCont.SetLevel(b);
    }

    void DMSel(bool b)
    {
        if (b)
        {
            DeathMenu.gameObject.SetActive(false);
            lvlCont.SetLevel(LevelController.lvl);
        }
        else
        {
            //return to main menu
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void returnToMenu()
    {
        //return to menu and reescale time
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
