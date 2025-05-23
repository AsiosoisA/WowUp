using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CharControl : MonoBehaviour
{
    public static CharControl Instance;

    private PlayableDirector pd;
    public TimelineAsset[] ta;

    void Start()
    {
        Instance = this;
        pd = GetComponent<PlayableDirector>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CutScene")
        {
            other.gameObject.SetActive(false);
            pd.Play(ta[0]);
        }
    }
}