﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.playerTag)
        {
            other.GetComponent<Player>().collision();
        }
    }
    
}