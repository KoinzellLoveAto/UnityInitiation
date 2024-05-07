using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    //varaible ref unity
    public Curtain curtain;

    public void Awake()
    {
        //initialize les script au awake
        curtain.Initialize();
    }


    public void DropCurtain()
    {
        curtain.Drop();
    }

}
