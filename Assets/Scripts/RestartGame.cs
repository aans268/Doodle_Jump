using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Assurez-vous d'importer cette ligne pour utiliser UI

public class RestartGame : MonoBehaviour
{
    public void OnRestartButtonClicked()
    {
        // Recharger la scène de jeu
        SceneManager.LoadScene("GameScene"); // Remplace "GameScene" par le nom de ta scène de jeu
    }
}
