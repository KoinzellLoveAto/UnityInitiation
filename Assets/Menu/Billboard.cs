using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Trouver la caméra principale dans la scène
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Pas de caméra principale trouvée dans la scène !");
        }
    }

    void Update()
    {
        // S'assurer que la caméra est valide
        if (mainCamera != null)
        {
            // Obtenir la direction vers la caméra
            Vector3 cameraDirection =  transform.position - mainCamera.transform.position;

            // Tourner le billboard pour faire face à la caméra
            transform.LookAt(transform.position + cameraDirection);
        }
    }
}