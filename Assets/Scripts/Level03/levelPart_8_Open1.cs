using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPart_8_Open1 : MonoBehaviour
{
    [SerializeField] private List<GameObject> toDisable = new List<GameObject>();
    [SerializeField] private List<GameObject> toEnable = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < toEnable.Count; i++)
        {
            toEnable[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            for (int i = 0; i < toDisable.Count; i++)
            {
                toDisable[i].SetActive(false);
            }
            for (int i = 0; i < toEnable.Count; i++)
            {
                toEnable[i].SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
