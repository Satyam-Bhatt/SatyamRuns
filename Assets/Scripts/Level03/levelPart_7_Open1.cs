using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class levelPart_7_Open1 : MonoBehaviour
{
    [SerializeField] private GameObject setFalse;
    [SerializeField] private List<GameObject> openLevel = new List<GameObject>();

    private bool leftClickEnabled = false;

    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < openLevel.Count; i++)
        {
            openLevel[i].SetActive(false);
        }

        setFalse.SetActive(false);
        mesh = GetComponent<MeshRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            for (int i = 0; i < openLevel.Count; i++)
            {
                openLevel[i].SetActive(true);
            }

            mesh.enabled = false;

        }

    }
}
