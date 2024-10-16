using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    private static PlayerComponent instance; // �N���X�̃C���X�^���X

    Vector3 inputMove; // ���͈ړ��ʂ̏�����

    private Rigidbody2D rb;
    private bool isJumped; // true = �W�����v�����Afalse = �W�����v���Ă��Ȃ�

    private float hp;
    [SerializeField] float hpMax;
    [SerializeField] Image imgHpBar;

    public enum STATE_PLAYER
    {
        EGG,     // ��
        BORN,    // ���܂ꂽ��
        BABY,    // �Ԃ����
        GANG,    // �M�����O
        NORMAL,  // �Ђ悱
        MACHINE, // ���{
        ADULT,   // �ɂ�Ƃ�
        NONE,    // ���ݒ�
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
    private float intervalAnimation; // �A�j���[�V�����̃C���^�[�o��

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

        inputMove = Vector3.zero; // ���͈ړ��ʂ̏�����

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        isJumped = false;

        hp = hpMax;

        spriteRenderer = GetComponent<SpriteRenderer>();
        state_player = STATE_PLAYER.EGG; // �v���C���[��Ԃ̏�����
        intervalAnimation = 0;



        IC = new InputControl(); // �C���v�b�g�A�N�V�������擾
        IC.Player.Move.started += OnMove; // �S�ẴA�N�V�����ɃC�x���g��o�^
        IC.Player.Move.performed += OnMove;
        IC.Player.Move.canceled += OnMove;
        IC.Player.Jump.started += OnJump;
        IC.Enable(); // �C���v�b�g�A�N�V�������@�\������ׂɗL��������B
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            // �����L���O��
            GameManager.OnClick();
            return;
        }

        switch (state_player)
        {
            case STATE_PLAYER.EGG:
                // ����]����
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
        // ���E�ړ�
        transform.position += 5 * inputMove.normalized * Time.deltaTime;
    }

    /// <summary>
    /// �C���X�^���X���擾����
    /// </summary>
    public static PlayerComponent GetInstance() { return instance; }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    private void OnJump(InputAction.CallbackContext context)
    {
        // null�`�F�b�N
        if (rb == null) return;

        if (!isJumped)
        {
            // �W�����v
            isJumped = true;
            rb.AddForce(10 * Vector3.up, ForceMode2D.Impulse);
            rb.gravityScale = 1;
        }
    }

    /// <summary>
    /// ���n����
    /// </summary>
    private void Land()
    {
        if (!isJumped) return;

        // null�`�F�b�N
        if (rb == null) return;

        if (transform.position.y < -3.5f)
        {
            // ���n
            isJumped = false;
            transform.position = new Vector3(transform.position.x, -3.5f);
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Hp�o�[
    /// </summary>
    private void HpBar()
    {
        imgHpBar.fillAmount = hp / hpMax;
    }

    /// <summary>
    /// �A�j���[�V��������
    /// </summary>
    /// <param name="_sprite">�A�j���[�V�����摜</param>
    private void Animation(Sprite[] _sprite)
    {
        if (intervalAnimation > 0.2f)
        {
            // �A�j���[�V�����̃C���^�[�o����������
            intervalAnimation = 0;

            for (int i = 0; i < _sprite.Length; i++)
            {
                if (spriteRenderer.sprite == _sprite[i])
                {
                    if (i == _sprite.Length - 1)
                    {
                        // �ŏ��̉摜�ɖ߂�
                        spriteRenderer.sprite = _sprite[0];
                        return;
                    }
                    else
                    {
                        // ���̉摜��
                        spriteRenderer.sprite = _sprite[i + 1];
                        return;
                    }
                }
                else if (i == _sprite.Length - 1)
                {
                    // �摜��ύX����
                    spriteRenderer.sprite = _sprite[0];
                }

            }
        }
        else
        {
            // �A�j���[�V�����̃C���^�[�o����
            intervalAnimation += Time.deltaTime;
        }
    }

    /// <summary>
    /// �v���C���[�̏�Ԃ�؂�ւ���
    /// </summary>
    /// <param name="_state_player">�؂�ւ���̃v���C���[�̏��</param>
    public void ChangePlayerState(STATE_PLAYER _state_player = STATE_PLAYER.NONE)
    {
        if(state_player == STATE_PLAYER.NONE)
        {
            Debug.LogError("�v���C���[�̏�Ԃ����ݒ�ł�");
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
