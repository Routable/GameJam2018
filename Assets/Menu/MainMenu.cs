using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;
    public GameObject howToPlay;

    public void PlayGame()
    {
        SceneManager.LoadScene("DannyTestScene");
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

    public void CloudCLick()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void LoadHowToPlay()
    {
        howToPlay.SetActive(true);
    }
}
