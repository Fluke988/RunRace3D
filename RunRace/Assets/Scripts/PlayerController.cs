using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    Vector3 playerMove;
    [SerializeField]
    private float speed;//playerSpeed
    public float playerJumpForce;
    public float playerVelocity = 0;
    public float gravity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
            playerVelocity = 0f;
            jump();
        }
        else
        {
            gravity = 30f;
            playerVelocity -= gravity * Time.deltaTime;
        }


        playerMove.Normalize();

        playerMove *= speed;
        playerMove.y = playerVelocity;

        characterController.Move(playerMove * Time.deltaTime);
    }

    private void jump()
    {




        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            print("Jump!");
            playerVelocity = playerJumpForce;
        }




    }
}