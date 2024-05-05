using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    private Rigidbody rb;

    public void Initialize()
    {
        // maniere de get automatiquement une component AUTOMATIQUEMENT sur le MËME GAMEOBJECT que le script
        rb ??= GetComponent<Rigidbody>();
    }

    
    public void Drop()
    {
        //iskinematic = "freeze tout movement" relatif a la physique


        rb.isKinematic = false;// on remet les movement physique pour faire la chute du rideau
    }
}
