using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReGame()
    {
        SceneManager.LoadSceneAsync("title");
    }

    public void GoTitle()
    {
        SceneManager.LoadSceneAsync("title");
    }
}