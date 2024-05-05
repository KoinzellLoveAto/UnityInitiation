using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : MonoBehaviour
{
    //variable a set sur unity
    public GameObject Item;
    public Transform SpawnPoint;

    public void SpawnItem()
    {
        //on crée un gameobjet dans la scnène à  la position, et rotation voulu.
        Instantiate(Item, SpawnPoint.position, SpawnPoint.rotation);
    }
}
