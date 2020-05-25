using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // var settings from GameSettings
    private float runSpeed;
    private List<GameObject> lanes;

    private GameObject activeLane;
    private int currentLane = 0;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        lanes = GameSettings.Instance.lanes;
        runSpeed = GameSettings.Instance.runSpeed;
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
                velocity = new Vector3(0, 0, runSpeed); // init velocity, like addForce
                GameManager.Instance.IsGameStarted = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) // move left
        {
            if(activeLane != lanes.ElementAt(0))
            {
                currentLane--;
                activeLane = lanes.ElementAt(currentLane);
                transform.position = new Vector3(activeLane.transform.position.x, transform.position.y, transform.position.z);
            } else
            {
                // Debug.Log("Border lane of the left side already reached");
            }
        }

        if (Input.GetKeyDown(KeyCode.D)) // move right
        {
            if (activeLane != lanes.ElementAt(lanes.Count - 1))
            {
                currentLane++;
                activeLane = lanes.ElementAt(currentLane);
                transform.position = new Vector3(activeLane.transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                // Debug.Log("Border lane of the left right already reached");
            }
        }

        transform.position += velocity * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        Debug.Log(GameSettings.Instance.chordTag);
        if (other.tag == GameSettings.Instance.chordTag)
        {
            other.GetComponent<Chord>().hit();
        }
    }
}
