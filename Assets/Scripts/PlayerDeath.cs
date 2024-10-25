using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    public GameObject gameOverUI;
    private float lowestVisibleY; // Position la plus basse visible de la caméra
    private Collider2D playerCollider; // Collider du joueur


    // Update is called once per frame

    void Start()
    {
        gameOverUI.SetActive(false);
        playerCollider = GetComponent<Collider2D>(); // Assurer que le joueur a un Collider2D

    }
    void Update()
    {
        CheckIfOutOfView();
    }


    void CheckIfOutOfView()
    {
        // Calculer la limite inférieure de la caméra visible
        lowestVisibleY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float playerBottomY = transform.position.y - playerCollider.bounds.extents.y;


        // Vérifier si le joueur est sous la limite visible (uniquement par le bas)
        if (playerBottomY < lowestVisibleY)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);


        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Invoke("RestartGame", 2f);

    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // void Die()
    // {
    //     SceneManager.LoadScene("DieScene", LoadSceneMode.Single);
    //     GameManager.Instance.SpawnPlayer(transform.position);

    // }
}
