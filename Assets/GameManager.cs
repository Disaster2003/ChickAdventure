using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum STATE_SCENE
    {
        TITLE,  // タイトル画面
        EXPLAIN,// 説明画面
        PLAY,   // プレイ画面
        RANKING,// ランキング画面
    }
    private static STATE_SCENE state_scene;

    // Start is called before the first frame update
    void Start()
    {
        state_scene = STATE_SCENE.TITLE; // シーンの初期化
    }

    /// <summary>
    /// ボタン処理
    /// </summary>
    public static void OnClick()
    {
        // 次のシーンへ
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex == (int)STATE_SCENE.RANKING)
        {
            state_scene = STATE_SCENE.TITLE;
            SceneManager.LoadSceneAsync((int)STATE_SCENE.TITLE);
        }
        else
        {
            state_scene++;
            SceneManager.LoadSceneAsync(buildIndex + 1);
        }
    }
}
