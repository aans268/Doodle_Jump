using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownPlatform : MonoBehaviour
{
    private Collider platformCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y<=0f)
        {
            Rigidbody2D rb= collision.gameObject.GetComponent<Rigidbody2D>();
            if(rb!=null)
            {
                //platformCollider.enabled = false;
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
