using Lean.Pool;
using System.Collections;
using UnityEngine;

public class PlantGunAttack : BasePlantAttack
{
    [SerializeField] private bool isShoot;
    private Coroutine shootCoroutine;

    
    private void Update()
    {
        if(GameManager.HasInstance)
        {
            if(GameManager.Instance.IsGameOver)
            {
                StopAllCoroutines();
                return;
            }
        }
        InitRaycast();
    }
   

    private void InitRaycast()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right; // hướng X dương
        float range = 10f;

        Debug.DrawRay(origin, direction * range, Color.red, 2f);
        int zombieLayer = LayerMask.GetMask("Zombie");

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, range, zombieLayer);
        if (hit.collider != null)
        {
            if (!isShoot)
            {
                isShoot = true;
                shootCoroutine = StartCoroutine(ShootLoop());
            }

        }
        else
        {
            if (isShoot)
            {
                isShoot = false;
                StopCoroutine(shootCoroutine);
            }
        }

    }
    IEnumerator ShootLoop()
    {
        while (isShoot)
        {
            FireOneBullet();
            yield return new WaitForSeconds(2f); // Thời gian giữa các lần bắn
        }
    }
    private void FireOneBullet()
    {
        GameObject bullet = LeanPool.Spawn(bulletPref, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().BulletMove(); // Hoặc firePoint.right nếu súng xoay
    }
  
}
