using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    private static PlayerComponent instance; // クラスのインスタンス

    Vector3 inputMove; // 入力移動量の初期化

    private Rigidbody2D rb;
    private bool isJumped; // true = ジャンプした、false = ジャンプしていない

    private float hp;
    [SerializeField] float hpMax;
    [SerializeField] Image imgHpBar;

    public enum STATE_PLAYER
    {
        EGG,     // 卵
        BORN,    // 生まれたて
        BABY,    // 赤ちゃん
        GANG,    // ギャング
        NORMAL,  // ひよこ
        MACHINE, // ロボ
        ADULT,   // にわとり
        NONE,    // 未設定
    }
    private STATE_PLAYER state_player;

    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] egg;
    [SerializeField] Sprite[] born;
    [SerializeField] Sprite[] baby;
    [SerializeField] Sprite[] gang;
    [SerializeField] Sprite[] normal;
    [SerializeField] Sprite[] machine;
    [SerializeField] Sprite[] adult;
    private float intervalAnimation; // アニメーションのインターバル

    private InputControl IC;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            // Singleton
            instance = this;
        }

        transform.rotation = Quaternion.Euler(0, 180, 0);

        inputMove = Vector3.zero; // 入力移動量の初期化

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        isJumped = false;

        hp = hpMax;

        spriteRenderer = GetComponent<SpriteRenderer>();
        state_player = STATE_PLAYER.EGG; // プレイヤー状態の初期化
        intervalAnimation = 0;



        IC = new InputControl(); // インプットアクションを取得
        IC.Player.Move.started += OnMove; // 全てのアクションにイベントを登録
        IC.Player.Move.performed += OnMove;
        IC.Player.Move.canceled += OnMove;
        IC.Player.Jump.started += OnJump;
        IC.Enable(); // インプットアクションを機能させる為に有効化する。
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            // ランキングへ
            GameManager.OnClick();
            return;
        }

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
            case STATE_PLAYER.NORMAL:
                Animation(normal);
                break;
            case STATE_PLAYER.MACHINE:
                Animation(machine);
                break;
            case STATE_PLAYER.ADULT:
                Animation(adult);
                break;
        }

        Land();

        HpBar();

        if (inputMove.magnitude < 0.01f) return;
        // 左右移動
        transform.position += 5 * inputMove.normalized * Time.deltaTime;
    }

    /// <summary>
    /// インスタンスを取得する
    /// </summary>
    public static PlayerComponent GetInstance() { return instance; }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
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

        // nullチェック
        if (rb == null) return;

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
    /// Hpバー
    /// </summary>
    private void HpBar()
    {
        imgHpBar.fillAmount = hp / hpMax;
    }

    /// <summary>
    /// アニメーション処理
    /// </summary>
    /// <param name="_sprite">アニメーション画像</param>
    private void Animation(Sprite[] _sprite)
    {
        if (intervalAnimation > 0.2f)
        {
            // アニメーションのインターバルを初期化
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
            // アニメーションのインターバル中
            intervalAnimation += Time.deltaTime;
        }
    }

    /// <summary>
    /// プレイヤーの状態を切り替える
    /// </summary>
    /// <param name="_state_player">切り替え先のプレイヤーの状態</param>
    public void ChangePlayerState(STATE_PLAYER _state_player = STATE_PLAYER.NONE)
    {
        if(state_player == STATE_PLAYER.NONE)
        {
            Debug.LogError("プレイヤーの状態が未設定です");
            return;
        }

        if (state_player == STATE_PLAYER.BABY) transform.rotation = Quaternion.Euler(Vector3.zero);
        state_player = _state_player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Rock") || collision.name.Contains("Wood"))
        {
            hp--;
        }
    }
}
