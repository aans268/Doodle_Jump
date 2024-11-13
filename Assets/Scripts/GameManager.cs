using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Préfabriqués des plateformes
    public GameObject GreenPlatformPrefab;
    public GameObject BluePlatformPrefab;
    public GameObject BrownPlatformPrefab;
    public GameObject WhitePlatformPrefab;

    // Préfabriqués des monstres
    public GameObject Monster1Prefab;
    public GameObject Monster2Prefab;
    public GameObject Monster3Prefab;

    // Préfabriqué du trou
    public GameObject HolePrefab;  // Ajouter le prefab du trou ici

    // Référence au joueur
    public Transform player; 
    public int initialPlatformCount = 100;
    public int minPlatformCount = 20;

    // Probabilités
    public float monsterProb = 0.05f;
    public float holeProb = 0.01f;  // Probabilité d'apparition du trou

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

            if (Random.value < monsterProb) // % de chance d'ajouter un monstre
            {
                GenerateMonster(spawnPosition);
            }

            // Vérifier si un trou doit apparaître
            if (Random.value < holeProb) // % de chance d'ajouter un trou
            {
                GenerateHole(spawnPosition);  // Appeler la méthode pour générer un trou
            }
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

    void GenerateMonster(Vector3 platformPosition)
    {
        // Choisir un monstre avec une probabilité égale
        GameObject monsterToSpawn = ChooseMonster();

        // Positionner le monstre légèrement au-dessus de la plateforme
        Vector3 monsterPosition = new Vector3(0, platformPosition.y + 0.5f, platformPosition.z);

        // Vérifier si la plateforme et le monstre se superposent trop sur l'axe Y
        if (Mathf.Abs(monsterPosition.y - platformPosition.y) < 0.6f)
        {
            monsterPosition.y += 0.6f; // Déplacer le monstre au-dessus de la plateforme
        }

        // Instancier le monstre à la nouvelle position
        Instantiate(monsterToSpawn, monsterPosition, Quaternion.identity);
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

    // Méthode pour générer un trou à une position donnée
    void GenerateHole(Vector3 platformPosition)
    {
        // Instancier le trou à la position de la plateforme
        Instantiate(HolePrefab, platformPosition, Quaternion.identity);
    }
}
