
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float speed;

    // reference sur unity
    public Transform posfrom;
    public Transform posTo;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = posfrom.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, posTo.position, speed); 
    }

   
}
