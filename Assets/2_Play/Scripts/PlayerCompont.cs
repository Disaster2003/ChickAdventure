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
        BORN,    // 生まれたて
        BABY,    // 赤ちゃん
        GANG,    // ギャング
        MACHINE, // ロボ
        ADULT,   // にわとり
    }
    private STATE_PLAYER state_player;

    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] egg;
    [SerializeField] Sprite[] born;
    [SerializeField] Sprite[] baby;
    [SerializeField] Sprite[] gang;
    [SerializeField] Sprite[] machine;
    [SerializeField] Sprite[] adult;
    private float intervalAnimation;

    private InputControl IC;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        isJumped = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        state_player = STATE_PLAYER.EGG;
        intervalAnimation = 0;

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

                Animation(egg);
                break;
            case STATE_PLAYER.BORN:
                Animation(born);
                break;
            case STATE_PLAYER.BABY:
                Animation(baby);
                break;
            case STATE_PLAYER.GANG:
                Animation(gang);
                break;
            case STATE_PLAYER.MACHINE:
                Animation(machine);
                break;
            case STATE_PLAYER.ADULT:
                Animation(adult);
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
        if (!isJumped) return;

        if (transform.position.y < -3.5f)
        {
            // 着地
            isJumped = false;
            transform.position = new Vector3(transform.position.x, -3.5f);
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// アニメーション処理
    /// </summary>
    /// <param name="_sprite">アニメーション画像</param>
    private void Animation(Sprite[] _sprite)
    {
        if (intervalAnimation > 0.2f)
        {
            intervalAnimation = 0;

            for (int i = 0; i < _sprite.Length; i++)
            {
                if (spriteRenderer.sprite == _sprite[i])
                {
                    if (i == _sprite.Length - 1)
                    {
                        // 最初の画像に戻す
                        spriteRenderer.sprite = _sprite[0];
                        return;
                    }
                    else
                    {
                        // 次の画像へ
                        spriteRenderer.sprite = _sprite[i + 1];
                        return;
                    }
                }
                else if (i == _sprite.Length - 1)
                {
                    // 画像を変更する
                    spriteRenderer.sprite = _sprite[0];
                }

            }
        }
        else
        {
            intervalAnimation += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Rock"))
        {

        }
    }
}
