using Unity.Cinemachine;
using UnityEngine;

public class CircleCamera : MonoBehaviour
{
    [SerializeField]
    private  CinemachineOrbitalFollow vcam;
    [SerializeField]
    private float speed = 20f;
    

    void Update()
    {
        if(vcam.HorizontalAxis.Value <= 360)
        {
            vcam.HorizontalAxis.Value += speed * Time.deltaTime;
        }
        
        
    }
    
 
}
