using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnedPos;
    [SerializeField] private GameObject prefabToSpawn;

    // Find the target scene by its name
    public GameObject CurrRespawnedPlayer { get; set; }

    // public AnimationClip animationClip;  // Reference to the animation clip
    private bool toSpawn = false;

    private void Update()
    {
        if (toSpawn)
        {
            SpawnItem();
            toSpawn = false;
        }
    }

    public void ToSpawn()
    {
        toSpawn = true;
    }

    void SpawnItem()
    {
        // Instantiate the item at the spawn point
        GameObject instantiatedItem = Instantiate(prefabToSpawn, spawnedPos, Quaternion.identity);
        CurrRespawnedPlayer = instantiatedItem;
        Debug.Log("INSTANTIATE: pos - " + spawnedPos);
        // Attach animation clip to the instantiated item
        // Animation animation = instantiatedItem.GetComponent<Animation>();
        // animation.AddClip(animationClip, "ItemAnimation");
        // animation.Play("ItemAnimation");
    }
}