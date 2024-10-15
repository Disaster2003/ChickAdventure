using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    private float intervalSpawn;

    // Start is called before the first frame update
    void Start()
    {
        intervalSpawn = 0; // スポーン時間の初期化
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
        // 時間経過
        intervalSpawn += -Time.deltaTime;
    }

    /// <summary>
    /// オブジェクトを生成する
    /// </summary>
    private void Spawn()
    {
        if (intervalSpawn <= 0)
        {
            intervalSpawn = Random.Range(1.0f, 2.0f);
            Instantiate(spawnObject);
        }
    }
}
