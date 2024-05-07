using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionItem : MonoBehaviour
{
    // m�thode qui gere l'evenement quand 2 objet avec un collider se rentre dedans
    // note : le notre, de cet objet, est un trigger donc ontriggerenter
    private void OnTriggerEnter(Collider other)
    {
        //on v�rifie si c'est un objet avec le tag "Item"
        if (other.gameObject.CompareTag("Item"))
        {
            //on essais de r�cup�r� le script sur l'objet qu'on a collide
            CubeCollider col = other.GetComponent<CubeCollider>();
            if (col != null)
            {
                // si le script n'est pas nulle, on execute la logique
                Destroy(col.Parent);
            }
        }
    }
}
