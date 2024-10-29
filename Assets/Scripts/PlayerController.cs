using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed= 10f;
    public Rigidbody2D rb;
    private float moveX;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteGauche;
    public Sprite spriteDroite;

    public GameObject projectilePrefab; // Associe ici le prefab du projectile
    public float projectileSpeed;



    // Limites du terrain
    private float minX = -3f;
    private float maxX = 3f;

    void Awake(){
        rb= GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX=Input.GetAxis("Horizontal")*moveSpeed;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.sprite =spriteGauche;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRenderer.sprite =spriteDroite;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FireProjectile();
        }

    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x=moveX;
        rb.velocity= velocity;

        // Vérifier les limites et téléporter si nécessaire
        if (transform.position.x > maxX)
        {
            // Si on dépasse maxX (3), on téléporte à minX (-3)
            transform.position = new Vector2(minX, transform.position.y);
        }
        else if (transform.position.x < minX)
        {
            // Si on dépasse minX (-3), on téléporte à maxX (3)
            transform.position = new Vector2(maxX, transform.position.y);
        }


    }


    void FireProjectile()
    {
        // Crée le projectile à la position du joueur
         if (projectilePrefab != null)
        {
            // Instancie un nouveau projectile
            /*GameObject projectile =*/ Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            // Ajoute une vitesse au projectile pour qu'il se déplace vers le haut
            //Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
            //projRb.velocity = Vector2.up * projectileSpeed;
        }
        
    }



}
