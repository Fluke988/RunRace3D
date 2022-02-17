using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    private Vector3 motion;
    public float speed = 3.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        motion = new Vector3(0, 0, 0);
        motion = transform.forward;
        motion.Normalize();

        motion = motion * speed * Time.deltaTime;
        characterController.Move(motion);
    }
}
