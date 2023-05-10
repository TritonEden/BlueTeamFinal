using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stats for the bullet, the bullet will damage when hit
public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(10f, 0);
    public Vector2 knockback = new Vector2(0, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // Change knockback direction based on where they are facing
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damage);
                Destroy(gameObject);
            }
        }
    }
}
