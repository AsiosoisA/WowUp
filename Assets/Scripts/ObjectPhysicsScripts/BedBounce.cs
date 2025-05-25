using UnityEngine;

public class BedBounce : MonoBehaviour
{
    public float bounceForce = 5f;
    public float boostForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ���� �÷��̾� ���� ����(��������)
                Vector3 curDir = rb.linearVelocity.normalized;
                // ���� �ӵ� ���� (�������� �����ϰ� Ƣ�� �� ����)
                curDir.y = 0;
                // ���� ���� ���� : ���� ���� ���� + y������ x�� �߰�
                Vector3 boostDir = curDir + Vector3.up * bounceForce;
                // ���� �� ����
                // Optional : ���� �ӵ� ����
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.AddForce(boostDir.normalized * boostForce, ForceMode.Impulse);
            }
        }
    }
}
