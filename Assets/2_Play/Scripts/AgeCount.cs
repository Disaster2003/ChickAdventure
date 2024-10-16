using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgeCount : MonoBehaviour
{
    private float age; // 年齢

    private float interval;
    [SerializeField] float changeAnimationTime;

    private PlayerComponent.STATE_PLAYER state_player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "age : 0";

        interval = 0; // インターバルの初期化

        state_player = PlayerComponent.STATE_PLAYER.EGG;
    }

    // Update is called once per frame
    void Update()
    {
        if (state_player != PlayerComponent.STATE_PLAYER.EGG)
        {
            if (age >= 100)
            {
                if (interval >= 100)
                {
                    // 経過時間の増量
                    interval = 0;
                    Time.timeScale++;
                }
                else
                {
                    // インターバル中
                    interval += Time.deltaTime;
                }
            }
            else
            {
                // 経過時間の増量
                Time.timeScale = 1 + age * 0.01f;
            }
            // 年を取る
            age += Time.deltaTime;
            GetComponent<Text>().text = "age : " + age.ToString("F1");
        }

        switch (state_player)
        {
            case PlayerComponent.STATE_PLAYER.EGG:
                SetPlayerState(PlayerComponent.STATE_PLAYER.BORN);
                break;
            case PlayerComponent.STATE_PLAYER.BORN:
                SetPlayerState(PlayerComponent.STATE_PLAYER.BABY);
                break;
            case PlayerComponent.STATE_PLAYER.BABY:
                SetPlayerState(PlayerComponent.STATE_PLAYER.GANG);
                break;
            case PlayerComponent.STATE_PLAYER.GANG:
                SetPlayerState(PlayerComponent.STATE_PLAYER.NORMAL);
                break;
            case PlayerComponent.STATE_PLAYER.NORMAL:
                SetPlayerState(PlayerComponent.STATE_PLAYER.MACHINE);
                break;
            case PlayerComponent.STATE_PLAYER.MACHINE:
                SetPlayerState(PlayerComponent.STATE_PLAYER.ADULT);
                break;
            case PlayerComponent.STATE_PLAYER.ADULT:
                break;
        }
    }

    /// <summary>
    /// プレイヤーの状態を設定する
    /// </summary>
    /// <param name="_state_player">次のプレイヤーの状態</param>
    private void SetPlayerState(PlayerComponent.STATE_PLAYER _state_player)
    {
        if (interval > changeAnimationTime)
        {
            // プレイヤーの状態を切り替える
            interval = 0;
            state_player = _state_player;
            PlayerComponent.GetInstance().ChangePlayerState(state_player);
        }
        else
        {
            interval += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("age", age);
    }
}
