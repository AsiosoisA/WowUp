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
        
        if (isSlowMotion) // ���ο��� -> �Ϲ� �ӵ� 
        {
            var root = director.playableGraph.GetRootPlayable(0);
            root.SetSpeed(GeneralSpeed);
            isSlowMotion = false;
            Debug.Log("���ο� ����");
        }
        else
        {
            // �Ϲ� �ӵ� -> ���ο� ���
            var root = director.playableGraph.GetRootPlayable(0);
            root.SetSpeed(SlowMotionSpeed);
            isSlowMotion = true;
            Debug.Log("���ο� ����");
        }

    }
}
