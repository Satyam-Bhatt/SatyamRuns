using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPart_7_Open3 : MonoBehaviour
{
    [SerializeField] private List<GameObject> toAppear = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < toAppear.Count; i++)
        {
            toAppear[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            for (int i = 0; i < toAppear.Count; i++)
            {
                toAppear[i].SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
