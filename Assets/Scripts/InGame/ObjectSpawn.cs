using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public static ObjectSpawn Instance;
    // Singleton
    [Header("Object Pool")]
    public GameObject carPrefab;
    public GameObject oilPrefab;

    private List<GameObject> objectPool = new List<GameObject>();
    private int poolSize = 11;
    private int oilIndex = 10;

    private string poolName = "ObjectPool";
    private Transform poolTransform;

    private IEnumerator spawnCoroutine;

    static int seed;
    System.Random random = new System.Random(seed++);

    private int minValue = 1;
    private int maxValue = 10;

    private int minTime = 1;
    private int maxTime = 3;

    void Start()
    {
        poolTransform = GameObject.Find(poolName).transform;

        if (Instance == null)
            Instance = this;
        // Singleton
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject go;
            if (i != oilIndex)
                go = Instantiate(carPrefab, poolTransform);            
            else
                go = Instantiate(oilPrefab, poolTransform);

            go.SetActive(false);
            objectPool.Add(go);
        }
        // OjbectPooling Setting

        if (spawnCoroutine != null)
            spawnCoroutine = null;
        // Coroutine 초기화
        spawnCoroutine = SpawnCoroutine();
        StartCoroutine(spawnCoroutine);
    }
    public GameObject CarSpawn()
    {
        for (int i = 0; i < oilIndex; ++i)
        {
            if (!objectPool[i].activeInHierarchy)
                return objectPool[i];
        }
        // ObjectPooling Spawn
        return null;
    }
    public GameObject OilSpawn()
    {
        return objectPool[oilIndex];
        // Oil Spawn
    }
    public void ObjectReturn(GameObject _go)
    {
        _go.SetActive(false);
        // ObjectPooling return
    }

    IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            int randomSpawn = random.Next(minValue, maxValue);
            if(randomSpawn == minValue && !objectPool[oilIndex].activeInHierarchy)
                OilSpawn().GetComponent<ObjectMovement>().SetPosition();
            else
                CarSpawn().GetComponent<ObjectMovement>().SetPosition();
            // 확률적으로 차 대신 기름을 Spawn

            int randomTime = random.Next(minTime, maxTime);
            yield return new WaitForSecondsRealtime(randomTime);
            // 랜덤한 시간 간격으로 Car Spawn
        }
    }
}
