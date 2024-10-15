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

    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x=moveX;
        rb.velocity= velocity;


    }



}
