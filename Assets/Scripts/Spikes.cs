using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
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
        if (other.CompareTag("Player") && playerMovementController.isRewinding == false)
        {
            FindObjectOfType<Game>().HitSpike();
        }
    }
}
