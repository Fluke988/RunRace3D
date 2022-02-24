using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 playerMove;
    Animator animator;

    [SerializeField]
    private float speed;//playerSpeed
    public float playerJumpForce;
    public float playerVelocity = 0;
    public float gravity;
    private bool doubleJump;
    private bool wallSlide;
    private bool playerTurn;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {

    }

    void Update()
    {
        playerMove = Vector3.zero;
        playerMove = transform.forward;

        if (characterController.isGrounded)
        {
            animator.SetBool("Grounded", true);
            wallSlide = false;
            playerVelocity = 0f;
            jump();
            //if(playerTurn)
            //{
            //    playerTurn = false;
            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            //}
        }

        if(!wallSlide)
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("WallSlide", false);
            //print("Not Wall Slide!");
            gravity = 30f;
            playerVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            animator.SetBool("Grounded", false);
            animator.SetBool("WallSlide", true);
            //print("Slide!");
            //gravity = 15f;
            playerVelocity -= gravity * Time.deltaTime * 0.5f;
        }

        //else
        //{
        //    gravity = 30f;
        //    playerVelocity -= gravity * Time.deltaTime;

        //    //this logic is for double jump, will activate it if required
        //    //if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && doubleJump)
        //    //{
        //    //    print("Jump!");
        //    //    playerVelocity += playerJumpForce * 0.5f;
        //    //    doubleJump = false;
        //    //    print("DoubleJump!!");
        //    //}
        //}

        playerMove.Normalize();

        playerMove *= speed;
        playerMove.y = playerVelocity;

        characterController.Move(playerMove * Time.deltaTime);
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Jump");
            //wallSlide = false; 
            print("Jump!");
            playerVelocity = playerJumpForce;
            //doubleJump = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!characterController.isGrounded)
        {
            if (hit.collider.tag == "wall")
            {
                if (playerVelocity < 0f)
                {
                    print("Sliding!!");
                    wallSlide = true;
                }
                /*else*/if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))             //need to fix the logic according to our game progress
                {
                    //jump();
                    playerVelocity = playerJumpForce;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                    doubleJump = false;
                    wallSlide = false;
                }
            }
        }
        //else
        //{
        //    if((transform.forward != hit.collider.transform.right) && (hit.collider.tag=="ground") && !playerTurn)
        //    {
        //        playerTurn = true;
        //        print("Player restricted from turning");
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("finish"))
        {
            print("Game Over!!");

            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            animator.SetTrigger("Dance");
            //Initiate.Fade("GameOverScene", Color.yellow, 1f);
        }
    }
}