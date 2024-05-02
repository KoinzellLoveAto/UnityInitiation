using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageInteraction : MonoBehaviour
{
    public Window parent;


    public void ManageInteractionDestruction()
    {
        parent.DropCurtain();
    }
}
