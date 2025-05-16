using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        Quaternion delatRotation = Quaternion.Euler(0f, 0f, rotationSpeed * Time.fixedDeltaTime);
        rigid.MoveRotation(rigid.rotation * delatRotation);
    }
}
