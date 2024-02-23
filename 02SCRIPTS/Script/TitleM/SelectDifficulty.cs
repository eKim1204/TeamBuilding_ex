using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDifficulty : MonoBehaviour
{
    public void PlayDifficult()
    {
        SceneManager.LoadSceneAsync("stage03");
    }
    public void PlayMedium()
    {
        SceneManager.LoadSceneAsync("stage02");
    }
    public void PlayEasy()
    {
        SceneManager.LoadSceneAsync("Eq");
    }
}