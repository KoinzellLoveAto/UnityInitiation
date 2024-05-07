using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class MultiTestController : MonoBehaviour
{
    private TestMulti controlmap;

    private bool multiComboDetected = false;

    private void Start()
    {
        controlmap = new TestMulti();

        controlmap.Player.Enable();
        controlmap.Player.A.canceled += A_canceled;
        controlmap.Player.B.canceled += B_canceled;

    }

    private void A_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (controlmap.Player.B.IsPressed())
        {
            Debug.Log("A & B");
            multiComboDetected = true;
        }
        else
        {
            if (!multiComboDetected)
            {
                Debug.Log("A");

            }
            else
            {
                multiComboDetected = false ;
            }

        }
    }

    private void B_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (controlmap.Player.A.IsPressed())
        {
            Debug.Log("A & B");
            multiComboDetected = true;

        }
        else
        {
            if (!multiComboDetected)
            {
                Debug.Log("B");

            }
            else
            {
                multiComboDetected = false;
            }

        }
    }
}

