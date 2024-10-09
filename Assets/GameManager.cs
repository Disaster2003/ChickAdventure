using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum STATE_SCENE
    {
        TITLE,
        EXPLAIN,
        PLAY,
        RANKING,
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneManager.LoadSceneAsync((int)STATE_SCENE.TITLE);
        }
        else
        {
            SceneManager.LoadSceneAsync(buildIndex + 1);
        }
    }
}
