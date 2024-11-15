using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float bounceForce = 10f;
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    private float moveX;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteGauche;
    public Sprite spriteDroite;
    public Sprite spriteHaut; // Sprite du personnage regardant vers le haut
    public Sprite spriteJambesPlieesGauche;
    public Sprite spriteJambesPlieesDroite;
    public Sprite spriteJambesPlieesHaut;


    public GameObject cannonObject; // GameObject représentant le canon attaché à la tête
    public GameObject projectilePrefab; // Associe ici le prefab du projectile
    public float projectileSpeed;
    public GameObject gameOverUI;

    // Limites du terrain
    private float minX = -3f;
    private float maxX = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cannonObject.SetActive(false); // Le canon est désactivé par défaut
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.sprite = spriteGauche;
            cannonObject.SetActive(false); // Désactive le canon
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRenderer.sprite = spriteDroite;
            cannonObject.SetActive(false); // Désactive le canon
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spriteRenderer.sprite = spriteHaut; // Affiche le sprite du personnage regardant vers le haut
            cannonObject.SetActive(true); // Active le canon
            FireProjectile(); // Tire un projectile
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Change le sprite du personnage en fonction de la direction actuelle
            if (spriteRenderer.sprite == spriteGauche)
            {
                spriteRenderer.sprite = spriteJambesPlieesGauche;
            }
            else if (spriteRenderer.sprite == spriteDroite)
            {
                spriteRenderer.sprite = spriteJambesPlieesDroite;
            }
            else if (spriteRenderer.sprite == spriteHaut)
            {
                spriteRenderer.sprite = spriteJambesPlieesHaut;
            }
        }

        // Si le personnage touche un monstre, conservez le comportement existant
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 center = collision.collider.bounds.center;

            if (contactPoint.y > center.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, bounceForce);
                Destroy(collision.gameObject);
            }
            else
            {
                Die();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Retour au sprite original en fonction de la direction actuelle
            if (spriteRenderer.sprite == spriteJambesPlieesGauche)
            {
                spriteRenderer.sprite = spriteGauche;
            }
            else if (spriteRenderer.sprite == spriteJambesPlieesDroite)
            {
                spriteRenderer.sprite = spriteDroite;
            }
            else if (spriteRenderer.sprite == spriteJambesPlieesHaut)
            {
                spriteRenderer.sprite = spriteHaut;
            }
        }
    }



    void FireProjectile()
    {
        // Crée le projectile à la position du joueur
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
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
}
