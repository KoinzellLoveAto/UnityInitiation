using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionItem : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            CubeCollider col = other.GetComponent<CubeCollider>();
            if (col != null)
            {
                Destroy(col.Parent);
            }
        }
    }
}
