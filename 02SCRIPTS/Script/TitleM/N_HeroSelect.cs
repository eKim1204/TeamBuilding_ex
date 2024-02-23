using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class N_HeroSelect : MonoBehaviour
{
    //임시
    public void SelectMainStage()

    {   
        Debug.Log("뭐함?");
        SceneManager.LoadSceneAsync("stage01");
    }
    public void GoBack()
    {
        SceneManager.LoadSceneAsync("N_MapSelect");
    }
}