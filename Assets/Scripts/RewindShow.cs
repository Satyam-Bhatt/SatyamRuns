using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewindShow : MonoBehaviour
{
    public TMP_Text rewindNumber;
    
    private PlayerMovementController playerMovementController;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovementController = FindObjectOfType<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        rewindNumber.text = playerMovementController.rewindAvailable.ToString();
    }
}
