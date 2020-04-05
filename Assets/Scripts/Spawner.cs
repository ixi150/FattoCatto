using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    public Vector2 minMaxInterval;
    public int numberOfSpawns=1;

    [Range(5, 60), SerializeField]
    float secondsToIncreaseSpawnCount=10;

    float timer;

    private void Awake()
    {
        RandomizeTimer();
        InvokeRepeating("IncreaseSpawnCount", secondsToIncreaseSpawnCount, secondsToIncreaseSpawnCount);
    }

    private void Update()
    {
        if ((timer -= Time.deltaTime) <= 0 && FattoCatto.IsCattoAlive())
        {
            RandomizeTimer();
            for (int i = 0; i < numberOfSpawns; i++)
                SpawnPrefab();
        }
    }

    void RandomizeTimer()
    {
        timer = Random.Range(minMaxInterval.x, minMaxInterval.y);
    }

    void SpawnPrefab()
    {
        bool isFacingRight = Random.Range(0f, 1f) > .5f;
        var parent = GetRandomChild();
        var enemiesX = parent.GetComponentsInChildren<Enemy>(false)
            .Where(_ => _.canBeAttacked && _.gameTransform.facingRight == isFacingRight)
            .Select(_ => _.gameTransform.x)
            .ToList();
        float x = prefab.GetComponentInChildren<GameTransform>().x;
        while (enemiesX.Contains(x))
        {
            x--;
            if (x < 3) return;
        }
        var g = Instantiate(prefab, parent);
        var enemy = g.GetComponent<GameTransform>();
        enemy.x = x;
        enemy.facingRight = isFacingRight;
    }

    Transform GetRandomChild()
    {
        //return transform.GetChild(Random.Range(0, transform.childCount));
        return transform.GetChild(FattoCatto.GetLineIndex());
    }

    void IncreaseSpawnCount()
    {
        numberOfSpawns++;
        if (numberOfSpawns > 15)
        {
            CancelInvoke("IncreaseSpawnCount");
        }
    }
}
