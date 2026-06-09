using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Respawn respawn;

    // Start is called before the first frame update
    void Awake()
    {
        respawn = FindObjectOfType<Respawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            respawn.posi = transform.position;
        }
    }
}
