using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityOfThePlayer : MonoBehaviour
{

    public float valueNeeded;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (FindObjectOfType<PlayerMovementController>().IsGrounded() == false)
        {
            StartCoroutine(curVelocity(0.1f));
           // print(valueNeeded);
        }

      //  Debug.Log(FindObjectOfType<PlayerMovementController>().IsGrounded());
    }

    
    IEnumerator curVelocity(float waitTime)
    {
        float previous;
        float current;
        float velocity;
        previous = transform.position.y;
        yield return new WaitForSeconds(waitTime);
        current = transform.position.y;
        velocity = current - previous;
        if (velocity == 0)
        {
            velocity = 0.000001f;
        }
        velocity = velocity / waitTime;
        //int intValue = (int)velocity;
          if (velocity < 0.0001f && velocity > -0.0001f)
          {
             velocity = 0;
          }

        valueNeeded = velocity;
    }

}
