using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void BulletMove()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }
        else
        {
            Debug.LogError("Rigidbody2D component is not assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            if(collision.TryGetComponent(out ZombieHeal zombieHeal))
            {
                zombieHeal.ReceiveDamage(1);
                Destroy(gameObject);
            }
        }
    }
}
