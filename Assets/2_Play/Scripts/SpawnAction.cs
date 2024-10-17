using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : MonoBehaviour
{
    [SerializeField] GameObject[] spawnObject;
    private float intervalSpawn; // �X�|�[�����Ԃ̃C���^�[�o��

    // Start is called before the first frame update
    void Start()
    {
        intervalSpawn = 0; // �X�|�[�����Ԃ̏�����
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
            // �����̃C���^�[�o����
            intervalSpawn += -Time.deltaTime;
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�𐶐�����
    /// </summary>
    private void Spawn()
    {
        // �X�|�[��
        intervalSpawn = Random.Range(2.0f, 5.0f);
        Instantiate(spawnObject[Random.Range(0,spawnObject.Length)]);
    }
}
