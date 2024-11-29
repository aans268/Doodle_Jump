using System.Collections.Generic; // Nécessaire pour utiliser List<>
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public List<Sprite> digitSprites;       // Liste des sprites pour les chiffres 0 à 9
    public GameObject digitPrefab;          // Prédéfini pour chaque chiffre
    public Transform currentScoreParent;    // Parent des chiffres pour le score final
    public Transform bestScoreParent;       // Parent des chiffres pour le meilleur score

    private List<GameObject> currentScoreObjects = new List<GameObject>();
    private List<GameObject> bestScoreObjects = new List<GameObject>();

    void Start()
    {
        // Afficher le score actuel
        UpdateDigitObjects(currentScoreObjects, ScoreManager.score, currentScoreParent);

        // Afficher le meilleur score
        UpdateDigitObjects(bestScoreObjects, ScoreManager.Bestscore, bestScoreParent);
    }

    void UpdateDigitObjects(List<GameObject> digitList, int value, Transform parent)
    {
        // Convertir la valeur en chaîne pour déterminer le nombre de chiffres
        string valueStr = value.ToString();
        int digitCount = valueStr.Length;

        // Supprimer les anciens objets chiffre
        foreach (GameObject digit in digitList)
        {
            Destroy(digit);
        }
        digitList.Clear();

        // Créer les nouveaux objets chiffre
        for (int i = 0; i < digitCount; i++)
        {
            int digit = int.Parse(valueStr[i].ToString());
            GameObject digitObj = Instantiate(digitPrefab, parent);
            digitObj.GetComponent<SpriteRenderer>().sprite = digitSprites[digit];

            // Positionner chaque chiffre
            digitObj.transform.localPosition = new Vector3(i * 0.2f, 0, 0); // Ajustez l'espacement
            digitList.Add(digitObj);
        }
    }
}
