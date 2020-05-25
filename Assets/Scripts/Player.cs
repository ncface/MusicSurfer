using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{

    public float maxZ; // speed of the player to init the velocity
    public List<GameObject> lanes;

    private GameObject activeLane;

    private int currentLane = 0;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        currentLane = 1;
        activeLane = lanes.ElementAt(currentLane); // init start lane position of the player
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.Instance.IsGameStarted)
        {
            if (Input.GetMouseButtonDown(0)) // press mouse button to start the game
            {
                velocity = new Vector3(0, 0, maxZ); // init velocity, like addForce
                GameManager.Instance.IsGameStarted = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) // move left
        {
            if(activeLane == lanes.ElementAt(0))
            {
                // do nothing, border lane of the left side already reached
            } else
            {
                currentLane--;
                activeLane = lanes.ElementAt(currentLane);
                transform.position = new Vector3(activeLane.transform.position.x, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.D)) // move right
        {
            if (activeLane == lanes.ElementAt(lanes.Count - 1))
            {
                // do nothing, border lane of the right side already reached
            }
            else
            {
                currentLane++;
                activeLane = lanes.ElementAt(currentLane);
                transform.position = new Vector3(activeLane.transform.position.x, transform.position.y, transform.position.z);
            }
        }

        transform.position += velocity * Time.deltaTime;
    }
}
