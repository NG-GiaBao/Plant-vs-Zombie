using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : BaseManager<SpawnController>
{
    [SerializeField] private float durationTime = 2f;
    [SerializeField] private float spawnTime = 10f;
    [SerializeField] private float time;
    [SerializeField] private float timeWave = 180f;
    [SerializeField] private int amountZombie = 10;
    [SerializeField] private int currentZombieCount = 0;
    [SerializeField] private bool isSpawning;
    [SerializeField] private List<Transform> gateSpawnList = new();
    [SerializeField] private Dictionary<ZombieType, GameObject> zombieList = new();
    public int AmountZombie => amountZombie;
    private const string ZOMBIE_PATH_FOLDER = "Prefabs/Zombie";

    private void Start()
    {
        InitTranformGate();
        InitZombie();
    }
    private void Update()
    {
        SetTime();
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
        foreach (GameObject zombiePrefab in Resources.LoadAll(ZOMBIE_PATH_FOLDER))
        {
            ZombieType zombieType = zombiePrefab.GetComponent<ZombieController>().GetZombieType();
            if (!zombieList.ContainsKey(zombieType))
            {
                zombieList.Add(zombieType, zombiePrefab);
            }
            else
            {
                Debug.LogWarning($"Duplicate zombie type found: {zombieType}");
            }
        }
    }
    public void SetTime()
    {
        time += Time.deltaTime;
        if (time >= timeWave) return;
        if (time >= spawnTime && !isSpawning)
        {
            StartCoroutine(DelaySpawnZombie());
            time = 0f;
        }
    }
    private void SpawnZombie(ZombieType zombieType)
    {
        Debug.Log("tesst");
        if (zombieList.TryGetValue(zombieType, out GameObject zombiePrefab))
        {
            Transform spawnPoint = gateSpawnList[Random.Range(0, gateSpawnList.Count)];
            GameObject zombieInstance = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log($"Spawned Zombie: {zombieType} at {spawnPoint.position}");
        }
        else
        {
            Debug.LogWarning($"Zombie prefab not found for type: {zombieType}");
        }
    }
    IEnumerator DelaySpawnZombie()
    {
        isSpawning = true;
        while (currentZombieCount < amountZombie)
        {
            yield return new WaitForSeconds(durationTime);
            SpawnZombie(ZombieType.ZOMBIE_NORMAL);
            currentZombieCount++;
        }
        isSpawning = false;
    }


}
