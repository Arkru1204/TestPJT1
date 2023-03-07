using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;
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
        if (!playerControl.isDead) {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), )
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
