using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBullet : MonoBehaviour
{
    //variable ref sur unity ()drag & drop)
    public Rigidbody rb;

    public void InitWithForce(Vector3 dir, float strength)
    {
        // on donne une direction, avec la force, et le rigid body va gere le reste.
        rb.AddForce(dir * strength, ForceMode.Impulse);
    }
}
