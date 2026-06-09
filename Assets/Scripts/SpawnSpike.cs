using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnSpike : MonoBehaviour
{
    [SerializeField] private GameObject spike;
    public GameObject[] pointsToSpawn;
    [SerializeField] private float waitTime;
    private PlayerMovementController playerMovementController;
    [SerializeField] private bool multipleSpawning = false;
    Transform my;

    Coroutine myCoroutine = null;

    private GameObject obs;
    private GameObject obs2;

    private GameObject spikeAllSpawn;

    private float distance1;
    private float distance2;

    [SerializeField] private bool shouldSpawn;

    public bool allSpawn = false;

    private int index;
    private int storedIndex;

    private Vector3 posiForSpawn2;

    private List<GameObject> spawnedSpikes = new List<GameObject>();

    [SerializeField] private CentreForSpikeRandom centreForSpikeRandom;

    private void Awake()
    {
        //playerMovementController = GameObject.Find("try").GetComponent<PlayerMovementController>();
        playerMovementController = FindObjectOfType<PlayerMovementController>();
        //centreForSpikeRandom = GetComponent<CentreForSpikeRandom>();
    }
    void Start()
    {
        my= GetComponent<Transform>();
        
        // StartCoroutine(DistanceCheck(2f));
    }

    // Update is called once per frame
    private void Update()
    {
        if (centreForSpikeRandom.allSpawnFromOtherScript == true)
        {

            // Instantiate(spike, pointsToSpawn[0].transform.position, Quaternion.identity);

            for (int i = 0; i < pointsToSpawn.Length; i++)
            {
                spikeAllSpawn = Instantiate(spike, pointsToSpawn[i].transform.position, Quaternion.identity);
                spikeAllSpawn.transform.SetParent(transform, true);
                spawnedSpikes.Add(spikeAllSpawn);
            }
            centreForSpikeRandom.allSpawnFromOtherScript = false;
        }

        if (centreForSpikeRandom.destroyAllSpawns)
        {
            for (int j = 0; j < spawnedSpikes.Count; j++)
            {
                Destroy(spawnedSpikes[j]);
            }
            centreForSpikeRandom.destroyAllSpawns = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (multipleSpawning == true && allSpawn == false)
        {
            int secondIndex;          

            index = Random.Range(0, pointsToSpawn.Length);
            storedIndex = index;
            Vector3 posiForSpawn = pointsToSpawn[index].transform.position;

            if(pointsToSpawn.Length > 3 && shouldSpawn)
            {
                secondIndex = Random.Range(0, pointsToSpawn.Length);
                
                while(secondIndex == index)
                {
                    secondIndex = Random.Range(0, pointsToSpawn.Length);
                }
                posiForSpawn2 = pointsToSpawn[secondIndex].transform.position;
            }

            if (other.gameObject.layer == 6)
            {
                obs = Instantiate(spike, posiForSpawn, Quaternion.identity);
                obs2 = Instantiate(spike, posiForSpawn2, Quaternion.identity);
                
                myCoroutine = StartCoroutine(SpikeSpawnAndDestroy());
                // Destroy(obs, 1f);
                // Debug.Log("cool");
            }
        }

        else if(multipleSpawning == false && allSpawn == false)
        {
            if (other.gameObject.layer == 6)
            {
               foreach(var points in pointsToSpawn)
                {
                   // points.GetComponent<WaypointSpike>().InstantiateObject();
                    points.GetComponent<WaypointSpike>().CoroutineStart();
                }          
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (multipleSpawning == true && allSpawn == false)
        {
            if (other.gameObject.layer == 6)
            {
                StopCoroutine(myCoroutine);
                Destroy(obs);
                Destroy(obs2);
            }
        }

        else if (multipleSpawning == false && allSpawn == false)
        {
            if (other.gameObject.layer == 6)
            {
                foreach (var points in pointsToSpawn)
                {
                    points.GetComponent<WaypointSpike>().CoroutineStop();
                    Destroy(points.GetComponent<WaypointSpike>().obs);
                }
            }
        }
    }

    IEnumerator SpikeSpawnAndDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(obs);
            Destroy(obs2);
            int secondIndex;
            index = Random.Range(0, pointsToSpawn.Length);

            while(index == storedIndex)
            {
                index = Random.Range(0, pointsToSpawn.Length);
            }
            storedIndex = index;
            Vector3 posiForSpawn = pointsToSpawn[index].transform.position;
            obs = Instantiate(spike, posiForSpawn, Quaternion.identity);

            /*            if(index != storedIndex)
                        {
                            Vector3 posiForSpawn = pointsToSpawn[index].transform.position;
                            obs = Instantiate(spike, posiForSpawn, Quaternion.identity);
                            storedIndex = index;
                        }
                        else if (index == storedIndex)
                        {
                            while (index == storedIndex)
                            {
                                index = Random.Range (0, pointsToSpawn.Length);
                            }
                            storedIndex = index;
                            Vector3 posiForSpawn = pointsToSpawn[index].transform.position;
                            obs = Instantiate(spike, posiForSpawn, Quaternion.identity);
                        }*/

            if (pointsToSpawn.Length > 3 && shouldSpawn)
            {
                secondIndex = Random.Range(0, pointsToSpawn.Length);

                while (secondIndex == index)
                {
                    secondIndex = Random.Range(0, pointsToSpawn.Length);
                }
                posiForSpawn2 = pointsToSpawn[secondIndex].transform.position;
            }

            obs2 = Instantiate(spike, posiForSpawn2, Quaternion.identity);

        } 
    }
}
