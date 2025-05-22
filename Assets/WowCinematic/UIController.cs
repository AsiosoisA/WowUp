using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup uiGroup; // Logo + Button ���� �θ� �ִ� CanvasGroup
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private float FadeTime= 3f;
    
    // ĳ���� �������� ��ױ� ���� ĳ���� ���� ������Ʈ ���� �ʿ�



    void Start()
    {
        // ��ǲ ��� �ڵ� �߰� �ʿ�
        
        // UI �ʱ� ����
        uiGroup.alpha = 0;
        uiGroup.blocksRaycasts = false;

        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void ShowTitle()
    {
        StartCoroutine(FadeInUI());
    }

    IEnumerator FadeInUI()
    {
        float t = 0;
        while (t < FadeTime)
        {
            t += Time.deltaTime;
            uiGroup.alpha = t;
            yield return null;
        }
        uiGroup.blocksRaycasts = true; // ��ư Ŭ�� ���
    }

    private void OnStartButtonClicked()
    {
        StartCoroutine(FadeOutUI());
    }

    IEnumerator FadeOutUI()
    {
        uiGroup.blocksRaycasts = false;

        float t = FadeTime;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            uiGroup.alpha = t;
            yield return null;
        }

        // ��ǲ ����
        gameObject.SetActive(false); // UI ��Ȱ��ȭ
    }
}