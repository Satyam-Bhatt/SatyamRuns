using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed;
    private float horizontalInput, verticalInput;
    private Rigidbody rb;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask playerMask;
    private int superJump;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           jumpKeyWasPressed = true;
        }
      
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }

    internal Vector3 GetPosition()
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(verticalInput * 5, rb.velocity.y, -horizontalInput * 5);

        if (rb.position.y < -3f)
        {
            FindObjectOfType<Game>().EndGame(); 
        }
      

       if (Physics.OverlapSphere(groundCheck.position, 0.1f, playerMask).Length == 0)
        {
           return;
        }

        if (jumpKeyWasPressed == true)
        {
            float jumpPower = 2f;
            if (superJump > 0)
            {
                jumpPower *= 1.5f;
                superJump--;
            }
            rb.AddForce(Vector3.up * 5 * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

      
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
           // Destroy(other.gameObject);
           // superJump++;
        }
    }



}
