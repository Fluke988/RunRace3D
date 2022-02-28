using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    CharacterController AIcharacterController;

    Vector3 AIplayerMove;
    [SerializeField]
    private float AIspeed;//playerSpeed
    public float AIplayerJumpForce;
    public float AIplayerVelocity = 0;
    public float AIgravity;
    private bool AIdoubleJump;
    private bool AIwallSlide;
    private bool AIplayerTurn;
    public Animator AIanimator;
    public float timeDelay;
    bool AIjump = true;

    private void Awake()
    {
        AIcharacterController = GetComponent<CharacterController>();
        AIanimator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        AIplayerMove = Vector3.zero;
        AIplayerMove = transform.forward;

        if (AIcharacterController.isGrounded)
        {
            AIwallSlide = false;    
            AIplayerVelocity = 0;

            RaycastMethod();

            print("Grounded");
            AIanimator.SetBool("Grounded",AIcharacterController.isGrounded);

        }
        if (!AIwallSlide)
        {
            AIanimator.SetBool("WallSlide", true);
            print(" wall slide off");
            AIgravity = 30f;
            AIplayerVelocity -= AIgravity * Time.deltaTime;
        }

        if (AIplayerTurn)
        {
            AIplayerTurn = false;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
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

        if (Physics.Raycast(transform.position /*+ new Vector3(0,2f,0)*/, transform.forward, out hit, 7f ))
        {

            if (hit.collider.tag == "Wall")
            {



                AIJump();
            }

            Debug.DrawLine(transform.position /*+ new Vector3(0, 2f, 0)*/, hit.point, Color.red);
            print("RayCast");
        }
    }

    private void AIJump()
    {


        AIanimator.SetTrigger("Jump");
        // wallSlide = false;
        print("Jump!");
        AIplayerVelocity = AIplayerJumpForce;
        //doubleJump = true;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);


    }


    


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.tag == "Wall")
        {


            if (AIjump)
            {
                StartCoroutine(AIJumpDelay(timeDelay));
                print("AIcoroutine Start");
            }
            //if (AIplayerVelocity < -0.2f)
            //{
            //    AIanimator.SetBool("WallSlide", true);
            //    print("Sliding");
            //    AIwallSlide = true;
            //}
            //else /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))*/ // need to fix the logic according to our game progress
            //{



            //    //jump();
            //    //AIplayerVelocity = AIplayerJumpForce;
            //    //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
            //    //AIdoubleJump = false;
            //    //AIwallSlide = false;

            //}

        }

        else
        {
            if (transform.forward != hit.collider.transform.right && hit.collider.tag == "Ground" && !AIplayerTurn)
            {
                AIplayerTurn = true;
                print("Player restricted from turning");
            }
        }
    }

    IEnumerator AIJumpDelay(float timeDelay)
    {
        timeDelay = UnityEngine.Random.Range(0.5f,1.0f);
        print("Time: " + timeDelay);
        //AIplayerTurn = false;
        AIwallSlide = true;
        AIjump = false;

        yield return new WaitForSeconds(timeDelay);

        if (!AIcharacterController.isGrounded)
        {
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
            AIplayerVelocity = AIplayerJumpForce;
            AIanimator.SetTrigger("Jump");
            AIdoubleJump = false;
            AIwallSlide = false;
        }
        //AIplayerTurn =true;
        AIjump = true;
        
    }
}
