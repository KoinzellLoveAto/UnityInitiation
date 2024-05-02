using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionGrpA : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Window"))
        {
            ManageInteraction interaction = other.GetComponent<ManageInteraction>();
            
            if (interaction == null) return;

            interaction.ManageInteractionDestruction();
            
        }
    }
}
