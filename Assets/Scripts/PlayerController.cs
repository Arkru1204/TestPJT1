using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;
    private Vector3 moveDir;

    private Transform cameraArm;
    private Transform playerBody;
    private GameObject player;

    public float cameraSpeed = 2.0f;
    public float playerSpeed = 5.5f;
    public float jumpPower = 10.0f;

    // ���콺 �����ӿ� ���� ȭ�� ȸ��
    private void CameraRotate() {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * cameraSpeed, Input.GetAxis("Mouse Y") * cameraSpeed);
        Vector3 cameraAngle = cameraArm.rotation.eulerAngles; // ī�޶��� rotation ���� ���Ϸ� ������ �ٲ�

        float x = cameraAngle.x - mouseDelta.y;

        // ī�޶� ���� ���� 70��, �Ʒ��� 25�� ����
        if (x < 180f) { x = Mathf.Clamp(x, -1.0f, 70.0f); }
        else { x = Mathf.Clamp(x, 335f, 361f); }

        cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
    }

    // WASD �̵� (ī�޶� ���� ���� ����)
    private void Move()
    {
        if (!PlayerMain.isDead) {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;
            animator.SetBool("isMove", isMove);

            if (isMove && !PlayerMain.isJump) {
                Vector3 lookDir = new Vector3(cameraArm.forward.x, 0.0f, cameraArm.forward.z);
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0.0f, cameraArm.right.z).normalized;
                moveDir = (lookDir * moveInput.y) + (lookRight * moveInput.x);

                playerBody.forward = moveDir;
            }

            transform.position += moveDir * Time.deltaTime * playerSpeed;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!PlayerMain.isJump) {
				PlayerMain.isJump = true;
				rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else { return; }
        }
    }

    //GameManager�� �и�����
    private void Dead() {
        player.SetActive(false);
		PlayerMain.isDead = true;

        // 5�ʵ� ��Ȱ - ��ư ������ ��Ȱ�ϰ� �ٲܱ�?
        Invoke("Respawn", 5);
    }

    private void Respawn() {
		PlayerMain.isDead = false;
        //�÷��̾� ���൵�� ���� ���� ������ ���(�ϵ��ڵ�X)�� �̵�
        //�ӽ÷� 0 0 0���� �̵��ϰ� �ص�
        transform.position = new Vector3(0, 0, 0);

        //����?
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = playerBody.GetComponent<Animator>();
		rigid = playerBody.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotate();
        Move();
        Jump();

        if(PlayerMain.isDead) { Dead(); }
    }
}
