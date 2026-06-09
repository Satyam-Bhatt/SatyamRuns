using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpike : MonoBehaviour
{
    [SerializeField] private GameObject spike;
    //[SerializeField] private PlayerMovementController playerMovementController;
    private PlayerMovementController playerMovementController;
    [SerializeField] private int probability;

    private float? distance1;
    private float? distance2;

    private int randomNumber;

    private bool moreSpawn = true;

    private SpawnSpike scriptForCollisionDetection;

    private Coroutine checkingDistance;

    public GameObject obs;

    private void Awake()
    {
        //playerMovementController = GameObject.Find("try").GetComponent<PlayerMovementController>();
        playerMovementController = FindObjectOfType<PlayerMovementController>();
    }

    // Start is called before the first frame update
    void Start()
    {
      scriptForCollisionDetection = GetComponent<SpawnSpike>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distance1 > distance2 && moreSpawn == true)
        {
            moreSpawn = false;
            int number = RandomNumberGenerator();
           // Debug.Log(number);
            if (number < probability)
            {
                obs = Instantiate(spike, transform.position, Quaternion.identity);
                obs.transform.SetParent(transform, true);
            }
        }

        else if ((distance1 + 0.038f) < distance2 && moreSpawn == false)
        {
            Destroy(obs);
            moreSpawn = true; 
        }

    }

    private int RandomNumberGenerator()
    {
        int randomNumber = Random.Range(0, 101);
        return randomNumber;
        
    }

    public void CoroutineStart()
    {
      checkingDistance =  StartCoroutine(DistanceCheck(0.01f));
    }

    public void CoroutineStop()
    {
        StopCoroutine(checkingDistance);
    }

    IEnumerator DistanceCheck(float waitTime)
    {
        while (true)
        {
            distance1 = null;
            distance2 = null;   
            distance1 = Mathf.Abs(transform.position.x - playerMovementController.transform.position.x);
            //Debug.Log("Distance 1= " + distance1);
            yield return new WaitForSeconds(waitTime);
            distance2 = Mathf.Abs(transform.position.x - playerMovementController.transform.position.x);
            //Debug.Log("Distance 2= " + distance2);
            yield return new WaitForSeconds(waitTime);
        }

    }
}
