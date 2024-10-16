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
        EGG,     // ��
        BORN,    // ���܂ꂽ��
        BABY,    // �Ԃ����
        GANG,    // �M�����O
        MACHINE, // ���{
        ADULT,   // �ɂ�Ƃ�
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

        IC = new InputControl(); // �C���v�b�g�A�N�V�������擾
        IC.Player.Jump.started += OnJump; // �S�ẴA�N�V�����ɃC�x���g��o�^
        IC.Enable(); // �C���v�b�g�A�N�V�������@�\������ׂɗL��������B
    }

    // Update is called once per frame
    void Update()
    {
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
    /// �A�j���[�V��������
    /// </summary>
    /// <param name="_sprite">�A�j���[�V�����摜</param>
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
