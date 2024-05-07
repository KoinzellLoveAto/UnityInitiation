using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //gère la logique des bouttons défini dans le menu.
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
