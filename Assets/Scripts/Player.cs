using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;

    // var settings from GameSettings
    private float run_Speed;
    private float jump_Speed;

    private List<GameObject> lanes;

    private GameObject activeLane;
    private int current_Lane;
    private Vector3 move;

    private Rigidbody rb;

    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lanes = GameSettings.Instance.lanes;
        run_Speed = GameSettings.Instance.runSpeed; 
        jump_Speed = GameSettings.Instance.jumpSpeed;
        current_Lane = GameSettings.Instance.currentLane;
        activeLane = lanes.ElementAt(current_Lane); // init start lane position of the player
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!GameManager.Instance.IsGameStarted)
        {
            if (Input.GetMouseButtonDown(0)) // press mouse button to start the game
            {
                move = new Vector3(0, 0, run_Speed); // init velocity, like addForce
                GameManager.Instance.IsGameStarted = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) // move left
        {
            if(activeLane != lanes.ElementAt(0))
            {
                current_Lane--;
                activeLane = lanes.ElementAt(current_Lane);
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
                current_Lane++;
                activeLane = lanes.ElementAt(current_Lane);
                transform.position = new Vector3(activeLane.transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                // Debug.Log("Border lane of the left right already reached");
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0,jump_Speed, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        transform.position += move * Time.deltaTime;
        transform.rotation = Quaternion.identity; // blocks the rotation of the player
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameSettings.Instance.chordTag)
        {
            other.GetComponent<Chord>().hit();
        }
    }

    public void collision()
    {
        move = Vector3.zero;
    }
}
