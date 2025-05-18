using UnityEngine;

public class ElevatorFix : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position += Vector3.up * 5;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("pos : " + transform.position);
    }
}
