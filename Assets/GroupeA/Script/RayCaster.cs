using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public Transform origine;
    public Transform CamTransform;
    public float distance;


    public void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(origine.position, CamTransform.forward, out hit, distance))
        {
            Debug.DrawLine(origine.position, CamTransform.forward * distance, Color.red) ;
            Debug.Log(hit.transform.gameObject.name);
        }
        else
        {
            Debug.DrawLine(origine.position, CamTransform.forward * distance, Color.yellow);

        }
    }
}
