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
    public Animator animator;  // Référence à l'Animator de l'objet
    public float animationDuration = 1f; // Durée de l'animation en secondes
    private bool hasAnimated = false; // Pour suivre si l'animation a déjà été jouée
    private Rigidbody2D rb;




    void Start()
    {
        // Désactiver l'animation par défaut
        animator.SetBool("isActivated", false);
        platformCollider = GetComponent<Collider>();
        hasAnimated = false; // Réinitialiser l'état d'animation
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Vérifier si le collider qui entre en contact est celui du joueur
        if (collider.GetComponent<Rigidbody2D>().velocity.y < 0) // Le joueur doit tomber
            {
                if (collider.CompareTag("Player") && !hasAnimated)
                {
                    // Activer l'animation
                    animator.SetBool("isActivated", true);
                    hasAnimated = true; // Marquer que l'animation a été jouée
                    StartCoroutine(HandleAnimationEnd());
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
            }
    }


    IEnumerator HandleAnimationEnd()
    {
        // Attendre la durée de l'animation
        yield return new WaitForSeconds(animationDuration);

        Destroy(gameObject);
    }

        

}
