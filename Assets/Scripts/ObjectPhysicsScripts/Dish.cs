using UnityEngine;

public class Dish : MonoBehaviour
{
    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rigid.isKinematic = false;

            BoxCollider[] colliders = GetComponents<BoxCollider>();
            foreach (BoxCollider col in colliders)
            {
                col.isTrigger = false;
            }
        }
    }

}
