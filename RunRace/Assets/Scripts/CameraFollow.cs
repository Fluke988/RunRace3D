using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset.z = player.forward.z * 2f;
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z),10f);
    }
}
