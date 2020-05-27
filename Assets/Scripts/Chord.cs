using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Chord : MonoBehaviour
{
    public float disctanceToHurdle;

    public ParticleSystem destroyEffect;

    public void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "Hurdle")
            {
                //child.transform.position += new Vector3(0, 0, disctanceToHurdle);
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
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "PassingCollider")
            {
                Destroy(child);
            }
        }
        //start audio
        GetComponent<AudioSource>().Play();

        //make chord object invisible
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "Chord")
            {
                child.GetComponent<Renderer>().enabled = false;
                child.GetComponent<Collider>().enabled = false;
                spawnDestroyEffect(child);
            }
        }

        //müssen wir überhaupt zerstören?
        //Destroy(gameObject, 2);
    }

    private void spawnDestroyEffect(GameObject child)
    {
        Transform spawnTransform = child.transform;
        Vector3 spawnPosition = new Vector3(spawnTransform.position.x, spawnTransform.position.y, spawnTransform.position.z);

        GameObject effect = Instantiate(destroyEffect.gameObject, spawnPosition, Quaternion.identity);
        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = child.GetComponent<Renderer>().material.color;
        Destroy(effect, destroyEffect.main.startLifetime.constant); // destroys effect after liftetime
    }
}
