using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPart7_Open2 : MonoBehaviour
{
    [SerializeField] private GameObject setTrue;
    [SerializeField] private List<GameObject> setFalse = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            for(int i = 0; i < setFalse.Count; i++)
            {
                setFalse[i].SetActive(false);
            }
            setTrue.SetActive(true);
            Destroy(gameObject);
        }
    }
}
