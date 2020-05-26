using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.player.tag)
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            if (transform.position.z - transform.localScale.z / 2 == collisionPoint.z)
            {
                other.GetComponent<Player>().collision();
            }
        }
    }
}
