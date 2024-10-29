using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownPlatform : MonoBehaviour
{
    private Collider platformCollider;

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y<0f)
        {
            Rigidbody2D rb= collision.gameObject.GetComponent<Rigidbody2D>();
            if(rb!=null)
            {
                //platformCollider.enabled = false;
                Destroy(gameObject);
            }
        }
    }
    */


    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Vérifie si l'objet qui entre en collision est le joueur
        if (collider.CompareTag("Player"))
        {
            // Vérifie si le joueur tombe sur la plateforme
            if (collider.GetComponent<Rigidbody2D>().velocity.y < 0) // Le joueur doit tomber
            {
                // Détruit la plateforme
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
