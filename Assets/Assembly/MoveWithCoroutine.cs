using System.Collections;
using UnityEngine;

public class MoveWithCoroutine : MonoBehaviour
{
    private float moveDuration = 2f;
    private float waitDuration = 1f;

    private Vector3 direction;
    private float speed = 1f;

    private void Start()
    {
        direction = Random.onUnitSphere;
        StartCoroutine(Flow());
    }

    private IEnumerator Flow()
    {
        float t = 0f;
        while (t < moveDuration)
        {
            transform.position += direction * speed * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitDuration);
        Destroy(gameObject);
    }
}
