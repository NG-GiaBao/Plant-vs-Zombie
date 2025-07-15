using Unity.VisualScripting;
using UnityEngine;

public class ZombieMove : BaseZombieMove
{
    [SerializeField] private Rigidbody2D rb2D;

    [SerializeField] private float speed = 2f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    //private void FixedUpdate()
    //{
    //    Move();
    //}

    public void Move()
    {
        rb2D.MovePosition(rb2D.position + Vector2.left * speed * Time.fixedDeltaTime);
    }




}
