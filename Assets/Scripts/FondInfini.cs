using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FondInfini : MonoBehaviour
{
    public  GameObject backgroundPrefab;
    public Transform playerTransform;
    public float spawnDistance =20f;
    private float lastSpawnY =0f;
    void Start()
    {
        SpawnBackground();        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.y > lastSpawnY - spawnDistance)
        {
            SpawnBackground();
        }
        
    }


    void SpawnBackground()
    {
        Vector3 spawnPosition = new Vector3(0, lastSpawnY + spawnDistance, 0);
        GameObject newBackground = Instantiate(backgroundPrefab,spawnPosition,Quaternion.identity);

        SpriteRenderer spriteRenderer = newBackground.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName ="Fond";
            spriteRenderer.sortingOrder = 0;
        }
        lastSpawnY+= spawnDistance;
    }
}
