using System.Collections;
using UnityEngine;

public class ToggleRagdoll : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Rigidbody characterRB;

    [SerializeField]
    private Rigidbody SpineRB;

    [SerializeField]
    private CapsuleCollider characterCollider;

    [SerializeField]
    private Collider[] RagDollcolliders;
    [SerializeField]
    private Rigidbody[] RagDollRigidbodies;

    [SerializeField]
    private Transform target;
    private Animator anim;


    private bool isRagdoll = false;

    private Vector3 Point;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundMask;

    public bool IsRagdollGrounded()                 // 래그돌 상태와 래그돌 바닥 감지 조건을 둘 다 만족할 때 
    {

        return isRagdoll && Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.3f, groundMask);

    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        Point = transform.position - target.position;

    }

    public void Toggle()
    {
        if (isRagdoll == false)
        {
            isRagdoll = true;
            anim.enabled = false;       // 래그돌 꺼져 있으면 래그돌 작동

            for (int i = 0; i < RagDollcolliders.Length; ++i)
            {
                RagDollcolliders[i].enabled = true;
            }

            for (int i = 0; i < RagDollRigidbodies.Length; ++i)
            {
                RagDollRigidbodies[i].isKinematic = false;
            }

            characterCollider.enabled = false;
            characterRB.isKinematic = true;


        }
        else if (isRagdoll == true)     // 래그돌 켜져 있으면 래그돌 off
        {
            isRagdoll = false;
            anim.enabled = true;

            transform.position = target.position + Point;
            transform.position += new Vector3(0, 0.25f, 0);
  

            for (int i = 0; i < RagDollcolliders.Length; ++i)
            {
                RagDollcolliders[i].enabled = false;
            }

            for (int i = 0; i < RagDollRigidbodies.Length; ++i)
            {
                RagDollRigidbodies[i].isKinematic = true;
            }

            characterCollider.enabled = true;
            characterRB.isKinematic = false;

        }
    }
    private void Test()
    {
        SpineRB.AddForce(new Vector3(0f, 10000f, 10000f));
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Toggle();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Test();
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * 0.3f);
    }
}
