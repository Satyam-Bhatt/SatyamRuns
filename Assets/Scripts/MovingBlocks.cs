using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlocks : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private float speed = 1f;

    int currentWaypiontIndex = 0;

    private void Start()
    {
     
    }



    void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[currentWaypiontIndex].transform.position) < 0.1f)
        {
            currentWaypiontIndex++;
            if(currentWaypiontIndex >= wayPoints.Length)
            {
                currentWaypiontIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypiontIndex].transform.position, speed * Time.unscaledDeltaTime);
        // OnTriggerEnter(player.GetComponent<CharacterController>());
    }
}
