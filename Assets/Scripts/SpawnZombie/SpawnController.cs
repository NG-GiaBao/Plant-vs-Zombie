using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

[System.Serializable]
public struct Wave
{
    public ZombieType zombieType;
    public int count;
    public float spawnInterval;
    public float timeBeforeNext;
}

public class SpawnController : BaseManager<SpawnController>
{
    [Header("Wave Configuration")]
    [SerializeField]
    private List<Wave> waves = new();

    [Header("Spawn Settings")]
    [SerializeField] private float timeWaveLimit = 180f;
    [SerializeField] private float timeStartSpawn = 10f;
    [SerializeField] private List<Transform> gateSpawnList = new();


    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private int activeZombieCount = 0;
    [SerializeField] private bool allWavesSpawned;

    [SerializeField] private float elapsedTime = 0f;
    [SerializeField] private bool isSpawning = false;

    private readonly Dictionary<ZombieType, GameObject> zombiePrefabs = new();

    private const string ZOMBIE_PATH_FOLDER = "Prefabs/Zombie";



    private void Start()
    {
        InitTranformGate();
        InitZombie();
        StartCoroutine(DelayStartSpawn());
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
        if (GameManager.Instance.IsGameOver)
        {
            StopAllCoroutines();
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeWaveLimit && isSpawning)
        {
            StopAllCoroutines();
            isSpawning = false;
        }
    }
    private void InitTranformGate()
    {
        gateSpawnList.Clear();
        foreach (Transform child in transform)
        {
            gateSpawnList.Add(child);
        }
    }

    private void InitZombie()
    {
        zombiePrefabs.Clear();
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
        //Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        GameObject obj = LeanPool.Spawn(prefab, spawnPoint.position, Quaternion.identity);
        
        if (LeanPool.Links.TryGetValue(obj, out LeanGameObjectPool pool))
        {
            pool.Stamp = true;
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy pool cho prefab: {prefab.name}");
        }
        if (obj != null)
        {
            if (obj.TryGetComponent<ZombieController>(out var zombieController))
            {
                zombieController.ZombieHeal.SetHeal(3);
            }
        }
        else
        {
            Debug.LogWarning($"Không thể spawn zombie từ prefab: {prefab.name}");
        }
        activeZombieCount++;
    }
    private void HandleZombieDeath(object value)
    {
        activeZombieCount--;
        CheckForVictory();
    }
    private void CheckForVictory()
    {
        if (allWavesSpawned && activeZombieCount <= 0)
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.PLAYER_WIN, null);
            }
            Debug.Log("Thắng");
        }
    }
    private IEnumerator DelayStartSpawn()
    {
        yield return new WaitForSeconds(timeStartSpawn);
        StartCoroutine(RunAllWaves());
    }

    private IEnumerator RunAllWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            Wave wave = waves[currentWaveIndex];
            Debug.Log($"Bắt đầu wave {currentWaveIndex + 1}: {wave.zombieType} x{wave.count} ---");
            yield return StartCoroutine(RunWave(wave));

            currentWaveIndex++;
            if (currentWaveIndex < waves.Count)
            {
                Debug.Log($"Wave {currentWaveIndex} hoàn tất. Chờ {wave.timeBeforeNext}s qua wave mới.");
                yield return new WaitForSeconds(wave.timeBeforeNext);
            }
        }
        allWavesSpawned = true;
        CheckForVictory();

        Debug.Log("tất cả wave hoàn tất");
    }
    private IEnumerator RunWave(Wave wave)
    {
        isSpawning = true;
        int spawned = 0;

        while (spawned < wave.count)
        {
            SpawnZombie(wave.zombieType);
            spawned++;
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawning = false;
        Debug.Log($"Wave {currentWaveIndex + 1} xong: t {spawned} zombies.");
    }



}
