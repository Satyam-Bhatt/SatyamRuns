using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnceBlock : MonoBehaviour
{
    [SerializeField] GameObject[] wayPoints;
    [SerializeField] float speed = 1f;

    private int currentWayPointIndex = 0;

    private bool move = false;
    private bool stopMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].transform.position) < 0.1f)
            {
                currentWayPointIndex++;
            }
            
        }

        if (stopMoving == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
        }


        if (currentWayPointIndex == wayPoints.Length - 1)
        {
            move = false;
        }

        if(transform.position == wayPoints[wayPoints.Length - 1].transform.position)
        {
            stopMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            move = true;
        }
    }
}
