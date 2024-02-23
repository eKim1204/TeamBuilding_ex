using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("stage01");
    }
     public void Home()
    {
        SceneManager.LoadScene("title");
    }
}