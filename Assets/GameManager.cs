using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum STATE_SCENE
    {
        TITLE,
        EXPLAIN,
        PLAY,
        RANKING,
    }
    private STATE_SCENE state_scene;

    // Start is called before the first frame update
    void Start()
    {
        state_scene = STATE_SCENE.TITLE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �{�^������
    /// </summary>
    public void OnClick()
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
