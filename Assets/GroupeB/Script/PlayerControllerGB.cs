using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGB : MonoBehaviour
{

    ControlMapGrpB controlMap;

    public Transform CameraPivotTransform;
    public float speed;
    public float cameraSensibility;
    public Vector2 minMaxAngleCam;
    // Start is called before the first frame update
    void Start()
    {
        controlMap = new ControlMapGrpB();

        Cursor.lockState = CursorLockMode.Locked;

        controlMap.Player.Enable();
        controlMap.Player.Fire.performed += HandleFire;
    }

    private void HandleFire(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Player fire");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = controlMap.Player.Move.ReadValue<Vector2>();

        Vector3 camForward = CameraPivotTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = CameraPivotTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        transform.position = transform.position +
            (camForward * moveInput.y * speed * Time.deltaTime +
            camRight * moveInput.x * speed * Time.deltaTime);



        Vector2 lookInput = controlMap.Player.LookInput.ReadValue<Vector2>();

        float angleX = CameraPivotTransform.rotation.eulerAngles.x + lookInput.y * cameraSensibility * Time.deltaTime;
        angleX = NormalizeAngle(angleX);

        CameraPivotTransform.rotation = Quaternion.Euler(
    Mathf.Clamp(angleX, minMaxAngleCam.x, minMaxAngleCam.y),
    CameraPivotTransform.rotation.eulerAngles.y + lookInput.x * cameraSensibility * Time.deltaTime,
    0);



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
