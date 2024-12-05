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
    
    public GameObject cannonObject; // GameObject représentant le canon attaché à la tête
    public GameObject projectilePrefab; // Associe ici le prefab du projectile
    public float projectileSpeed;
    public Animator animator;

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
        if (projectilePrefab == null)
        {
            Debug.Log("oe");
            projectilePrefab = Resources.Load<GameObject>("Projectile");

            if (projectilePrefab == null)
            {
                Debug.LogError("Le prefab du projectile est toujours manquant !");
            }
        }
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetInteger("lookDirection", 0);
            cannonObject.SetActive(false); 
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetInteger("lookDirection", 1);
            cannonObject.SetActive(false); // Désactive le canon
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetInteger("lookDirection", 2);
            cannonObject.SetActive(true); 
            FireProjectile();
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
            if (collision.relativeVelocity.y>=0f)
            {
                animator.SetBool("isOnPlatform", true);
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
            animator.SetBool("isOnPlatform", false);
            
        }
    }



    void FireProjectile()
    {
        // Crée le projectile à la position du joueur
        if (!(projectilePrefab != null))
        {
            projectilePrefab = Resources.Load<GameObject>("Projectile 1");
        }
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);

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
