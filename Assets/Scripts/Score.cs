using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public PlayerMovementController player;
    public Text scoreText;

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        dist = 500 - player.transform.position.x;
        scoreText.text = dist.ToString("0");
    }
}
