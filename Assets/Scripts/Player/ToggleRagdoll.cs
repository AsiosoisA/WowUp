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

            transform.position += new Vector3(0, 0.25f, 0);

            transform.position = target.position + Point;


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
}
