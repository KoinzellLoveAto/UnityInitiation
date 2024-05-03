using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGB : MonoBehaviour
{

    ControlMapGrpB controlMap;
    public SpawnerItem spawner;
    private Rigidbody rb;
    public Animator animator;

    public float MaxVelocity;
    public float rotationPlayerSpeed;
    public GameObject CharacterMesh;

    public Transform CameraPivotTransform;
    public float speed;
    public float cameraSensibility;
    public Vector2 minMaxAngleCam;
    // Start is called before the first frame update
    void Start()
    {
        controlMap = new ControlMapGrpB();
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("RigidBody Found !");
        }
        else
        {
            Debug.Log("RigidBody not found !");
        }

        Cursor.lockState = CursorLockMode.Locked;

        controlMap.Player.Enable();
        controlMap.Player.Fire.performed += HandleFire;
    }

    private void HandleFire(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Player fire");
        spawner.SpawnItem();
        animator.SetBool("IsDeath", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = controlMap.Player.LookInput.ReadValue<Vector2>();

        float angleX = CameraPivotTransform.rotation.eulerAngles.x + lookInput.y * cameraSensibility * Time.deltaTime;
        angleX = NormalizeAngle(angleX);

        CameraPivotTransform.rotation = Quaternion.Euler(
    Mathf.Clamp(angleX, minMaxAngleCam.x, minMaxAngleCam.y),
    CameraPivotTransform.rotation.eulerAngles.y + lookInput.x * cameraSensibility * Time.deltaTime,
    0);

        Vector3 veloDir = rb.velocity.normalized;
        veloDir.Normalize();

        CharacterMesh.transform.rotation = Quaternion.Euler(0, CameraPivotTransform.rotation.y, 0);

    }

    public void FixedUpdate()
    {
        Vector2 moveInput = controlMap.Player.Move.ReadValue<Vector2>();

        Vector3 camForward = CameraPivotTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = CameraPivotTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        rb.AddForce(camForward * moveInput.y * speed +
            camRight * moveInput.x * speed);

        float currentVelocity = Mathf.Abs(rb.velocity.magnitude);

        if (currentVelocity > MaxVelocity)
        {
            Vector3 dirVelocity = rb.velocity.normalized;
            rb.velocity = dirVelocity * MaxVelocity;
        }


        if (currentVelocity > MaxVelocity)
        {
            Vector3 dirVelocity = rb.velocity.normalized;
            rb.velocity = dirVelocity * MaxVelocity;
        }


        float remapValue = Remap(currentVelocity, 0, MaxVelocity, 0, 1);
        animator.SetFloat("Velocity", remapValue);


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

    float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Assurer que la valeur est dans la plage d'origine
        value = Mathf.Clamp(value, fromMin, fromMax);

        // Calculer le pourcentage de la valeur dans la plage d'origine
        float normalizedValue = (value - fromMin) / (fromMax - fromMin);

        // Remapper la valeur dans la plage de destination
        float remappedValue = (normalizedValue * (toMax - toMin)) + toMin;

        return remappedValue;
    }
}
