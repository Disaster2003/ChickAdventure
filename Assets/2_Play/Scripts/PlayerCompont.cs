using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCompont : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isJumped;

    private enum STATE_PLAYER
    {
        EGG,     // 卵
        BABY,    // 赤ちゃん
        GANG,    // ギャング
        MACHINE, // ロボ
        ADULT,   // にわとり
    }
    private STATE_PLAYER state_player;

    private InputControl IC;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        isJumped = false;

        state_player = STATE_PLAYER.EGG;

        IC = new InputControl(); // インプットアクションを取得
        IC.Player.Jump.started += OnJump; // 全てのアクションにイベントを登録
        IC.Enable(); // インプットアクションを機能させる為に有効化する。
    }

    // Update is called once per frame
    void Update()
    {
        switch (state_player)
        {
            case STATE_PLAYER.EGG:
                // 卵を転がす
                transform.Rotate(0, 0, 180 * Time.deltaTime);
                break;
            case STATE_PLAYER.BABY:
                break;
            case STATE_PLAYER.GANG:
                break;
            case STATE_PLAYER.MACHINE:
                break;
            case STATE_PLAYER.ADULT:
                break;
        }

        Land();
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void OnJump(InputAction.CallbackContext context)
    {
        // nullチェック
        if (rb == null) return;

        if (!isJumped)
        {
            // ジャンプ
            isJumped = true;
            rb.AddForce(10 * Vector3.up, ForceMode2D.Impulse);
            rb.gravityScale = 1;
        }
    }

    /// <summary>
    /// 着地処理
    /// </summary>
    private void Land()
    {
        if (transform.position.y < -3.5f)
        {
            // 着地
            isJumped = false;
            transform.position = new Vector3(transform.position.x, -3.5f);
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Rock"))
        {

        }
    }
}
