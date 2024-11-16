using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GreenPlatformPrefab;
    public GameObject BluePlatformPrefab;
    public GameObject BrownPlatformPrefab;
    public GameObject WhitePlatformPrefab;

    public GameObject Monster1Prefab;
    public GameObject Monster2Prefab;
    public GameObject Monster3Prefab;

    public GameObject HolePrefab;

    public Transform player;
    public int initialPlatformCount = 100;
    public int minPlatformCount = 20;

    public float monsterProb = 0.05f;
    public float holeProb = 0.01f;

    private float minSpacing = 0.5f;
    private float maxSpacing = 1.5f;
    private float minX = -2.7f;
    private float maxX = 2.7f;

    private float jumpHeight = 2.3f; // Hauteur maximale de saut du joueur
    private float safeZoneRadius = 3f; // Rayon sécurisé autour du joueur

    private Vector3 lastPlatformPosition;
    private List<Vector3> holePositions = new List<Vector3>();
    private List<Vector3> monsterPositions = new List<Vector3>();
    private List<GameObject> platforms = new List<GameObject>();

    private int currentPlatformCount;

    void Start()
    {
        currentPlatformCount = initialPlatformCount;
        lastPlatformPosition = new Vector3(0, 0, 0);
        GeneratePlatformBatch();
    }

    void Update()
    {
        if (player.position.y > lastPlatformPosition.y - 10f)
        {
            GeneratePlatformBatch();
        }
        RemovePassedPlatforms();
    }

    void GeneratePlatformBatch()
    {
        Vector3 spawnPosition = lastPlatformPosition;

        // Étape 1 : Générer les trous et les monstres
        for (int i = 0; i < currentPlatformCount; i++)
        {
            spawnPosition.y += Random.Range(minSpacing, Mathf.Min(maxSpacing, jumpHeight));
            spawnPosition.x = Random.Range(minX, maxX);

            // Vérifier si la position est dans la zone sécurisée autour du joueur pour les monstres et les trous
            if (Vector3.Distance(spawnPosition, player.position) < safeZoneRadius)
            {
                i--; // Re-tenter la génération si la position est trop proche du joueur
                continue;
            }

            if (Random.value < holeProb)
            {
                GenerateHole(spawnPosition);
            }
            else if (Random.value < monsterProb)
            {
                GenerateMonster(spawnPosition);
            }
        }

        // Étape 2 : Générer les plateformes en évitant les trous et les monstres
        spawnPosition = lastPlatformPosition;
        bool hasNonBrownPlatform = false; // Variable pour s'assurer qu'il y ait toujours une plateforme non-marron

        for (int i = 0; i < currentPlatformCount; i++)
        {
            spawnPosition.y += Random.Range(minSpacing, Mathf.Min(maxSpacing, jumpHeight));
            spawnPosition.x = Random.Range(minX, maxX);

            // Vérifier que la position n'est pas sur un trou ou un monstre
            if (IsOverHole(spawnPosition) || IsOverMonster(spawnPosition))
            {
                i--; // Re-tenter la génération pour ce cycle
                continue;
            }

            // Ne pas utiliser le safeZoneRadius pour la vérification des plateformes, car elles doivent être accessibles
            GameObject platformToSpawn = ChoosePlatform(ref hasNonBrownPlatform); // Passer la référence pour savoir si une plateforme non-marron a été générée
            GameObject newPlatform = Instantiate(platformToSpawn, spawnPosition, Quaternion.identity);

            platforms.Add(newPlatform);
        }

        // Si aucune plateforme non-marron n'a été générée, en forcer une avant de terminer la génération
        if (!hasNonBrownPlatform)
        {
            Vector3 fallbackPosition = spawnPosition;
            fallbackPosition.y += jumpHeight; // S'assurer que la plateforme est à une hauteur où le joueur peut sauter
            GameObject platformToSpawn = ChoosePlatform(ref hasNonBrownPlatform);
            Instantiate(platformToSpawn, fallbackPosition, Quaternion.identity);
        }

        lastPlatformPosition = spawnPosition;

        if (currentPlatformCount > minPlatformCount)
        {
            currentPlatformCount--;
        }

        minSpacing += 0.1f;
        maxSpacing += 0.1f;
    }

    void RemovePassedPlatforms()
    {
        float deleteThresholdY = Camera.main.transform.position.y - Camera.main.orthographicSize - 5f;

        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i] != null && platforms[i].transform.position.y < deleteThresholdY)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }

    GameObject ChoosePlatform(ref bool hasNonBrownPlatform)
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue < 0.6f)
        {
            hasNonBrownPlatform = true; // Une plateforme non-marron est générée
            return GreenPlatformPrefab;
        }
        else if (randomValue < 0.7f)
        {
            hasNonBrownPlatform = true; // Une plateforme non-marron est générée
            return BluePlatformPrefab;
        }
        else if (randomValue < 0.9f)
        {
            return BrownPlatformPrefab;
        }
        else
        {
            hasNonBrownPlatform = true; // Une plateforme non-marron est générée
            return WhitePlatformPrefab;
        }
    }

    void GenerateMonster(Vector3 position)
    {
        GameObject monsterToSpawn = ChooseMonster();
        monsterPositions.Add(position);
        Instantiate(monsterToSpawn, position, Quaternion.identity);
    }

    GameObject ChooseMonster()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue < 0.33f)
        {
            return Monster1Prefab;
        }
        else if (randomValue < 0.66f)
        {
            return Monster2Prefab;
        }
        else
        {
            return Monster3Prefab;
        }
    }

    void GenerateHole(Vector3 position)
    {
        holePositions.Add(position);
        Instantiate(HolePrefab, position, Quaternion.identity);
    }

    bool IsOverHole(Vector3 position)
    {
        foreach (Vector3 hole in holePositions)
        {
            if (Vector3.Distance(position, hole) < 1f) // Ajuste le seuil selon la taille du trou
            {
                return true;
            }
        }
        return false;
    }

    bool IsOverMonster(Vector3 position)
    {
        foreach (Vector3 monster in monsterPositions)
        {
            if (Vector3.Distance(position, monster) < 1f) // Ajuste le seuil selon la taille des monstres
            {
                return true;
            }
        }
        return false;
    }
}
