using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnBall : MonoBehaviour
{
    public CubeBullet Prefab;
    public Transform spotSpawn;
    public float ForceEjection;
    public void SpawnPrefab(float duration)
    {
        float force = Remap(duration, 0, 2, 0, ForceEjection);
        CubeBullet obj = Instantiate(Prefab, spotSpawn);
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
