using UnityEngine;

public class Slope : MonoBehaviour
{
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();

            rigid.useGravity = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();

            rigid.useGravity = true;
        }
    }
}
