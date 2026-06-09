using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPart_1opener1 : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    // Start is called before the first frame update
    void Start()
    {
        platform.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            platform.SetActive(true);
            Destroy(gameObject);
        }
    }
}
