using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    Vector3 playerMove;
    [SerializeField] 
    private  float speed;//playerSpeed
    public float playerJumpForce;
    public float playerVelocity = 0;
    public float gravity;
    private bool doubleJump;
    private bool wallSlide;
    private bool playerTurn;
    public Animator animator;

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
            
            wallSlide = false;
            playerVelocity = 0f;

            if ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                jump();
            }
            
            
            if (playerTurn)
            {
                playerTurn = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
            }
        }

        if (!wallSlide)
        {
            //animator.SetBool("WallSlide", true);
            print(" wall slide off");
            gravity = 30f;
            playerVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            
            //print("wallslide");
            //gravity = 15f;
            playerVelocity -= gravity * Time.deltaTime * 0.5f;
        }


        animator.SetBool("Grounded", characterController.isGrounded);
        animator.SetBool("WallSlide", wallSlide);

        //else
        //{
        //    gravity = 30f;
        //    playerVelocity -= gravity * Time.deltaTime;

        //    //this logic is for double jump, will activate if required
        //    //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && doubleJump)
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


        

        
        
            animator.SetTrigger("Jump");
           // wallSlide = false;
            print("Jump!");
            playerVelocity = playerJumpForce;
            //doubleJump = true;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
        
        


        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!characterController.isGrounded)
        {
            if (hit.collider.tag == "Wall")
            {
                //if (playerVelocity < -0.2f)
                //{
                //    animator.SetBool("WallSlide", true);
                //    print("Sliding");
                //    wallSlide = true;
                //}
                //else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // need to fix the logic according to our game progress
                //{
                //    //jump();
                //    playerVelocity = playerJumpForce;
                //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
                //    doubleJump = false;
                //    wallSlide = false;
                //}



                
                 if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // need to fix the logic according to our game progress
                {
                    //jump();
                    playerVelocity = playerJumpForce;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
                    doubleJump = false;
                    wallSlide = false;
                }
                else
                {
                    animator.SetBool("WallSlide", true);
                    print("Sliding");
                    wallSlide = true;
                }





            }
        }

        //if (hit.collider.tag == "finish")
        //{
        //    print("Game Over!!");
        //}

        else
        {
            if (transform.forward != hit.collider.transform.right && hit.collider.tag == "Ground" && !playerTurn)
            {
                playerTurn = true;
                print("Player restricted from turning");
            }
        }

        
    }
}
