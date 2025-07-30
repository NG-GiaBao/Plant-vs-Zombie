using Lean.Pool;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class Move : MonoBehaviour
{
    private enum State
    {
        Moving,
        Waiting,
        Done
    }

    private State currentState = State.Moving;
    private float moveDuration = 2f;
    private float waitDuration = 1f;

    private float timer = 0f;
    private Vector3 direction;
    private float speed = 1f;

    private void Start()
    {
        direction = Random.onUnitSphere;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case State.Moving:
                transform.position += direction * speed * Time.deltaTime;
                if (timer >= moveDuration)
                {
                    timer = 0f;
                    currentState = State.Waiting;
                }
                break;

            case State.Waiting:
                if (timer >= waitDuration)
                {
                    currentState = State.Done;
                }
                break;

            case State.Done:
                Destroy(gameObject);
                break;
        }
    }

}
