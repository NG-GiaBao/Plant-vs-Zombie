using UnityEngine;

public class HouseMain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Zombie"))
        {
            if(ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.ZOMBIE_WIN, null);
            }
        }    
    }
}
