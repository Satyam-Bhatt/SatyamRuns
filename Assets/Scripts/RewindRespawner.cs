using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindRespawner : MonoBehaviour
{
    private PlayerMovementController playerMovementController;
    private RewindCoin rewindCoin;

    [SerializeField] GameObject spawnItem;
    [SerializeField] float timeInterval = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovementController = FindObjectOfType<PlayerMovementController>();
        rewindCoin = FindObjectOfType<RewindCoin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementController.respawnRewind)
        {
            Invoke("Respawn", timeInterval);
            playerMovementController.respawnRewind = false;
        }
    }

    private void Respawn()
    {
        Instantiate(spawnItem, rewindCoin.position, rewindCoin.rotation);
    }
}
