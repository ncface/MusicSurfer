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

    public bool IsGameStarted = false;
    public bool IsGameLost = false;
    public bool IsGameWon = false;

    public GameObject EndMenu; // panel object of end menu
    public GameObject WonMenu;

    public void GameOver()
    {
        if (IsGameLost == false)
        {
            Time.timeScale = 0f;
            IsGameLost = true;
            IsGameStarted = false;
            EndMenu.SetActive(true);
            GameSettings.Instance.audioPlayer.GetComponent<AudioSource>().Pause();
        }
    }
    public void GameWon()
    {
        if (!IsGameLost && IsGameStarted)
        {
            //Time.timeScale = 0f;
            IsGameWon = true;
            IsGameStarted = false;
            WonMenu.SetActive(true);
            GameSettings.Instance.audioPlayer.GetComponent<AudioSource>().Pause();
        }
    }

    public void startGame()
    {
        IsGameStarted = true;
        GameSettings.Instance.audioPlayer.GetComponent<AudioSource>().Play();
    }
}
