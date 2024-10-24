using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class SetAge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "age : " + PlayerPrefs.GetFloat("age").ToString("F0");
        // ボードNo1にエイジを送信する。
        UnityroomApiClient.Instance.SendScore(1, PlayerPrefs.GetFloat("age"), ScoreboardWriteMode.HighScoreDesc);
    }
}
