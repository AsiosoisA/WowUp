using UnityEngine;
using UnityEngine.Playables;

public class TimelineSpeedControl : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private float GeneralSpeed = 1.0f;
    [SerializeField]
    private float SlowMotionSpeed = 0.3f;

    private bool isSlowMotion =false;

 
    void Start()
    {
        
    }



    public void ToggleSlowMotion()
    {
        
        if (isSlowMotion) // 슬로우모션 -> 일반 속도 
        {
            var root = director.playableGraph.GetRootPlayable(0);
            root.SetSpeed(GeneralSpeed);
            isSlowMotion = false;
            Debug.Log("슬로우 꺼짐");
        }
        else
        {
            // 일반 속도 -> 슬로우 모션
            var root = director.playableGraph.GetRootPlayable(0);
            root.SetSpeed(SlowMotionSpeed);
            isSlowMotion = true;
            Debug.Log("슬로우 켜짐");
        }

    }
}
