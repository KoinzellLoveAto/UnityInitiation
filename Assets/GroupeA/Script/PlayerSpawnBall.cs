using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnBall : MonoBehaviour
{
    //variable ref unity
    public CubeBullet Prefab;
    public Transform spotSpawn;

    //variable gerant la force.
    public float ForceEjection;
    public void SpawnPrefab(float duration)
    {
        // remap de la valeur pour faire le lien entre la duration appuyer et la force final a donné a l'(objet
        float force = Remap(duration, 0, 2, 0, ForceEjection);

        // on crée un objet dans la scene, a une position donnée.
        CubeBullet obj = Instantiate(Prefab, spotSpawn);

        // et on appel le script pour "l'initialisé"
        obj.InitWithForce(Camera.main.transform.forward, force);
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
