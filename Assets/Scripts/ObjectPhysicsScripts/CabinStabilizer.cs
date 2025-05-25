using UnityEngine;

public class CabinStabilizer : MonoBehaviour
{
    void Start()
    {
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.up = Vector3.up;
    }
}
