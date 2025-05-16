using UnityEngine;

public class ArrowRotate : MonoBehaviour
{
    public float rotationAngle = 30f;

    void Update()
    {
        transform.Rotate(rotationAngle * Time.deltaTime, 0, 0);       
    }
}
