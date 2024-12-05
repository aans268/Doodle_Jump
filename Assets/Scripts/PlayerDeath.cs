using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    private float lowestVisibleY; // Position la plus basse visible de la caméra
    private Collider2D playerCollider; // Collider du joueur


    // Update is called once per frame

    void Start()
    {
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
        if (playerBottomY < lowestVisibleY)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);


        Invoke("StartDeath", 1f);

    }

    public void StartDeath()
    {

        SceneManager.LoadScene("DieScene");
    }

    
}
