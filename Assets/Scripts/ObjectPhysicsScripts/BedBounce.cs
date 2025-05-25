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
                // 현재 플레이어 진행 방향(단위벡터)
                Vector3 curDir = rb.linearVelocity.normalized;
                // 수직 속도 제거 (수직으로 과도하게 튀는 것 방지)
                curDir.y = 0;
                // 힘을 가할 방향 : 현재 진행 방향 + y축으로 x배 추가
                Vector3 boostDir = curDir + Vector3.up * bounceForce;
                // 최종 힘 적용
                // Optional : 수직 속도 리셋
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.AddForce(boostDir.normalized * boostForce, ForceMode.Impulse);
            }
        }
    }
}
