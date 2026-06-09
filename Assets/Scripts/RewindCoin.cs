using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindCoin : MonoBehaviour
{
    private Transform transform;

    public Vector3 position;
    public Quaternion rotation;
    // Start is called before the first frame update

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
    void Start()
    {
        position = transform.position;   
        rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
