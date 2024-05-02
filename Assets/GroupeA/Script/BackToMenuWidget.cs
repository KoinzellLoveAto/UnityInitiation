using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuWidget: MonoBehaviour
{
    
    public void HandleBackToMenu()
    {
        print("Click back to menu");
        SceneManager.LoadScene(0);
    }
}
