using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleAction : MonoBehaviour
{
    private enum STATE_HURDLE
    {
        ROCK, // 岩
        WOOD_LOW,
        WOOD_MIDDLE, // 木
    }
    [SerializeField] STATE_HURDLE state_hurdle;

    // Start is called before the first frame update
    void Start()
    {
        // 初期配置
        switch (state_hurdle)
        {
            case STATE_HURDLE.ROCK:
                transform.position = new Vector3(10, -3.5f);
                break;
            case STATE_HURDLE.WOOD_LOW:
                transform.position = new Vector3(10,-2.8f);
                break;
            case STATE_HURDLE.WOOD_MIDDLE:
                transform.position = new Vector3(10, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -10)
        {
            // 自身を破棄する
            Destroy(gameObject);
        }
        else
        {
            // 左に移動する
            transform.Translate(5 * -Time.deltaTime, 0, 0);
        }
    }
}
