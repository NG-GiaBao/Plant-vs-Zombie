using Lean.Pool;
using UnityEngine;

public class PlantGunAttack : BasePlantAttack
{
    [SerializeField] private bool isShoot;
    [SerializeField] private float timer;
    [SerializeField] private float durationShoot;


    public void InitRaycast()
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
                FireOneBullet();
                isShoot = true;
            }
            else
            {
                ShootTime();
            }
        }
    }
    public void ShootTime()
    {
        timer += Time.deltaTime;
        if (timer >= durationShoot)
        {
            isShoot = false;
            timer = 0;
        }

    }
    private void FireOneBullet()
    {
        GameObject bullet = LeanPool.Spawn(bulletPref, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().BulletMove(); // Hoặc firePoint.right nếu súng xoay
    }

}
