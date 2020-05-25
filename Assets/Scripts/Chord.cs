using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour
{
    public void hit()
    {
        GetComponent<AudioSource>().Play();
    }
}
