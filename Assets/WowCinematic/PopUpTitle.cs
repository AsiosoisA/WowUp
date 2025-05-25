using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class PopUpTitle : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup uiGroup; // Logo + Button ���� �θ� �ִ� CanvasGroup

    [SerializeField]
    private float FadeTime= 3f;

    private bool isTitleOn = false;
    
    // ĳ���� �������� ��ױ� ���� ĳ���� ���� ������Ʈ ���� �ʿ�



    void Start()
    {
        // ��ǲ ��� �ڵ� �߰� �ʿ�
        
        // UI �ʱ� ����
        uiGroup.alpha = 0;
        uiGroup.blocksRaycasts = false;  
        
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
        isTitleOn = true;
        while (t < FadeTime)
        {
            t += Time.deltaTime;
            uiGroup.alpha = t;
            yield return null;
        }
        uiGroup.blocksRaycasts = true; // ��ư Ŭ�� ���
        
        
    }
    public void ReceiveFadeOuTitleSignal()
    {
        Debug.Log("Fade Out ���� ");
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

        // ��ǲ ���� �ڵ� �߰� �ʿ� 
        isTitleOn = false;
        gameObject.SetActive(false); // UI ��Ȱ��ȭ
        
    }
}