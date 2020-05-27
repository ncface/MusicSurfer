using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.player.tag)
        {
            GameManager.Instance.GameWon();
        }
    }
}
