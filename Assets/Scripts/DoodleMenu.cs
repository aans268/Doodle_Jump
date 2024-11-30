using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJump : MonoBehaviour
{
    public Rigidbody2D rb;          // Référence au Rigidbody2D du personnage
    public float jumpForce = 10f;   // Force du saut, vous pouvez ajuster cette valeur

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // Initialiser le Rigidbody2D
    }

    void Start()
    {
        Jump();  // Effectuer le saut immédiatement au début du jeu
    }

    // Fonction pour effectuer le saut
    void Jump()
    {
        // Réinitialiser la vélocité verticale à 0 pour éviter l'accumulation de vitesse
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // Appliquer une force d'impulsion vers le haut pour effectuer le saut
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
