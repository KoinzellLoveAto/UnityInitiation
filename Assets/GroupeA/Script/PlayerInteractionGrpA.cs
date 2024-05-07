using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionGrpA : MonoBehaviour
{

    // méthode qui gere l'evenement quand 2 objet avec un collider se rentre dedans
    // note : le notre, de cet objet, est un trigger donc ontriggerenter
    private void OnTriggerEnter(Collider other)
    {
        //on vérifie si c'est un objet avec le tag "window"
        if (other.gameObject.CompareTag("Window"))
        {
            //on essais de récupéré le script sur l'objet qu'on a collide
            ManageInteraction interaction = other.GetComponent<ManageInteraction>();
            
            //si ce script est null on quitte cette fonction
            if (interaction == null) return;


            // si le script n'est pas nulle, on execute la logique
            interaction.ManageInteractionDestruction();
            
        }
    }
}
