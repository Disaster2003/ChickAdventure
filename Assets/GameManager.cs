using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum STATE_SCENE
    {
        TITLE,  // �^�C�g�����
        EXPLAIN,// �������
        PLAY,   // �v���C���
        RANKING,// �����L���O���
    }
    private static STATE_SCENE state_scene;

    // Start is called before the first frame update
    void Start()
    {
        state_scene = STATE_SCENE.TITLE; // �V�[���̏�����
    }

    // Update is called once per frame
    void Update()
    {
        if(state_scene == STATE_SCENE.PLAY)
        {
            if(PlayerComponent.GetInstance().GetHp() <= 0)
            {
                // �����L���O��
                OnClick();
            }
        }
    }

    /// <summary>
    /// �{�^������
    /// </summary>
    public static void OnClick()
    {
        // ���̃V�[����
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
