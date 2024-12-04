using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownPlatform : MonoBehaviour
{
    private Collider platformCollider;  // Collider de la plateforme
    public Animator animator;  // Référence à l'Animator de l'objet
    public float animationDuration = 1f; // Durée de l'animation en secondes
    private bool hasAnimated = false; // Pour suivre si l'animation a déjà été jouée
    private Rigidbody2D rb; // Référence au Rigidbody2D de la plateforme

    void Start()
    {
        // Vérifiez que tous les composants nécessaires sont attachés
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator est manquant sur la plateforme marron !");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D est manquant sur la plateforme marron !");
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic;  // Initialisation à Kinematic
        }

        platformCollider = GetComponent<Collider>();
        if (platformCollider == null)
        {
            //Debug.LogError("Collider est manquant sur la plateforme marron !");
        }

        hasAnimated = false;  // Réinitialiser l'état d'animation
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Vérifier si le collider a un Rigidbody2D et si la vitesse sur l'axe Y est négative (le joueur tombe)
        Rigidbody2D rb2D = collider.GetComponent<Rigidbody2D>();
        if (rb2D != null && rb2D.velocity.y < 0)  // Le joueur doit tomber
        {
            if (collider.CompareTag("Player") && !hasAnimated)
            {
                // Activer l'animation si elle n'a pas déjà été jouée
                if (animator != null)
                {
                    animator.SetBool("isActivated", true); // Active l'animation
                }
                hasAnimated = true; // Marquer que l'animation a été jouée
                StartCoroutine(HandleAnimationEnd());
                
                // Changer le Rigidbody2D de la plateforme à dynamique pour la destruction
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                else
                {
                    //Debug.LogError("Rigidbody2D manquant dans la plateforme lors de l'activation.");
                }
            }
        }
    }

    IEnumerator HandleAnimationEnd()
    {
        // Attendre la durée de l'animation
        yield return new WaitForSeconds(animationDuration);

        // Détruire la plateforme après l'animation
        Destroy(gameObject);
    }
}
