using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 1f;

    public float speed = 15f;

    void Start()
    {
        // Détruire le projectile après un certain temps (par exemple, 1 seconde)
        //Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Déplacer le projectile vers le haut
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject); // Détruit l'ennemi ou applique des dégâts
        }
    }
}
