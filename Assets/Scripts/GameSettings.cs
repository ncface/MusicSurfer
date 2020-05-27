using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    #region Singleton

    private static GameSettings _instance;

    public static GameSettings Instance => _instance;

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

    public float runSpeed; // speed of the player to init the velocity
    public float jumpSpeed;
    public float jumpSlowdown;

    public GameObject chordPrefab;
    public GameObject player;
    public GameObject hurdle;
    public GameObject passingCheck;
    public List<GameObject> obstaclePrefabs;

    public List<GameObject> lanes; // includes all lanes where the player can run
    public int currentLane; // defines on which lane the player is on init
}
