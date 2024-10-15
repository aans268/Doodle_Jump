using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public GameObject GreenPlatformPrefab;
    public GameObject BluePlatformPrefab;
    public GameObject BrownPlatformPrefab;
    public GameObject WhitePlatformPrefab;

    public int initialPlatformCount = 5; // Moins de plateformes au début
    public float initialYSpacing = 1.5f;
    public float maxYSpacing = 5f;

    private float currentYPosition = 0f;
    private float minX = -2.7f;
    private float maxX = 2.7f;
    private float platformSpacingIncrease = 0.01f;

    // Liste pour stocker les plateformes générées
    private List<GameObject> platforms = new List<GameObject>();

    // Référence à la caméra
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Générer des plateformes initiales
        for (int i = 0; i < initialPlatformCount; i++)
        {
            GeneratePlatform();
        }
    }

    void Update()
    {
        // Générer de nouvelles plateformes si le joueur monte
        if (PlayerHasPassedLowestPlatform())
        {
            GeneratePlatform();
        }

        // Détruire les plateformes hors du champ de vision
        DestroyPlatformsOutOfView();
    }

    void GeneratePlatform()
    {
        // Générer la position pour la nouvelle plateforme
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = Random.Range(minX, maxX);
        spawnPosition.y = currentYPosition + Random.Range(initialYSpacing, initialYSpacing + platformSpacingIncrease);

        // Sélectionner un prefab en fonction des probabilités
        GameObject platformPrefab = GetRandomPlatformPrefab();
        
        // Instancier la plateforme
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        
        // Ajouter à la liste des plateformes
        platforms.Add(newPlatform);
        
        // Augmenter la position en Y et l'espacement pour la prochaine plateforme
        currentYPosition = spawnPosition.y;
        initialYSpacing = Mathf.Min(initialYSpacing + platformSpacingIncrease, maxYSpacing);
    }

    GameObject GetRandomPlatformPrefab()
    {
        float randomValue = Random.value;
        if (randomValue <= 0.6f)
            return GreenPlatformPrefab;
        else if (randomValue <= 0.7f)
            return BluePlatformPrefab;
        else if (randomValue <= 0.9f)
            return BrownPlatformPrefab;
        else
            return WhitePlatformPrefab;
    }

    bool PlayerHasPassedLowestPlatform()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (platforms.Count > 0)
        {
            GameObject lowestPlatform = platforms[0];
            if (player.transform.position.y > lowestPlatform.transform.position.y + 2) // Condition ajustée pour qu'elle ne soit pas trop sensible
            {
                return true;
            }
        }
        return false;
    }

    void DestroyPlatformsOutOfView()
    {
        // Récupérer les coordonnées de la caméra pour savoir quand une plateforme sort de l'écran
        float cameraBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize;

        // Parcourir les plateformes et les détruire si elles sont en dehors du champ de la caméra
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = platforms[i];
            // Vérifie si la plateforme existe toujours avant d'essayer de l'utiliser
            if (platform != null && platform.transform.position.y < cameraBottomY - 1) // Détruire quand elle est en dessous du champ de vision
            {
                Destroy(platform); // Détruit la plateforme
                platforms.RemoveAt(i); // Retire la plateforme de la liste après destruction
                // Optionnel : débogage pour vérifier la destruction
                // Debug.Log("Plateforme détruite : " + platform.name);
            }
        }
    }
}
