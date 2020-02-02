using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float spawnItemInterval;
    public float spawnWaifuInterval;
    float spawnItemCountdown;
    float spawnWaifuCountdown;

    float itemChanceRange = 0;
    float waifuChanceRange = 0;

    public List<ItemSpawn> ItemSpawnList;
    public List<ItemSpawn> WaifuSpawnList;

    public static ItemSpawner Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        spawnItemCountdown = spawnItemInterval;
        spawnWaifuCountdown = spawnWaifuInterval;
        foreach (ItemSpawn item in ItemSpawnList)
        {
            item.chanceRangeStart = itemChanceRange;
            itemChanceRange += item.chance;
            item.chanceRangeEnd = itemChanceRange;
        }
        foreach (ItemSpawn item in WaifuSpawnList)
        {
            item.chanceRangeStart = waifuChanceRange;
            waifuChanceRange += item.chance;
            item.chanceRangeEnd = waifuChanceRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnItemCountdown -= Time.deltaTime;
        spawnWaifuCountdown -= Time.deltaTime;

        if (spawnItemCountdown <= 0)
        {
            spawnItem();
            spawnItemCountdown = spawnItemInterval;
        }
        if (spawnWaifuCountdown <= 0)
        {
            spawnWaifu();
            spawnWaifuCountdown = spawnWaifuInterval;
        }

    }
    void spawnItem()
    {
        float randomChance = Random.Range(0f, itemChanceRange);
        foreach (ItemSpawn item in ItemSpawnList)
        {
            if (randomChance > item.chanceRangeStart && randomChance < item.chanceRangeEnd)
                Instantiate(item.itemPrefab, new Vector2(Camera.main.orthographicSize + Random.Range(-7f, 2f), transform.position.y + 2), Quaternion.identity);
        }
    }
    void spawnWaifu()
    {
        float randomChance = Random.Range(0f, waifuChanceRange);
        foreach (ItemSpawn item in WaifuSpawnList)
        {
            if (randomChance > item.chanceRangeStart && randomChance < item.chanceRangeEnd)
                Instantiate(item.itemPrefab, new Vector2(transform.position.x + Random.Range(-7f, 2f), transform.position.y + 2), Quaternion.identity);
        }
    }
}

[System.Serializable]
public class ItemSpawn
{
    public float chance;
    public GameObject itemPrefab;
    public float chanceRangeStart;
    public float chanceRangeEnd;
}
