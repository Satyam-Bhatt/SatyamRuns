using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreForSpikeRandom : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [SerializeField] private float radius = 5f;
    [SerializeField] private bool TurnOnRadius = false;

    public bool allSpawnFromOtherScript = false;
    public bool destroyAllSpawns = false;

    private bool respawn;

    [SerializeField] private SpawnSpike spawnSpike;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, radius, player)  && spawnSpike.allSpawn && respawn)
        {
            allSpawnFromOtherScript = true;
            respawn = false;
           // Debug.Log("I baj");
        }
        else if(!Physics.CheckSphere(transform.position, radius, player) && spawnSpike.allSpawn)
        {
            destroyAllSpawns = true;
            respawn = true;
        }
       
    }
    private void OnDrawGizmos()
    {
        if (TurnOnRadius)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
