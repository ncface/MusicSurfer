using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour
{
    public float disctanceToHurdle;

    public void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "Hurdle")
            {
                child.transform.position += new Vector3(0, 0, disctanceToHurdle);
            }
            if (child.tag == "PassingCollider")
            {
                float playerThickness = GameSettings.Instance.player.GetComponent<CapsuleCollider>().radius;
                child.transform.position += new Vector3(0, 0, playerThickness);
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.player.tag)
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
