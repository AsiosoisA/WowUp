using UnityEngine;

public class Cheese : MonoBehaviour
{
    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dish"))
        {
            rigid.useGravity = true;
        }
    }
}
