using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExplainControl : MonoBehaviour
{
    private Text txtTurorialMessage;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        txtTurorialMessage = GetComponent<Text>();
        txtTurorialMessage.text = "Space‚ÅƒWƒƒƒ“ƒv!!";
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        timer += -Time.deltaTime;

        txtTurorialMessage.color = Color.Lerp(txtTurorialMessage.color, Color.clear, 0.5f * Time.deltaTime);
    }
}
