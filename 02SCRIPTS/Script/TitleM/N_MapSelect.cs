using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class N_MapSelect : MonoBehaviour
{
    public void Select01()

    {   
        Debug.Log("뭐함?");
        SceneManager.LoadSceneAsync("stage01");
        
    }

    public void Select02()
    {
        Debug.Log("뭐함?");
        SceneManager.LoadSceneAsync("stage02");
    }

    public void GoBack()
    {
        SceneManager.LoadSceneAsync("title");
    }
}