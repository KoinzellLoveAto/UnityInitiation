using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Trouver la cam�ra principale dans la sc�ne
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Pas de cam�ra principale trouv�e dans la sc�ne !");
        }
    }

    void Update()
    {
        // S'assurer que la cam�ra est valide
        if (mainCamera != null)
        {
            // Obtenir la direction vers la cam�ra
            Vector3 cameraDirection =  transform.position - mainCamera.transform.position;

            // Tourner le billboard pour faire face � la cam�ra
            transform.LookAt(transform.position + cameraDirection);
        }
    }
}