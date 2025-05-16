using UnityEngine;

public class OtherBall : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    // # 충돌이 일어났을 때
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MySphere")
            mat.color = new Color(0, 0, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MySphere")
            mat.color = new Color(1, 1, 1);
    }
}
