using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    private Rigidbody rb;

    public void Initialize()
    {
        rb ??= GetComponent<Rigidbody>();
    }

    public void Drop()
    {
        rb.isKinematic = false;
    }
}
