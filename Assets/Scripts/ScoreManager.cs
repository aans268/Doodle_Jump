using System.Collections.Generic; // Pour List<>
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public List<Sprite> digitSprites;       // Liste des sprites pour les chiffres 0 à 9
    public GameObject digitPrefab;          // Prédéfini pour chaque chiffre du score
    public float scoreMultiplier = 1.5f;    // Multiplicateur de vitesse d'augmentation du score

    private float highestPoint;
    public static int score;
    public static int Bestscore;

    public List<GameObject> digitObjects = new List<GameObject>(); // Pour le score actuel
    public List<GameObject> BestdigitObjects = new List<GameObject>(); // Pour le meilleur score

    public bool showBestScore = true; // Contrôle si le meilleur score est affiché

    void Start()
    {
        LoadBestScore();

        highestPoint = player.position.y;
        score = 0;

        // Initialiser les objets pour le score
        CreateDigitObjects(digitObjects, 1);

        // Initialiser les objets pour le meilleur score uniquement si activé
        if (showBestScore)
        {
            CreateDigitObjects(BestdigitObjects, 1);
        }

        UpdateScoreUI();
        if (showBestScore)
        {
            UpdateBestScoreUI();
        }
    }

    void Update()
    {
        // Mettre à jour le score si le joueur monte plus haut
        if (player.position.y > highestPoint)
        {
            highestPoint = player.position.y;
            score = Mathf.FloorToInt(highestPoint * scoreMultiplier);

            UpdateScoreUI();

            if (score > Bestscore)
            {
                Bestscore = score;
                SaveBestScore();
                if (showBestScore)
                {
                    UpdateBestScoreUI();
                }
            }
        }
    }

    void CreateDigitObjects(List<GameObject> digitList, int digitCount)
    {
        foreach (GameObject digit in digitList)
        {
            Destroy(digit);
        }
        digitList.Clear();

        for (int i = 0; i < digitCount; i++)
        {
            GameObject digit = Instantiate(digitPrefab, transform);
            digitList.Add(digit);
        }
    }

    public void UpdateScoreUI()
    {
        UpdateDigitObjects(digitObjects, score);
    }

    public void UpdateBestScoreUI()
    {
        UpdateDigitObjects(BestdigitObjects, Bestscore);
    }

    void UpdateDigitObjects(List<GameObject> digitList, int value)
    {
        string valueStr = value.ToString();
        int digitCount = valueStr.Length;

        if (digitList.Count != digitCount)
        {
            CreateDigitObjects(digitList, digitCount);
        }

        for (int i = 0; i < digitCount; i++)
        {
            int digit = int.Parse(valueStr[i].ToString());
            digitList[i].GetComponent<SpriteRenderer>().sprite = digitSprites[digit];
            digitList[i].transform.localPosition = new Vector3(i * 0.2f, 0, 0); // Ajustez l'espacement
        }
    }

    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", Bestscore);
        PlayerPrefs.Save();
    }

    void LoadBestScore()
    {
        Bestscore = PlayerPrefs.GetInt("BestScore", 0);
    }
}
