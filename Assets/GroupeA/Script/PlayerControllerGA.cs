using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGA : MonoBehaviour
{

    public Transform transformCamPivot;

    ControlMapGrpA controlMap;

    public float speed;
    public float camSensitive;

    public Vector2 clampAngle;
    public float minAngle;
    public float maxAngle;


    // Start is called before the first frame update
    void Awake()
    {
        controlMap = new ControlMapGrpA();
        controlMap.Player.Enable();
        controlMap.Player.Fire.performed += Fire_started;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Fire_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        obj.ReadValueAsButton();
        Debug.Log("Player shoot");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = controlMap.Player.Move.ReadValue<Vector2>();

        Vector3 forwardCam = this.transformCamPivot.forward;
        forwardCam.y = 0;
        forwardCam.Normalize();

        Vector3 RightCam = this.transformCamPivot.right;
        RightCam.y = 0;
        RightCam.Normalize();


        transform.position = transform.position + (forwardCam * moveInput.y * speed * Time.deltaTime + RightCam * moveInput.x * speed * Time.deltaTime);

        Vector2 lookInput = controlMap.Player.LookInput.ReadValue<Vector2>();

        // Normaliser l'angle d'Euler dans la plage de -180 à 180 degrés
        float xCamrotValue = transformCamPivot.rotation.eulerAngles.x + lookInput.y * camSensitive * Time.deltaTime;
        xCamrotValue = NormalizeAngle(xCamrotValue);


        Quaternion camRotation = Quaternion.Euler(
            Mathf.Clamp(xCamrotValue, minAngle, maxAngle),
     transformCamPivot.rotation.eulerAngles.y + lookInput.x * camSensitive * Time.deltaTime,
     0f) ;

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
