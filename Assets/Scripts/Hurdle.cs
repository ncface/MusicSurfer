using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.player.tag)
        {
            
            if (transform.position.z - transform.lossyScale.z / 2 >= other.transform.position.z)
            {
                other.GetComponent<Player>().collision();
            }
        }
    }
}
