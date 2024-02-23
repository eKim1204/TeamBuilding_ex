using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene2 : MonoBehaviour
{
    public void Restart2()
    {
        SceneManager.LoadScene("stage02");
    }

    public void Home()
    {
        SceneManager.LoadScene("title");
    }
}