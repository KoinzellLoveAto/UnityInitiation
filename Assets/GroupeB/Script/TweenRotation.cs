using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : MonoBehaviour
{
    public float speed;

    public bool XAxis , YAxis, ZAxis;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float newX=0, newY=0, newZ =0;
        if (XAxis)
        {
            newX = transform.rotation.eulerAngles.x + speed * Time.deltaTime;
        }
        if (YAxis)
        {
            newY = transform.rotation.eulerAngles.y + speed * Time.deltaTime;
        }
        if (ZAxis)
        {
            newZ = transform.rotation.eulerAngles.z + speed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(new Vector3(newX, newY, newZ));

    }
}
