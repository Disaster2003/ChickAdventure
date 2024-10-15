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
        BABY,    // �Ԃ����
        GANG,    // �M�����O
        MACHINE, // ���{
        ADULT,   // �ɂ�Ƃ�
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
        if (transform.position.y < -3.5f)
        {
            // ���n
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
