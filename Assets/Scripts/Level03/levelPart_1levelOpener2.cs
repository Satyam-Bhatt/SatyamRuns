using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPart_1levelOpener2 : MonoBehaviour
{
    [SerializeField] GameObject oldPlatform;
    [SerializeField] GameObject newPlatform;
    // Start is called before the first frame update
    void Start()
    {
        newPlatform.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            oldPlatform.SetActive(false);
            newPlatform.SetActive(true);
            Destroy(gameObject);
        }
    }
}
