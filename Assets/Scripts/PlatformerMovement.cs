using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    private Transform rbody;
    private bool isOnPlatform;
    private Transform platformRBody;
    private Vector3 lastPlatformPosition;

    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Transform>();
      
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        
        if (isOnPlatform)
        {
            Vector3 deltaPosition = platformRBody.position - lastPlatformPosition;
            rbody.position = rbody.position + deltaPosition;
            lastPlatformPosition = platformRBody.position;
        }
       
    } 

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            platformRBody = collision.gameObject.GetComponent<Transform>();
            lastPlatformPosition = platformRBody.position;
            isOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            isOnPlatform = false;
            platformRBody = null;
        }
    }


}
