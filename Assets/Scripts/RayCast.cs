using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    private Ray myRay;
    public RaycastHit myHit;
    public float hitDistance;

    [SerializeField] private LayerMask platforms;

    private Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(0.1f, 1.6f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        myRay = new Ray (transform.position, Vector3.forward);
       // Debug.DrawRay(transform.position, transform.forward * 100f);

       // if(Physics.Raycast (myRay, out myHit, 100f, platforms))
      //  {
      //      Debug.Log("I hit it ");
      //  }

       // if(Physics.SphereCast(transform.position, 1f, transform.forward, out myHit, 4f, platforms))
       if(Physics.BoxCast(transform.position, scale/2, transform.forward, out myHit, Quaternion.identity, 5f, platforms))
        {
          //  Debug.Log("Hit Again");
            hitDistance = myHit.distance;
        }
        else
        {
            hitDistance = 5f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * hitDistance);
        //Gizmos.DrawWireSphere(transform.position + transform.forward * hitDistance, 1f);
        Gizmos.DrawWireCube(transform.position + transform.forward * hitDistance, scale);
    }

    public bool IsHit()
    {
        return Physics.BoxCast(transform.position, scale / 2, transform.forward, out myHit, Quaternion.identity, 5f, platforms);
    }
}
