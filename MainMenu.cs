using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsWindow;

    public void ButtonStart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        //GameController.Instance.AudioManager.PlayMusic(false);
        // GameController.Instance.AudioManager.PlaySound("forest2");
    }

    public void ButtonOptions()
    {
        ShowWindow(optionsWindow);
    }

    public void ButtonExit()
    {
        Application.Quit();
    }

    public void SetSoundVolume(Slider slider)
    {
        GameController.Instance.AudioManager.SfxVolume = slider.value;
    }
    public void SetMusicVolume(Slider slider)
    {
        GameController.Instance.AudioManager.MusicVolume = slider.value;
    }

    public void ShowWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Open", true);
        
    }

    public void HideWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Open", false);
        
    }


    // Use this for initialization
    private void Start ()
    {   
        Time.timeScale = 1f;
        //GameController.Instance.AudioManager.PlayMusic(true);
       // GameController.Instance.AudioManager.PlaySound("castle2");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
