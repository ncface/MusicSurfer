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

    public string chordTag;

    public List<GameObject> lanes; // includes all lanes where the player can run
}
