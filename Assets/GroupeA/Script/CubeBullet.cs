using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBullet : MonoBehaviour
{
    public Rigidbody rb;

    public void InitWithForce(Vector3 dir, float strength)
    {
        rb.AddForce(dir * strength, ForceMode.Impulse);
    }
}
