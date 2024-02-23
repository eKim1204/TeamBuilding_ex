using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    public void PlayGame()

    {   
        Debug.Log("뭐함?");
        SceneManager.LoadSceneAsync("N_MapSelect");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void Awake() 
{
    DontDestroyOnLoad(gameObject);
    Screen.SetResolution(1920, 1080, true);
}

}