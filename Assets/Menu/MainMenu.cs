using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //g�re la logique des bouttons d�fini dans le menu.
    public void HandleClickButtonGroupA()
    {
        Debug.Log("Click sur groupe A");
        SceneManager.LoadScene(1);
    }
    public void HandleClickButtonGroupB()
    {
        Debug.Log("Click sur groupe B");
        SceneManager.LoadScene(2);


    }


}
