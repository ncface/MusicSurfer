using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCollider : MonoBehaviour
{
    public GameObject dancingPeople;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.player.tag)
        {
            other.GetComponent<Player>().Win();

            //starte feuerwerk
            Instantiate(dancingPeople,new Vector3(0,0.2f,(other.transform.position.z + 3)), Quaternion.identity);

            GameManager.Instance.GameWon();
        }
    }
}
