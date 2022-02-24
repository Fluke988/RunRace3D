using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    CharacterController AIcharacterController;
    Vector3 AIplayerMove;
    Animator AIanimator;

    [SerializeField]
    private float AIspeed;                  //playerSpeed
    public float AIplayerJumpForce;
    public float AIplayerVelocity = 0;
    public float AIgravity;
    private bool AIdoubleJump;
    private bool AIwallSlide;
    private bool AIplayerTurn;

    private void Awake()
    {
        AIcharacterController = GetComponent<CharacterController>();
        AIanimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        AIplayerMove = Vector3.zero;
        AIplayerMove = transform.forward;

        if (AIcharacterController.isGrounded)
        {
            AIplayerVelocity = 0f;
            RaycastMethod();
            AIanimator.SetBool("Grounded", AIcharacterController.isGrounded);
        }
        if (!AIwallSlide)
        {
            AIanimator.SetBool("Grounded", true);
            AIanimator.SetBool("WallSlide", false);
            //print("Not Wall Slide!");
            AIgravity = 30f;
            AIplayerVelocity -= AIgravity * Time.deltaTime;
        }
        else
        {
            AIgravity = 10f;
            AIplayerVelocity -= AIgravity * Time.deltaTime;
        }
        AIanimator.SetBool("Grounded", AIcharacterController.isGrounded);
        AIanimator.SetBool("WallSlide", AIwallSlide);

        AIplayerMove.Normalize();

        AIplayerMove *= AIspeed;
        AIplayerMove.y = AIplayerVelocity;

        AIcharacterController.Move(AIplayerMove * Time.deltaTime);
    }

    private void RaycastMethod()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position /*+ new Vector3(0, 2f, 0)*/, transform.forward, out hit, 6f))
        {
            if(hit.collider.tag=="wall")
            {
                AIJump();
            }


            Debug.DrawLine(transform.position /*+ new Vector3(0, 2f, 0)*/, hit.point, Color.red);
        }
    }

    private void AIJump()
    {
        
            AIanimator.SetTrigger("Jump");
            //wallSlide = false; 
            print("AIJump!");
            AIplayerVelocity =AIplayerJumpForce;
            //doubleJump = true;
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "wall")
        {
            if (AIplayerVelocity < -0.2f)
            {
                AIanimator.SetBool("WallSlide", AIwallSlide);
                print("Sliding!!");
                AIwallSlide = true;
            }
            else
            /* if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))*/             //need to fix the logic according to our game progress
            {
                //jump();
                AIplayerVelocity = AIplayerJumpForce;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
                AIdoubleJump = false;
                AIwallSlide = false;
            }
        }
    }
}
