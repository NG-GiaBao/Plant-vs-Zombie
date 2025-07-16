using UnityEngine;

public class ZombieCollider : MonoBehaviour
{
    [SerializeField] private Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("House"))
        {
            col.isTrigger = false;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            col.isTrigger = true;
        }
        if( collision.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            col.isTrigger = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if( collision.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            if (collision.gameObject.TryGetComponent(out BasePlantHeal plantHeal))
            {
                plantHeal.ReceiveHeal(1);
                Debug.Log("Plant received heal damage.");
            }
        }
    }

}
