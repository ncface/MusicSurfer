using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Restart()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
