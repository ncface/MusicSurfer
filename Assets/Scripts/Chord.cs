using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.playerTag)
        {
            hit();
        }
    }

    public void hit()
    {
        GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2);
    }
}
