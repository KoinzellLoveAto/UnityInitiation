using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerGA : MonoBehaviour
{
    public PlayerSpawnBall spawnBall;

    public Transform transformCamPivot;
    public Rigidbody rb;

    ControlMapGrpA controlMap;

    public float maxVelocity;

    public float speed;
    public float camSensitive;

    public Vector2 clampAngle;
    public float minAngle;
    public float maxAngle;

    private bool IsGamePause = false;

    // Start is called before the first frame update
    void Awake()
    {
        controlMap = new ControlMapGrpA();
        controlMap.Player.Enable();
        controlMap.Player.Fire.canceled += Fire_Canceled;
        controlMap.Player.Pause.performed += HandlePause;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandlePause(InputAction.CallbackContext obj)
    {
        if (IsGamePause)
        {
            IsGamePause = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

        }
        else
        {
            IsGamePause = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = .05f;
        }

    }

    public void Fire_Canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        spawnBall.SpawnPrefab(timeFirePress);
        fireIsDown = false;
        timeFirePress = 0;
    }

    public float timeFirePress = 0;
    public bool fireIsDown = false;
    // Update is called once per frame
    void Update()
    {
        if (controlMap.Player.Fire.IsPressed())
        {
            timeFirePress += Time.deltaTime;
            if (timeFirePress >= 2)
            {
                timeFirePress = 2;
            }
            fireIsDown = true;

        }


        Vector2 moveInput = controlMap.Player.Move.ReadValue<Vector2>();

        Vector3 forwardCam = this.transformCamPivot.forward;
        forwardCam.y = 0;
        forwardCam.Normalize();

        Vector3 RightCam = this.transformCamPivot.right;
        RightCam.y = 0;
        RightCam.Normalize();

        rb.AddForce(forwardCam * moveInput.y * speed + RightCam * moveInput.x * speed);

        if (MathF.Abs(rb.velocity.magnitude) > maxVelocity)
        {
            Vector3 dirVelo = rb.velocity.normalized;
            rb.velocity = dirVelo * (maxVelocity);
        }


        Vector2 lookInput = controlMap.Player.LookInput.ReadValue<Vector2>();

        // Normaliser l'angle d'Euler dans la plage de -180 à 180 degrés
        float xCamrotValue = transformCamPivot.rotation.eulerAngles.x + lookInput.y * camSensitive * Time.deltaTime;
        xCamrotValue = NormalizeAngle(xCamrotValue);


        Quaternion camRotation = Quaternion.Euler(
            Mathf.Clamp(xCamrotValue, minAngle, maxAngle),
     transformCamPivot.rotation.eulerAngles.y + lookInput.x * camSensitive * Time.deltaTime,
     0f);

        transformCamPivot.rotation = camRotation;


    }

    void FixedUpdate()
    {

    }
    float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            return angle - 360f;
        else if (angle < -180f)
            return angle + 360f;
        else
            return angle;
    }
}
