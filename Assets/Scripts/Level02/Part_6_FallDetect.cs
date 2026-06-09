using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_6_FallDetect : MonoBehaviour
{
    [SerializeField] GameObject tilesToAppear;
    // Start is called before the first frame update
    void Awake()
    {
        tilesToAppear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            tilesToAppear.SetActive(true);
        }
    }
}
