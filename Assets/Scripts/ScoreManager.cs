using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public List<Sprite> digitSprites;       // Liste des sprites pour les chiffres 0 à 9
    public GameObject digitPrefab;          // Prédéfini pour chaque chiffre du score
    public float scoreMultiplier = 1.5f;    // Multiplicateur de vitesse d'augmentation du score

    private float highestPoint;
    private int score;
    private List<GameObject> digitObjects = new List<GameObject>();

    void Start()
    {
        highestPoint = player.position.y;
        score = 0;

        // Initialiser un seul objet pour le premier chiffre
        CreateDigitObjects(1);
        UpdateScoreUI();
    }

    void Update()
    {
        // Si le joueur dépasse le point le plus haut, mettre à jour le score
        if (player.position.y > highestPoint)
        {
            highestPoint = player.position.y;

            // Calcul du score en appliquant le multiplicateur
            score = Mathf.FloorToInt(highestPoint * scoreMultiplier);

            // Mettre à jour l'affichage du score
            UpdateScoreUI();
        }
    }

    void CreateDigitObjects(int digitCount)
    {
        // Supprimer les objets de chiffres précédents, s'il y en a
        foreach (GameObject digit in digitObjects)
        {
            Destroy(digit);
        }
        digitObjects.Clear();

        // Créer de nouveaux objets de chiffre pour le nombre de chiffres requis
        for (int i = 0; i < digitCount; i++)
        {
            GameObject digit = Instantiate(digitPrefab, transform);
            digitObjects.Add(digit);
        }
    }

    void UpdateScoreUI()
    {
        // Convertir le score en chaîne pour obtenir le nombre de chiffres
        string scoreStr = score.ToString();
        int digitCount = scoreStr.Length;

        // Ajuster le nombre d'objets chiffre si nécessaire
        if (digitObjects.Count != digitCount)
        {
            CreateDigitObjects(digitCount);
        }

        // Mettre à jour chaque chiffre avec le sprite correspondant et le positionner de gauche à droite
        for (int i = 0; i < digitCount; i++)
        {
            int digit = int.Parse(scoreStr[i].ToString());
            digitObjects[i].GetComponent<SpriteRenderer>().sprite = digitSprites[digit];

            // Positionner chaque chiffre de gauche à droite
            digitObjects[i].transform.localPosition = new Vector3(i * 0.2f, 0, 0); // Ajustez pour espacement
        }
    }
}
