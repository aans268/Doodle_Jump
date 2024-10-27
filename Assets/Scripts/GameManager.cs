using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GreenPlatformPrefab;
    public GameObject BluePlatformPrefab;
    public GameObject BrownPlatformPrefab;
    public GameObject WhitePlatformPrefab;

    public Transform player; // Référence au joueur
    public int initialPlatformCount = 100;
    public int minPlatformCount = 20;

    private float minSpacing = 0.5f;
    private float maxSpacing = 1.5f;
    private float minX = -2.7f;
    private float maxX = 2.7f;

    private Vector3 lastPlatformPosition;
    private int currentPlatformCount;

    // Liste pour stocker les plateformes générées
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        currentPlatformCount = initialPlatformCount;
        lastPlatformPosition = new Vector3(0, 0, 0); // Initialiser la position
        GeneratePlatformBatch();
    }

    void Update()
    {
        // Vérifier si le joueur est proche de la dernière plateforme
        if (player.position.y > lastPlatformPosition.y - 10f)
        {
            GeneratePlatformBatch();
        }

        // Supprimer les plateformes qui sont trop loin sous le joueur
        RemovePassedPlatforms();
    }

    void GeneratePlatformBatch()
    {
        Vector3 spawnPosition = lastPlatformPosition;

        for (int i = 0; i < currentPlatformCount; i++)
        {
            spawnPosition.y += Random.Range(minSpacing, maxSpacing);
            spawnPosition.x = Random.Range(minX, maxX);

            GameObject platformToSpawn = ChoosePlatform();
            GameObject newPlatform = Instantiate(platformToSpawn, spawnPosition, Quaternion.identity);

            // Ajouter la plateforme à la liste
            platforms.Add(newPlatform);
        }

        // Mettre à jour la position de la dernière plateforme générée
        lastPlatformPosition = spawnPosition;

        // Réduire le nombre de plateformes générées par batch
        if (currentPlatformCount > minPlatformCount)
        {
            currentPlatformCount--;
        }

        // Augmenter l'espacement entre les plateformes pour la difficulté
        minSpacing += 0.1f;
        maxSpacing += 0.1f;
    }

    void RemovePassedPlatforms()
    {
        float lowestVisibleY = Camera.main.transform.position.y - Camera.main.orthographicSize;

        // Limite sous laquelle les plateformes seront supprimées (sous le joueur)
        float deleteThresholdY = lowestVisibleY - 0.6f; // Utiliser la position de la caméra

        // Parcourir la liste des plateformes et les supprimer si elles sont sous le seuil
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            // Vérifier si la plateforme est toujours valide avant de la détruire
            if (platforms[i] != null && platforms[i].transform.position.y < deleteThresholdY)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i); // Enlever de la liste après destruction
                //Debug.Log("detruit");
            }
        }
    }


    GameObject ChoosePlatform()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue < 0.6f)
        {
            return GreenPlatformPrefab;
        }
        else if (randomValue < 0.7f)
        {
            return BluePlatformPrefab;
        }
        else if (randomValue < 0.9f)
        {
            return BrownPlatformPrefab;
        }
        else
        {
            return WhitePlatformPrefab;
        }
    }
}
