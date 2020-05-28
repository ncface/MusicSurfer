using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.player.tag)
        {
            if (tag == GameSettings.Instance.hurdle.tag)
            {
                //bei hindernisen nur mit dem vorderen teil kollidieren
                if (transform.position.z - transform.lossyScale.z / 2 >= other.transform.position.z)
                {
                    other.GetComponent<Player>().Collision();
                }
            } else
            {
                other.GetComponent<Player>().Collision();
            }
        }
    }
}
