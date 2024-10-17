using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour
{
    [SerializeField] GameObject[] spawnObject;
    private float intervalSpawn; // スポーン時間のインターバル

    // Start is called before the first frame update
    void Start()
    {
        intervalSpawn = 0; // スポーン時間の初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (intervalSpawn <= 0)
        {
            Spawn();
        }
        else
        {
            // 生成のインターバル中
            intervalSpawn += -Time.deltaTime;
        }
    }

    /// <summary>
    /// オブジェクトを生成する
    /// </summary>
    private void Spawn()
    {
        // スポーン
        intervalSpawn = Random.Range(2.0f, 5.0f);
        Instantiate(spawnObject[Random.Range(0,spawnObject.Length)]);
    }
}
