using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public float spawnItemInterval;
    float spawnWaifuInterval;
    public float spawnItemCounter;
    float spawnWaifuCounter;

    float itemChanceRange = 0;
    float waifuChanceRange = 0;

    public List<ItemSpawn> ItemSpawnList;
    public List<ItemSpawn> WaifuSpawnList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (ItemSpawn item in ItemSpawnList)
        {
            item.chanceRangeStart = itemChanceRange;
            itemChanceRange += item.chance;
            item.chanceRangeEnd = itemChanceRange;
        }
        foreach (ItemSpawn item in WaifuSpawnList)
        {
            item.chanceRangeStart = itemChanceRange;
            itemChanceRange += item.chance;
            item.chanceRangeEnd = itemChanceRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnItemCounter -= Time.deltaTime;
        spawnWaifuCounter -= Time.deltaTime;

        if (spawnItemCounter <= 0)
        {
            spawnItem();
            spawnItemCounter = spawnItemInterval;
        }
        if (spawnWaifuCounter <= 0)
        {
            spawnWaifu();
            spawnWaifuCounter = spawnWaifuInterval;
        }

    }
    void spawnItem()
    {
        float randomChance = Random.Range(0f, itemChanceRange);
        foreach (ItemSpawn item in ItemSpawnList)
        {
            if (randomChance > item.chanceRangeStart && randomChance < item.chanceRangeEnd)
                Instantiate(item.itemPrefab, new Vector2(Screen.height, Random.Range((100), (Screen.width - 100))), Quaternion.identity);
        }
    }
    void spawnWaifu()
    {
        float randomChance = Random.Range(0f, waifuChanceRange);
        foreach (ItemSpawn item in WaifuSpawnList)
        {
            if (randomChance > item.chanceRangeStart && randomChance < item.chanceRangeEnd)
                Instantiate(item.itemPrefab, new Vector2(Screen.height, Random.Range((100), (Screen.width - 100))), Quaternion.identity);
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
