using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class PopUpTitle : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup uiGroup; // Logo + Button 묶인 부모에 있는 CanvasGroup
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private float FadeTime= 3f;

    private bool isTitleOn = false;
    
    // 캐릭터 움직임을 잠그기 위한 캐릭터 게임 오브젝트 변수 필요



    void Start()
    {
        // 인풋 잠금 코드 추가 필요
        
        // UI 초기 상태
        uiGroup.alpha = 0;
        uiGroup.blocksRaycasts = false;

        startButton.onClick.AddListener(OnStartButtonClicked);
        
        
    }

    public void ReceivePopUpTitleSignal()
    {
        StartCoroutine(FadeInUI());
    }

    IEnumerator FadeInUI()
    {
        if(isTitleOn) 
        {
            yield break;
        }
        float t = 0;
        while (t < FadeTime)
        {
            t += Time.deltaTime;
            uiGroup.alpha = t;
            yield return null;
        }
        uiGroup.blocksRaycasts = true; // 버튼 클릭 허용
        isTitleOn = true;
        
    }
    public void ReceiveFadeOuTitleSignal()
    {
        Debug.Log("Fade Out 실행 ");
        StartCoroutine(FadeOutUI());
    }

    private void OnStartButtonClicked()
    {
        StartCoroutine(FadeOutUI());
    }

    IEnumerator FadeOutUI()
    {
        if(!isTitleOn)
        {
            yield break;
        }
        uiGroup.blocksRaycasts = false;

        float t = FadeTime;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            uiGroup.alpha = t;
            yield return null;
        }

        // 인풋 해제
        gameObject.SetActive(false); // UI 비활성화
        isTitleOn = false;
    }
}