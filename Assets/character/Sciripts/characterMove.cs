using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMove : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    private characterState state;
    public Camera charactercamera;
    // Start is called before the first frame update

    public float gravity = -9.81f;
    private float pressStartTime;
    private Vector3 velocity;
    private float speed = 0.0f;
    // <summary><相机>
    public float distance = 3.0f; // 相机距离
    public float height = 1.5f;  // 相机高度
    public float sensitivity = 2.0f; // 旋转灵敏度
    public float minY = -20f, maxY = 60f; // 俯仰角限制

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    // </summary>

    void Start()
    {
        state = GetComponent<characterState>();
    }


    void Update()
    {
        CameraControl();
        Move();
        Dodge();
    }
    public void CameraControl()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minY, maxY); // 限制角度

        // 计算相机位置
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 position = transform.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // 应用位置和旋转
        charactercamera.transform.position = position;
        charactercamera.transform.LookAt(transform.position + Vector3.up * height);
    }
    public void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pressStartTime = Time.time;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pressStartTime = Time.time - pressStartTime;
            if (pressStartTime < 0.2f && speed < 0.001)
            {

                animator.SetTrigger("dodgeback");


            }
            else if(pressStartTime < 0.6f)
                animator.SetTrigger("roll");

        }
    }
    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        var right = charactercamera.transform.right;
        var forward = Vector3.Normalize(Vector3.Cross(right, Vector3.up));
        Vector3 move =  right * moveX +  forward* moveZ;

        if (!Input.GetKey(KeyCode.LeftShift)) move /= 2;
        speed = move.magnitude;
        if (speed > 0.001)
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);

        animator.SetFloat("speed", speed, 0.1f, Time.deltaTime);
        if (move.magnitude > 0.1f)
        {

            Quaternion targetRotation = Quaternion.LookRotation(move.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.5f);
        }
        //characterController.Move(move * state.maxSpeed * Time.deltaTime);

        bool isGrounded = characterController.isGrounded;
        if (isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (speed > 0.9 && Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("runingtoruningslide");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (speed > 0.9)
                animator.SetTrigger("runingtojump");
            else
                animator.SetTrigger("standtojump");
        }
        characterController.Move(velocity * Time.deltaTime);


    }
    public void onDodgeStart()
    {

    }
    public void onDodgeEnd()
    {

    }
    public void onJumpingStart()
    {
        velocity.y = Mathf.Sqrt(state.jumpHeight* 1.2f * -2f * gravity); // 计算初速度
    }
    public void onBigJumpingStart()
    {
        velocity.y = Mathf.Sqrt(state.jumpHeight* 1.2f * -2f * gravity); // 计算初速度
    }
}
