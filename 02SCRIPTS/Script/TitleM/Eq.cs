using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eq : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadSceneAsync("stage01");
    }

    
}