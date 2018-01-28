using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void OnHover()
    {
        source.PlayOneShot(hover);
    }

    public void OnClick()
    {
        source.PlayOneShot(click);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(2);  
    }

}
