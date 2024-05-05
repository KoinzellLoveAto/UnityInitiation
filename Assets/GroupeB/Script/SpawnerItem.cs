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
        //on cr�e un gameobjet dans la scn�ne �  la position, et rotation voulu.
        Instantiate(Item, SpawnPoint.position, SpawnPoint.rotation);
    }
}
