using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindReset : MonoBehaviour
{
    private PlayerMovementController playerMovementController;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovementController = FindObjectOfType<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            playerMovementController.rewindAvailable = 0;
        }
    }
}
