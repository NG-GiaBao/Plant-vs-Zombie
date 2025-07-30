using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
//public struct Wave
//{
//    public ZombieType zombieType;
//    public int count;
//    public float spawnInterval;
//    public float timeBeforeNext;
//}

public class SpawnController : BaseManager<SpawnController>
{
    //[Header("Wave Configuration")]
    //[SerializeField]
    //private List<Wave> waves = new();

    [Header("Spawn Settings")]
    //[SerializeField] private float timeWaveLimit = 180f;
    [SerializeField] private float timeStartSpawn = 5f;
    [SerializeField] private float timeDuringSpawnZombie = 3f;
    [SerializeField] private float timeDuringWave = 3f;
    [SerializeField] private List<Transform> gateSpawnList;


    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private int activeZombieCount = 0;
    [SerializeField] private int spawnZombieCount = 0;
    [SerializeField] private int waveCount = 3;
    [SerializeField] private bool allWavesSpawned;

    [SerializeField] private float elapsedTime = 0f;
    //[SerializeField] private bool isSpawning = false;
    [SerializeField] private State state = State.Start;

    private readonly Dictionary<ZombieType, GameObject> zombiePrefabs = new();

    private const string ZOMBIE_PATH_FOLDER = "Prefabs/Zombie";

    private enum State
    {
        Start,
        Spawning,
        End,
    }



    private void Start()
    {
        InitTranformGate();
        InitZombie();

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.ZOMBIE_DEAD, HandleZombieDeath);
        }
    }
    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.ZOMBIE_DEAD, HandleZombieDeath);
        }
    }
    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        StartSpawnWave();
    }
    private void InitTranformGate()
    {
        foreach (Transform child in transform)
        {
            gateSpawnList.Add(child);
        }
    }

    private void InitZombie()
    {
        foreach (var prefab in Resources.LoadAll<GameObject>(ZOMBIE_PATH_FOLDER))
        {
            if (!prefab.TryGetComponent<ZombieController>(out var controller)) continue;

            ZombieType type = controller.GetZombieType();
            if (!zombiePrefabs.ContainsKey(type))
                zombiePrefabs[type] = prefab;
        }
    }

    private void SpawnZombie(ZombieType zombieType)
    {
        if (!zombiePrefabs.TryGetValue(zombieType, out GameObject prefab))
        {
            Debug.LogWarning($"Không tìm thấy prefab cho loại zombie: {zombieType}");
            return;
        }

        if (gateSpawnList.Count == 0)
        {
            Debug.LogWarning("Chưa có gate spawn nào được gán!");
            return;
        }

        Transform spawnPoint = gateSpawnList[Random.Range(0, gateSpawnList.Count)];
        GameObject obj = LeanPool.Spawn(prefab, spawnPoint.position, Quaternion.identity);
       
        if (LeanPool.Links.TryGetValue(obj, out LeanGameObjectPool pool))
        {
            pool.Stamp = true;
        } 
        else Debug.LogWarning($"Không tìm thấy pool cho prefab: {prefab.name}");

        if (obj != null)
        {
            if (obj.TryGetComponent<ZombieController>(out var zombieController))
            { 
                zombieController.OnSpawn?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning($"Không thể spawn zombie từ prefab: {prefab.name}");
        }
        spawnZombieCount++;
        activeZombieCount++;


    }
    private void HandleZombieDeath(object value)
    {
        activeZombieCount--;
        CheckForVictory();
    }
    private void CheckForVictory()
    {
        if (allWavesSpawned && activeZombieCount <= 0 )
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_WIN, null);
            }
            Debug.Log("Thắng");
        }
    }
    private void StartSpawnWave()
    {
        elapsedTime += Time.deltaTime;
       
        switch (state)
        {
            case State.Start:
                {
                    if (elapsedTime >= timeStartSpawn)
                    {
                        SpawnZombie(ZombieType.ZOMBIE_NORMAL);
                        elapsedTime = 0;
                        state = State.Spawning;
                    }
                }
                break;
            case State.Spawning:
                {
                    if (elapsedTime >= timeDuringSpawnZombie)
                    {
                        SpawnZombie(ZombieType.ZOMBIE_NORMAL);
                        elapsedTime = 0;
                    }

                    if (spawnZombieCount >= GetZombiePerWave(currentWaveIndex))
                    {
                        currentWaveIndex++;
                        spawnZombieCount = 0;
                        elapsedTime = 0;
                        state = State.End;
                    }
                }
                break;
            case State.End:
                {
                    if (elapsedTime >= timeDuringWave)
                    {
                        state = State.Start;
                        elapsedTime = 0;
                    }
                }
                break;
        }
        if(currentWaveIndex > waveCount)
        {
            allWavesSpawned = true;
        }    
    }
    private int GetZombiePerWave(int waveIndex)
    {
        return 2 + waveIndex * 2; 
    }

    public void ResetWave()
    {
        activeZombieCount = 0;
        currentWaveIndex = 0;
        elapsedTime = 0;
        allWavesSpawned = false;
        state = State.Start;
       
    }    
}
