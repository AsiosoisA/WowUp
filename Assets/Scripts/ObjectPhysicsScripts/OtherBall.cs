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

    // # �浹�� �Ͼ�� ��
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
