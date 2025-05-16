using UnityEngine;

public class MyBall : MonoBehaviour
{
    Rigidbody rigid;

    public float normalSpeed = 10f;
    public float windSpeed = 1f;
    public float jumpForce = 20f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;
    public float maxSpeed = 3f;

    private bool isGrounded;
    private float currentSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        // # 점프 && 더블 점프 방지
        isGrounded = Physics.Raycast(transform.position, Vector3.down,
            groundCheckDistance, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        // # 힘을 가하기
        Vector3 vec = new Vector3(
            Input.GetAxisRaw("Horizontal") * currentSpeed,
            0, Input.GetAxisRaw("Vertical") * currentSpeed
        );
        rigid.AddForce(vec * Time.deltaTime, ForceMode.Impulse);
        // rigid.linearVelocity = vec;

        // # 가속도 제한
        if (Mathf.Abs(rigid.linearVelocity.x) > normalSpeed * maxSpeed)
        {
            rigid.linearVelocity = new Vector3(
                Mathf.Sign(rigid.linearVelocity.x) * normalSpeed * maxSpeed,
                rigid.linearVelocity.y,
                rigid.linearVelocity.z);
        }
        if (Mathf.Abs(rigid.linearVelocity.z) > normalSpeed * maxSpeed)
        {
            rigid.linearVelocity = new Vector3(
                rigid.linearVelocity.x,
                rigid.linearVelocity.y,
                Mathf.Sign(rigid.linearVelocity.z) * normalSpeed * maxSpeed);
        }

        // # 회전력
        // rigid.AddTorque(Vector3.back);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Cube")
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {

    }
    public void Jump()
    {
        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void enterWindZone()
    {
        currentSpeed = windSpeed;
    }

    public void exitWindZone()
    {
        currentSpeed = normalSpeed;
    } 
}
