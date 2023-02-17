using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public static ObjectSpawn Instance;
    // Singleton
    [SerializeField] Canvas canvas;

    public GameObject carPrefab;
    public GameObject oilPrefab;

    private List<GameObject> carPool = new List<GameObject>();
    private int poolSize = 11;

    private IEnumerator spawnCoroutine;

    static int seed;
    System.Random random = new System.Random(seed++);

    private int minTime = 1;
    private int maxTime = 3;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        // Singleton
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject go = Instantiate(carPrefab, canvas.transform);
            go.SetActive(false);
            carPool.Add(go);
        }
        // OjbectPooling
        GameObject oilGo = Instantiate(oilPrefab, canvas.transform);
        oilGo.SetActive(false);

        if (spawnCoroutine != null)
            spawnCoroutine = null;
        // Coroutine 초기화
        spawnCoroutine = SpawnCoroutine();
        StartCoroutine(spawnCoroutine);
    }
    public GameObject CarSpawn()
    {
        for (int i = 0; i < carPool.Count; ++i)
        {
            if (!carPool[i].activeInHierarchy)
                return carPool[i];
        }
        // ObjectPooling Spawn
        return null;
    }

    public void CarRemove(GameObject _go)
    {
        _go.SetActive(false);
        // ObjectPooling return
    }

    IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            CarSpawn().GetComponent<CarMovement>().SetPosition();
            int randomTime = random.Next(minTime, maxTime);
            yield return new WaitForSecondsRealtime(randomTime);
            // 랜덤한 시간 간격으로 Car Spawn
        }
    }
}
