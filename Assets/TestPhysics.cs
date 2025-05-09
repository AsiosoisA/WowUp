using System.Collections;
using UnityEngine;

public class TestPhysics : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Rigidbody rb;
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

    private void ToggleRagdoll()
    {
        if(isRagdoll == false) 
        {
            isRagdoll=true;
            anim.enabled = false;       // 래그돌 꺼져 있으면 래그돌 작동

            for(int i = 0; i < RagDollcolliders.Length; ++i)
            {
                RagDollcolliders[i].enabled = true;
            }
            

            
        }
        else if(isRagdoll == true)
        {
            isRagdoll=false;
            transform.position = target.position + Point;
            anim.enabled = true;

            transform.position += new Vector3(0, 0.25f, 0);
            anim.SetTrigger("bstand");

            for (int i = 0; i < RagDollcolliders.Length; ++i)
            {
                RagDollcolliders[i].enabled = false;
            }

        }
    }
    private void Test()
    {
        rb.AddForce(new Vector3(0f, 10000f, 10000f));
    }

    public void StartWakeUp()
    {

        Debug.Log("실행되었다");
    }
    public void EndWakeUp()
    {
        transform.position -= new Vector3(0, 0.25f, 0);
    }


    IEnumerator EnableRagdoll()
    {

        anim.enabled = false;
        rb.AddForce(new Vector3(0f, 10000f, 10000f));
        yield return new WaitForSeconds(5.0f);
        transform.position = target.position + Point;
        anim.enabled = true;

    }
    IEnumerator EnableRagdoll2()
    {

        anim.enabled = false;

        yield return new WaitForSeconds(5.0f);
        transform.position = target.position + Point;
        anim.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) {

            anim.SetTrigger("Next");

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ToggleRagdoll();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Test();
        }
      
    }
}
