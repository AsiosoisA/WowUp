using UnityEngine;
using UnityEngine.Rendering;

using System.Collections;
using UnityEngine.Rendering.Universal;

public class SunDizzyController : MonoBehaviour
{
    [SerializeField] private Volume volume; // Post Process Volume (Box Volume)
    [SerializeField] private Transform targetObject; // 위치를 추적할 오브젝트 (예: 플레이어 또는 카메라)
    [SerializeField] private Vector2 focusDistanceRange = new Vector2(0.1f, 10f); // Depth of Field의 Focus Distance 범위 (최소, 최대)
    [SerializeField] private float effectCurvePower = 2f; // 비선형 곡선 강도 (제곱 곡선)
    private ChromaticAberration chromaticAberration;
    private DepthOfField depthOfField;

    private void Start()
    {
        // Volume에서 Chromatic Aberration 효과 가져오기
        if (volume.profile.TryGet(out ChromaticAberration tempChromatic))
        {
            chromaticAberration = tempChromatic;
        }
        else
        {
            Debug.LogError("Chromatic Aberration effect not found in Volume profile!");
            enabled = false;
            return;
        }

        // Volume에서 Depth of Field 효과 가져오기
        if (volume.profile.TryGet(out DepthOfField tempDepthOfField))
        {
            depthOfField = tempDepthOfField;
            // Gaussian 모드 설정 (Low-Poly 게임에 적합)
            depthOfField.mode.value = DepthOfFieldMode.Gaussian;
        }
        else
        {
            Debug.LogError("Depth of Field effect not found in Volume profile!");
            enabled = false;
            return;
        }

        // targetObject가 null인 경우 Main Camera를 기본으로 사용
        if (targetObject == null)
        {
            targetObject = Camera.main.transform;
            if (targetObject == null)
            {
                Debug.LogError("No target object or Main Camera found!");
                enabled = false;
                return;
            }
        }
    }

    private void Update()
    {
        // Box Volume의 BoxCollider 가져오기
        BoxCollider boxCollider = volume.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("Box Volume requires a BoxCollider!");
            return;
        }

        // 대상 오브젝트의 월드 좌표를 Box Volume의 로컬 좌표로 변환
        Vector3 localPosition = volume.transform.InverseTransformPoint(targetObject.position);

        // Box Volume의 Y축 범위 계산 (Local Space)
        float halfHeight = boxCollider.size.y / 2f;
        float minY = -halfHeight;
        float maxY = halfHeight;

        // Y축 위치를 0~1로 정규화
        float normalizedY = Mathf.InverseLerp(minY, maxY, localPosition.y);

        // 비선형 곡선 적용 (제곱 곡선으로 위쪽에서 효과가 강해짐)
        float curvedY = Mathf.Pow(normalizedY, effectCurvePower);

        // Chromatic Aberration의 Intensity 설정 (0~1, 아래쪽에서 미비)
        chromaticAberration.intensity.value = curvedY;

        // Depth of Field의 Focus Distance 설정 (최상단: 0.1, 최하단: 10)
        // 높이 올라갈수록 Focus Distance 감소 (주변 오브젝트가 더 흐려짐)
        float targetFocusDistance = Mathf.Lerp(focusDistanceRange.y, focusDistanceRange.x, curvedY);
        depthOfField.gaussianStart.value = targetFocusDistance;
        depthOfField.gaussianEnd.value = targetFocusDistance + 2f; // Gaussian 블러 범위 조정
        depthOfField.gaussianMaxRadius.value = curvedY * 2f; // 블러 강도 증가 (0~2)
    }
}