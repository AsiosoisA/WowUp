using Unity.Cinemachine;
using UnityEngine;

public class RagdollCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera camera1;
    [SerializeField]
    private ToggleRagdoll TRD;
    [SerializeField]
    private Transform Hips;
    [SerializeField]
    private Transform WowRigFix;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TRD.isRagdoll)
        {
            camera1.Follow = Hips;
        }
        else camera1.Follow = WowRigFix;
        
    }
}
