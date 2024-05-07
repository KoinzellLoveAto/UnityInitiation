using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceLineObjet : MonoBehaviour
{
    public Transform from;
    public Transform transformCam;
    public float distanceInteraction = 20;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(from.position, transformCam.forward, out hit, distanceInteraction))
        {
            Debug.Log(hit.transform.gameObject.name);
            Debug.DrawLine(from.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(from.position, transformCam.forward * distanceInteraction, Color.red);
        }
    }
}
