using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgeCount : MonoBehaviour
{
    private float age; // �N��

    private float interval;
    [SerializeField] float changeAnimationTime;

    private PlayerComponent.STATE_PLAYER state_player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "age : 0";

        interval = 0; // �C���^�[�o���̏�����

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
                    // �o�ߎ��Ԃ̑���
                    interval = 0;
                    Time.timeScale++;
                }
                else
                {
                    // �C���^�[�o����
                    interval += Time.deltaTime;
                }
            }
            else
            {
                // �o�ߎ��Ԃ̑���
                Time.timeScale = 1 + age * 0.01f;
            }
            // �N�����
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
    /// �v���C���[�̏�Ԃ�ݒ肷��
    /// </summary>
    /// <param name="_state_player">���̃v���C���[�̏��</param>
    private void SetPlayerState(PlayerComponent.STATE_PLAYER _state_player)
    {
        if (interval > changeAnimationTime)
        {
            // �v���C���[�̏�Ԃ�؂�ւ���
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
