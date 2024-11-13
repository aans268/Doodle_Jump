using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleController : MonoBehaviour
{
    public GameObject gameOverUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si le joueur entre en collision avec le trou (par exemple, un objet avec un tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            Die(collision.gameObject); // Passer le joueur à la fonction Die
        }
    }

    void Die(GameObject player)
    {
        // Désactive le trou
        //gameObject.SetActive(false);

        // Désactive le joueur
        player.SetActive(false);

        // Affiche l'écran Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Redémarre le jeu après 2 secondes
        Invoke("RestartGame", 2f);
    }

    void RestartGame()
    {
        // Redémarre la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
