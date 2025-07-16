using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HouseMain : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool isAttacked;
    private void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.SEND_HEAL_HOUSE, health);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            if (!isAttacked)
            {
                isAttacked = true;
                health -= 1;
                StartCoroutine(DelayAttacked());
                if (ListenerManager.HasInstance)
                {
                    ListenerManager.Instance.BroadCast(ListenType.SEND_HEAL_HOUSE, health);
                }
                if (health <= 0)
                {
                    if (ListenerManager.HasInstance)
                    {
                        ListenerManager.Instance.BroadCast(ListenType.ZOMBIE_WIN, null);
                    }
                }
            }

        }
    }
  
    IEnumerator DelayAttacked()
    {
        yield return new WaitForSeconds(1f);
        isAttacked = false;
    }
}
