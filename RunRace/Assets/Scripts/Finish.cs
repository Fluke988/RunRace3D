using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        print(other.gameObject.name);
        if (other.gameObject.GetComponent<PlayerController>())
        {
            print("Player Dance");
            other.gameObject.GetComponent<PlayerController>().animator.SetTrigger("Dance");
        }
        else if (other.gameObject.GetComponent<AIPlayerController>())
        {
            print("AI Dance");
            other.gameObject.GetComponent<AIPlayerController>().AIanimator.SetTrigger("Dance");
        }
        //PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
}
