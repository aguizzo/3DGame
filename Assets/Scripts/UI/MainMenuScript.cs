using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuTheme");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("save");
        FindObjectOfType<AudioManager>().StopPlaying("MenuTheme");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        SceneManager.LoadScene("credits");
        //FindObjectOfType<AudioManager>().StopPlaying("MenuTheme");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void ButtonSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress");
    }
}
