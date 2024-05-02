using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public Curtain curtain;

    public void Awake()
    {
        curtain.Initialize();
    }


    public void DropCurtain()
    {
        curtain.Drop();
    }

}