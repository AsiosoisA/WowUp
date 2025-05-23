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
       
            vcam.HorizontalAxis.Value += speed * Time.deltaTime;
        
        
        
    }
    
 
}
