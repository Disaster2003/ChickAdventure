using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExplainControl : MonoBehaviour
{
    private Text txtTurorialMessage;
    private float timer; // タイマー

    // Start is called before the first frame update
    void Start()
    {
        txtTurorialMessage = GetComponent<Text>();
        txtTurorialMessage.text = "Spaceでジャンプ!!"; // 説明文の代入
        timer = 3; // タイマーの初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            // プレイ画面へ
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        else
        {
            // 時間経過
            timer += -Time.deltaTime;
        }

        // 透明化
        txtTurorialMessage.color = Color.Lerp(txtTurorialMessage.color, Color.clear, 0.5f * Time.deltaTime);
    }
}
