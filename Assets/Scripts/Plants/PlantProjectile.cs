using UnityEngine;

public class PlantProjectile : MonoBehaviour
{
    public float speed = 3f;  // Speed of the projectile
    public int damage = 1;    // Damage dealt by the projectile
    public float lifespan = 15f;  // Lifespan of the projectile in seconds

    void Start()
    {
        // Destroy the projectile after its lifespan expires
        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        // Move the projectile to the right
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hits a skeleton
        if (collision.CompareTag("Skeleton"))
        {
            Skeleton skeleton = collision.GetComponent<Skeleton>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(damage);
                Destroy(gameObject);  // Destroy the projectile after hitting a skeleton
            }
        }
        // Check if the projectile hits a shield
        else if (collision.CompareTag("Shield"))
        {
            Shield shield = collision.GetComponent<Shield>();
            if (shield != null)
            {
                shield.TakeDamage(damage);
                Destroy(gameObject);  // Destroy the projectile after hitting a shield
            }
        }
    }
}
