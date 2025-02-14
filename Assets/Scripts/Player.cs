﻿using System.Collections.Generic;
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
    private bool isSliding = false;
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

        //mobile input handler
        SwipeDetector.OnSwipe += MobileInputHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded != Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            isGrounded = !isGrounded;
            if(isGrounded)
            {
                Landing();
            } else
            {
                TakeOff();
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
        
        CheckStandaloneInputs();
        
        transform.position += move * Time.deltaTime;
        transform.rotation = Quaternion.identity; // blocks the rotation of the player
    }

    private void MobileInputHandler(SwipeData data)
    {
        if (GameManager.Instance.IsGameStarted)
        {
            switch (data.Direction)
            {
                case SwipeDirection.Left:
                    MoveLeft();
                    break;
                case SwipeDirection.Right:
                    MoveRight();
                    break;
                case SwipeDirection.Up:
                    ResetSlide();
                    break;
                case SwipeDirection.Down:
                    InitSlide();
                    break;
            }
        } else
        {
            switch (data.Direction)
            {
                case SwipeDirection.Left:
                    MoveLeft();
                    break;
                case SwipeDirection.Right:
                    MoveRight();
                    break;
                default:
                    StartGame();
                    break;
            }
        }
    }

    private void CheckStandaloneInputs()
    {
        if (!GameManager.Instance.IsGameStarted && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost)
        {
            if (Input.GetButtonDown("Start")) // press mouse button to start the game
            {
                StartGame();
            }
        }

        if (Input.GetButtonDown("HorizontalLeft") && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost) // move left
        {
            MoveLeft();

        }

        if (Input.GetButtonDown("HorizontalRight") && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost) // move right
        {
            MoveRight();
        }

        if (Input.GetButtonDown("Duck") && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost) // slide
        {
            InitSlide();

        }
        else if (Input.GetButtonUp("Duck") && !GameManager.Instance.IsGameWon && !GameManager.Instance.IsGameLost)
        {
            ResetSlide();
        }



        if (Input.GetButtonDown("Jump") && isGrounded && GameManager.Instance.IsGameStarted)
        {
            rb.AddForce(new Vector3(0, jump_Speed, 0), ForceMode.Impulse);

            animationCharacter.GetComponent<PersonAnimation>().jump();
        }
    }

    private void StartGame()
    {
        move = new Vector3(0, 0, run_Speed); // init velocity, like addForce
        GameManager.Instance.startGame();
        animationCharacter.GetComponent<PersonAnimation>().run();
    }

    private void MoveLeft()
    {
        //check if obstacle
        Vector3 positionOfRayOrigin = transform.position + new Vector3(0, -0.5f, 0);
        Ray ray = new Ray(positionOfRayOrigin, new Vector3(-1, 0, 0));
        RaycastHit hits;
        bool obstacleAtLeft = Physics.Raycast(ray, out hits, 2f)
                && hits.transform.tag == GameSettings.Instance.obstaclePrefabs[0].tag;
        if (!obstacleAtLeft
            && activeLane != lanes.ElementAt(0))
        {
            current_Lane--;
            activeLane = lanes.ElementAt(current_Lane);
            transform.position = new Vector3(activeLane.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void MoveRight()
    {
        //check if obstacle
        Vector3 positionOfRayOrigin = transform.position + new Vector3(0, -0.5f, 0);
        Ray ray = new Ray(positionOfRayOrigin, new Vector3(1, 0, 0));
        RaycastHit hits;
        bool obstacleAtRight = Physics.Raycast(ray, out hits, 2f)
                && hits.transform.tag == GameSettings.Instance.obstaclePrefabs[0].tag;
        if (!obstacleAtRight
            && activeLane != lanes.ElementAt(lanes.Count - 1))
        {
            current_Lane++;
            activeLane = lanes.ElementAt(current_Lane);
            transform.position = new Vector3(activeLane.transform.position.x, transform.position.y, transform.position.z);
        }
    }


    private void ResetSlide()
    {
        if (isSliding)
        {
            isSliding = false;

            // reset player collider
            CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
            collider.height = 1.8f;
            collider.center = new Vector3(0, -0.15f, 0);
            
            // reset animation player position
            // wierd setting - depends on the parent player?
            animationCharacter.transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
            animationCharacter.transform.Rotate(90, 0, 0);

            if (GameManager.Instance.IsGameStarted)
            {
                animationCharacter.GetComponent<PersonAnimation>().run();
            }
        }
    }

    private void InitSlide()
    {
        if (!isSliding)
        {
            // shrink player collider
            CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
            collider.height = 0f;
            collider.center = new Vector3(0, -0.8f, 0);
            animationCharacter.GetComponent<PersonAnimation>().idle();

            // adapt animation player position
            // wierd setting - depends on the parent player?
            animationCharacter.transform.position = new Vector3(transform.position.x, 0.40f, transform.position.z + 0.9f);
            animationCharacter.transform.Rotate(-90, 0, 0);

            isSliding = true;
        }
    }

    private void Landing()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            animationCharacter.GetComponent<PersonAnimation>().run();
            move += new Vector3(0, 0, GameSettings.Instance.jumpSlowdown);
        }
        else if(!(GameManager.Instance.IsGameWon || GameManager.Instance.IsGameLost))
        {
            animationCharacter.GetComponent<PersonAnimation>().idle();
        }
    }

    private void TakeOff()
    {
        move -= new Vector3(0, 0, GameSettings.Instance.jumpSlowdown);
    }

    public void Collision()
    {
        move = Vector3.zero;
        animationCharacter.GetComponent<PersonAnimation>().idle();
        GameManager.Instance.GameOver();
    }

    public void Win()
    {
        move = Vector3.zero;
        ResetSlide();
        animationCharacter.GetComponent<PersonAnimation>().win();
        rb.velocity = new Vector3(0,0,0);
    }

}
