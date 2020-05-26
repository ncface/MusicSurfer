using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public bool IsGameStarted { get; set; }
    public bool IsGameLost = false;
    public bool IsGameWon = false;

    public GameObject EndMenu; // panel object of end menu

    public void GameOver()
    {
        if (IsGameLost == false)
        {
            Time.timeScale = 0f;
            IsGameLost = true;
            IsGameStarted = false;
            EndMenu.SetActive(true);
        }
    }

}
