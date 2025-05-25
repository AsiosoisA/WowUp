using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Rigidbody myRigidbody;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(
            Input.GetAxis("Horizontal") * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime, 0));
    }
}