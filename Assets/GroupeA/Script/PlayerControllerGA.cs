using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class PlayerControllerGA : MonoBehaviour
{

    //Ces variables sont a set sur Unity, on donne les bonne référence pour la bonne exécution de ce script
    public PlayerSpawnBall spawnBall;
    public Animator animator;
    public Transform transformCamPivot;
    public Rigidbody rb;
    public GameObject CharacterMesh;

    // On fait une variable de notre controlle map
    ControlMapGrpA controlMap;


    // variables pour pour nos déplacement.
    public float maxVelocity;
    public float speed;
    public float camSensitive;
    public float minAngle;
    public float maxAngle;
    public float rotationSpeed;

    private bool IsGamePause = false;


    // variable de gestion de "on reste appuyer sur un touche pour tiré"
    public float timeFirePress = 0;
    public bool fireIsDown = false;

    // Start is called before the first frame update
    void Awake()
    {
        // on crée notre controlMap
        controlMap = new ControlMapGrpA();

        //On active les input de la map "Player"
        controlMap.Player.Enable();

        //On s'abonne au evements qui nous interesse
        controlMap.Player.Fire.canceled += Fire_Canceled;
        controlMap.Player.Pause.performed += HandlePause;

        //on cache et lock notre cursor dans l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }


    //Fonction appellé quand l'evement pause est détecté.
    private void HandlePause(InputAction.CallbackContext obj)
    {
        if (IsGamePause)
        {
            IsGamePause = false;
            Cursor.lockState = CursorLockMode.Locked; // on cache le curseur
            Time.timeScale = 1; //on met normal la vitesse  d'excution du jeu

        }
        else
        {
            IsGamePause = true;
            Cursor.lockState = CursorLockMode.Confined; // on montre le cursor dans l'écran.
            Time.timeScale = .05f; // on multiplie  la vitesse d'execution du jeu par 0.05.
        }

    }

    //Fonction appeler quand on l'evement fire de l'input map est déclenché
    public void Fire_Canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // on communique avec l'animateur pour jouer l'animation de mort.
        animator.SetBool("IsDeath", true);

        //on communique avec un autre script qui fait spawn un objet, et on renseigne le temps passé appuyer en paramettre.
        spawnBall.SpawnPrefab(timeFirePress);


        //on reset nos variables
        fireIsDown = false;
        timeFirePress = 0;
    }


    // Update is called once per frame
    void Update()
    {
        //on check toute les frame si on appui sur le boutton
        if (controlMap.Player.Fire.IsPressed())
        {
            timeFirePress += Time.deltaTime; // c'est une manière de faire un timer avec le time.delaTime.
            if (timeFirePress >= 2)
            {
                timeFirePress = 2;
            }
            fireIsDown = true;

        }

        //on lis les valeur de notre vector deux de movement définit dans la controlMap
        Vector2 moveInput = controlMap.Player.Move.ReadValue<Vector2>();


        // on récupère le vector forward(la ou on regarde) de la cam, on set le Y a 0 (car cette composante nous interesse pas)
        // et on le normalize (l'ensemble de ses composantes = 1)
        Vector3 forwardCam = this.transformCamPivot.forward;
        forwardCam.y = 0;
        forwardCam.Normalize();

        // on récupère le vector forward(a droite d'ou on regarde) de la cam, on set le Y a 0 (car cette composante nous interesse pas)
        // et on le normalize (l'ensemble de ses composantes = 1)
        Vector3 RightCam = this.transformCamPivot.right;
        RightCam.y = 0;
        RightCam.Normalize();

        //on intéragit avec notre rigid body, qui gère la physique pour ajouter une force dans la direction calculé, avec la "speed" souhaité
        rb.AddForce(forwardCam * moveInput.y * speed + RightCam * moveInput.x * speed);

        // Si on appuis sur aucun boutons lié au déplacement
        if(moveInput!=Vector2.zero)
        {
            //on récupère le vector directeur de la vélocity (on ignore Y) NORMALISER
            Vector3 movedir = new Vector3(rb.velocity.x,0,rb.velocity.z).normalized;

            //On récupère la rotation qui "look" vers cette direction
            Quaternion targetRotation = Quaternion.LookRotation(movedir);

            //On rotate le mesh du personnage progressivement vers sa rotation souhaiter
            CharacterMesh.transform.rotation = Quaternion.RotateTowards(CharacterMesh.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        float currentVelocity = MathF.Abs(rb.velocity.magnitude); //on récupère la vitesse, en prenant la valeur absolue du la magnétude du vecteur de velocity

        // si elle est supérieur à celle autoriser, alors on la définit au max indiqué dans nos variables
        if (currentVelocity > maxVelocity)
        {
            Vector3 dirVelo = rb.velocity.normalized;
            rb.velocity = dirVelo * (maxVelocity);
        }

        // la fonction remap permet de passer un valeur situé entre une certaine borne, et en gardant le même coeff de propotionalité dans une autre borne, ici 0 maxvelocity TO 0 1
        //  0 1 car on souhaite changer la valeur de l'animateur, et la valeur max dans ce dernier qu'on s'est autorisé etais 1
        float mappedValue = Remap(currentVelocity, 0, maxVelocity, 0, 1);
        animator.SetFloat("Velocity", mappedValue);


        //on lis la valeur enrengistré par le control map(cette valeur control la souris) 
        Vector2 lookInput = controlMap.Player.LookInput.ReadValue<Vector2>();

        // Normaliser l'angle d'Euler dans la plage de -180 à 180 degrés
        float xCamrotValue = transformCamPivot.rotation.eulerAngles.x + lookInput.y * camSensitive * Time.deltaTime;
        xCamrotValue = NormalizeAngle(xCamrotValue);


        //On calcul la nouvelle rotation du cameraPivot, en clampant la valeur (entre tant et tant, et ne peux pas dépassé cette valeur)
        // Ici on l'utilise pour que notre camera ne puisse pas aller a 180° au dessus du personnage, et en dessous, on limite l'angle entre le minimal et maximal défini
        Quaternion camRotation = Quaternion.Euler(
            Mathf.Clamp(xCamrotValue, minAngle, maxAngle),
     transformCamPivot.rotation.eulerAngles.y + lookInput.x * camSensitive * Time.deltaTime,
     0f);


        //on applique la rotation calculé
        transformCamPivot.rotation = camRotation;




    }

    void FixedUpdate()
    {

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
