using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGB : MonoBehaviour
{
    // On fait une variable de notre controlle map
    ControlMapGrpB controlMap;
    
    //Ces variables sont a set sur Unity, on donne les bonne référence pour la bonne exécution de ce script
    public SpawnerItem spawner;
    private Rigidbody rb;
    public Animator animator;
    public Transform CameraPivotTransform;
    public GameObject CharacterMesh;

    // variables pour pour nos déplacement.
    public float MaxVelocity;
    public float rotationPlayerSpeed;
    public float speed;
    public float cameraSensibility;
    public Vector2 minMaxAngleCam;
    public float rotationSpeed;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        // on crée notre controlMap
        controlMap = new ControlMapGrpB();

        // on récupère automatiquement le rigidbody placé sur le même objet que ce script.
        rb = GetComponent<Rigidbody>();

        // on check si le rigid body a été trouvé.
        if (rb != null)
        {
            Debug.Log("RigidBody Found !");
        }
        else
        {
            Debug.Log("RigidBody not found !");
        }

        //on cache et lock notre cursor dans l'écran
        Cursor.lockState = CursorLockMode.Locked;



        //On active les input de la map "Player"
        controlMap.Player.Enable();

        //On s'abonne au evements qui nous interesse
        controlMap.Player.Fire.performed += HandleFire;
    }

    //Fonction appeler quand on l'evement fire de l'input map est déclenché
    private void HandleFire(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Player fire");

        //on communique avec un autre script qui fait spawn un objet, et on renseigne le temps passé appuyer en paramettre.
        spawner.SpawnItem();
        
        // on communique avec l'animateur pour jouer l'animation de mort.
        animator.SetBool("IsDeath", true);
    }

    // Update is called once per frame
    void Update()
    {

        //on lis les valeur de notre vector deux de movement définit dans la controlMap
        moveInput = controlMap.Player.Move.ReadValue<Vector2>();


        //on lis la valeur enrengistré par le control map(cette valeur control la souris) 
        Vector2 lookInput = controlMap.Player.LookInput.ReadValue<Vector2>();

        // Normaliser l'angle d'Euler dans la plage de -180 à 180 degrés
        float angleX = CameraPivotTransform.rotation.eulerAngles.x + lookInput.y * cameraSensibility * Time.deltaTime;
        angleX = NormalizeAngle(angleX);

        //On calcul la nouvelle rotation du cameraPivot, en clampant la valeur (entre tant et tant, et ne peux pas dépassé cette valeur)
        // Ici on l'utilise pour que notre camera ne puisse pas aller a 180° au dessus du personnage, et en dessous, on limite l'angle entre le minimal et maximal défini
        CameraPivotTransform.rotation = Quaternion.Euler(
    Mathf.Clamp(angleX, minMaxAngleCam.x, minMaxAngleCam.y),
    CameraPivotTransform.rotation.eulerAngles.y + lookInput.x * cameraSensibility * Time.deltaTime,
    0);


        // Si on appuis sur aucun boutons lié au déplacement
        if (moveInput != Vector2.zero)
        {
            //on récupère le vector directeur de la vélocity (on ignore Y) NORMALISER
            Vector3 movedir = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized;

            //On récupère la rotation qui "look" vers cette direction
            Quaternion targetRotation = Quaternion.LookRotation(movedir);

            //On rotate le mesh du personnage progressivement vers sa rotation souhaiter
            CharacterMesh.transform.rotation = Quaternion.RotateTowards(CharacterMesh.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
      

    }

    public void FixedUpdate()
    {
        // on récupère le vector forward(la ou on regarde) de la cam, on set le Y a 0 (car cette composante nous interesse pas)
        // et on le normalize (l'ensemble de ses composantes = 1)
        Vector3 camForward = CameraPivotTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        // on récupère le vector forward(a droite d'ou on regarde) de la cam, on set le Y a 0 (car cette composante nous interesse pas)
        // et on le normalize (l'ensemble de ses composantes = 1)
        Vector3 camRight = CameraPivotTransform.right;
        camRight.y = 0;
        camRight.Normalize();


        //on intéragit avec notre rigid body, qui gère la physique pour ajouter une force dans la direction calculé, avec la "speed" souhaité
        rb.AddForce(camForward * moveInput.y * speed +
            camRight * moveInput.x * speed);

        float currentVelocity = Mathf.Abs(rb.velocity.magnitude);//on récupère la vitesse, en prenant la valeur absolue du la magnétude du vecteur de velocity

        // si elle est supérieur à celle autoriser, alors on la définit au max indiqué dans nos variables
        if (currentVelocity > MaxVelocity)
        {
            Vector3 dirVelocity = rb.velocity.normalized;
            rb.velocity = dirVelocity * MaxVelocity;
        }


        // la fonction remap permet de passer un valeur situé entre une certaine borne, et en gardant le même coeff de propotionalité dans une autre borne, ici 0 maxvelocity TO 0 1
        //  0 1 car on souhaite changer la valeur de l'animateur, et la valeur max dans ce dernier qu'on s'est autorisé etais 1
        float remapValue = Remap(currentVelocity, 0, MaxVelocity, 0, 1);
        animator.SetFloat("Velocity", remapValue);


    }

    // on normalise un angle pour les calcul de rotation (devrai être dans un script tools)
    float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            return angle - 360f;
        else if (angle < -180f)
            return angle + 360f;
        else
            return angle;
    }

    // on remap une valeur entre 2 borne dans une autre plage de donnée (2 borne également), on garde le coeff de proportionalité
    // devrais être aussi dans un script tools.
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
