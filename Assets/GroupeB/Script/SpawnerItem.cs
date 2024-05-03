using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : MonoBehaviour
{
    public GameObject Item;
    public Transform SpawnPoint;

    public void SpawnItem()
    {
        Instantiate(Item, SpawnPoint.position, SpawnPoint.rotation);
    }
}
