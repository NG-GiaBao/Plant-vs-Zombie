using System;
using UnityEngine;
using UnityEngine.UI;

public enum MoveType
{
    Update,
    Coroutine
}

public class TestCoroutine : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int objectCount = 1000;
    [SerializeField] private Button coroutineButton;
    [SerializeField] private Button updateButton;
    public Action OnDeath;
    public event Action OnDestroy;


    private void Start()
    {
        coroutineButton.onClick.AddListener(() => SpawnAll(MoveType.Coroutine));
        updateButton.onClick.AddListener(() => SpawnAll(MoveType.Update));
    }


    private void SpawnAll(MoveType moveType)
    {
        //for (int i = 0; i < objectCount; i++)
        //{
        //    GameObject obj = Instantiate(enemyPrefab, Random.insideUnitSphere * 5f, Quaternion.identity);
        //    if (moveType == MoveType.Update)
        //    {
        //        obj.AddComponent<Move>();
        //    }
        //    else
        //    {
        //        obj.AddComponent<MoveWithCoroutine>();
        //    }

        //}

    }

}
