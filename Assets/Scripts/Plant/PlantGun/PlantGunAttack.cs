using UnityEngine;

public class PlantGunAttack : BasePlantAttack
{

    private void Update()
    {
        InitRaycast();
    }

    private void InitRaycast()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right; // hướng X dương
        float range = 10f;

        Debug.DrawRay(origin, direction * range, Color.red, 2f);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, range);
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }
}
