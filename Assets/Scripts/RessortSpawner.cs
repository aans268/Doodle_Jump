using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessortSpawner : MonoBehaviour
{
     public GameObject springPrefab;  // Le prefab du ressort
    public float spawnChance = 0.05f; // Probabilité qu'un ressort apparaisse

    void Start()
    {
        // Décider si un ressort doit apparaître sur cette plateforme
        if (Random.value <= spawnChance)
        {
            SpawnSpring();
        }
    }

    void SpawnSpring()
    {
        // Calculer la position aléatoire sur l'axe X de la plateforme
        float platformWidth = GetComponent<Collider2D>().bounds.size.x;  // Récupérer la largeur de la plateforme
        float platformX = transform.position.x;  // Position X de la plateforme
        float randomX = Random.Range(platformX - platformWidth / 2, platformX + platformWidth / 2); // Position aléatoire sur la plateforme

        // Créer la position du ressort (déplacé verticalement au-dessus de la plateforme)
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y+0.4f, transform.position.z);

        // Instancier le ressort à la position calculée
        Instantiate(springPrefab, spawnPosition, Quaternion.identity); 
    }
}
