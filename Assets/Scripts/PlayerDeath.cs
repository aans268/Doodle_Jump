using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    public float deathTreshold = -10f;
    public GameObject gameOverUI;


    private bool isDead = false;
    // Update is called once per frame

    void Start()
    {
        gameOverUI.SetActive(false);
    }
    void Update()
    {

        if (transform.position.y < deathTreshold && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Le joueur est mort !");

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
}
