using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform groundCheck;
    public LayerMask groundMask;

    public GameObject animationCharacter;

    // var settings from GameSettings
    private float run_Speed;
    private float jump_Speed;

    private List<GameObject> lanes;

    private GameObject activeLane;
    private int current_Lane;
    private Vector3 move;

    private Rigidbody rb;

    private bool isGrounded = true;
    private float groundDistance = 0.4f;

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
        if (isGrounded != Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            isGrounded = !isGrounded;
            if(isGrounded)
            {
                landing();
            } else
            {
                takeoff();
            }
        }

        if (GameManager.Instance.IsGameStarted)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        } 
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (!GameManager.Instance.IsGameStarted && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost)
        {
            if (Input.GetMouseButtonDown(0)) // press mouse button to start the game
            {
                move = new Vector3(0, 0, run_Speed); // init velocity, like addForce
                GameManager.Instance.startGame();
                animationCharacter.GetComponent<PersonAnimation>().run();
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost) // move left
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

        if (Input.GetKeyDown(KeyCode.D) && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost) // move right
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

        if (Input.GetButtonDown("Jump") && isGrounded && GameManager.Instance.IsGameStarted)
        {
            rb.AddForce(new Vector3(0, jump_Speed, 0), ForceMode.Impulse);

            animationCharacter.GetComponent<PersonAnimation>().jump();
        }
        
        transform.position += move * Time.deltaTime;
        transform.rotation = Quaternion.identity; // blocks the rotation of the player
    }
    
    private void landing()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            animationCharacter.GetComponent<PersonAnimation>().run();
        }
        else
        {
            animationCharacter.GetComponent<PersonAnimation>().idle();
        }
        move += new Vector3(0, 0, GameSettings.Instance.jumpSlowdown);
    }

    private void takeoff()
    {
        move -= new Vector3(0, 0, GameSettings.Instance.jumpSlowdown);
    }

    public void collision()
    {
        move = Vector3.zero;
        animationCharacter.GetComponent<PersonAnimation>().idle();
        GameManager.Instance.GameOver();
    }

    public void win()
    {
        move = Vector3.zero;
        animationCharacter.GetComponent<PersonAnimation>().win();
        rb.velocity = new Vector3(0,0,0);
    }
}
