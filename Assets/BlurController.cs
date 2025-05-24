using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class BlurController : MonoBehaviour
{
    [SerializeField] private Volume volume; // Post Process Volume
    private Vignette vignette;
    private FilmGrain filmGrain;
    [SerializeField] private float transitionDuration = 1.0f; // 전환 시간
    [SerializeField] private Vector2 changeIntervalRange = new Vector2(1.0f, 3.0f); // 랜덤 변경 간격 범위 (최소, 최대)
    [SerializeField] private Vector2 centerRange = new Vector2(0.4f, 0.6f); // Vignette Center 이동 범위 (x, y)

    private void Start()
    {
        // Volume에서 Vignette 효과 가져오기
        if (volume.profile.TryGet(out Vignette tempVignette))
        {
            vignette = tempVignette;
        }
        else
        {
            Debug.LogError("Vignette effect not found in Volume profile!");
            return;
        }

        // Volume에서 Film Grain 효과 가져오기
        if (volume.profile.TryGet(out FilmGrain tempFilmGrain))
        {
            filmGrain = tempFilmGrain;
        }
        else
        {
            Debug.LogError("Film Grain effect not found in Volume profile!");
            return;
        }

        // 초기 설정
        StartCoroutine(RandomBlurEffect());
    }

    private IEnumerator RandomBlurEffect()
    {
        while (true)
        {
            // Vignette: Intensity (0.3~0.5), Smoothness (0.5~1), Center (0.4~0.6) 랜덤 값 선택
            float targetVignetteIntensity = Random.Range(0.3f, 0.8f);
            float targetVignetteSmoothness = Random.Range(0.5f, 1f);
            Vector2 targetVignetteCenter = new Vector2(
                Random.Range(centerRange.x, centerRange.y),
                Random.Range(centerRange.x, centerRange.y)
            );
            // Film Grain: Intensity (0.5~1) 랜덤 값 선택
            float targetFilmGrainIntensity = Random.Range(0.5f, 1f);

            // 현재 값에서 목표 값으로 부드럽게 전환
            float startVignetteIntensity = vignette.intensity.value;
            float startVignetteSmoothness = vignette.smoothness.value;
            Vector2 startVignetteCenter = vignette.center.value;
            float startFilmGrainIntensity = filmGrain.intensity.value;
            float elapsed = 0f;

            while (elapsed < transitionDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / transitionDuration;
                vignette.intensity.value = Mathf.Lerp(startVignetteIntensity, targetVignetteIntensity, t);
                vignette.smoothness.value = Mathf.Lerp(startVignetteSmoothness, targetVignetteSmoothness, t);
                vignette.center.value = Vector2.Lerp(startVignetteCenter, targetVignetteCenter, t);
                filmGrain.intensity.value = Mathf.Lerp(startFilmGrainIntensity, targetFilmGrainIntensity, t);
                yield return null;
            }

            // 목표 값에 정확히 도달
            vignette.intensity.value = targetVignetteIntensity;
            vignette.smoothness.value = targetVignetteSmoothness;
            vignette.center.value = targetVignetteCenter;
            filmGrain.intensity.value = targetFilmGrainIntensity;

            // 랜덤한 변경 간격 대기 (1~3초)
            float waitTime = Random.Range(changeIntervalRange.x, changeIntervalRange.y);
            yield return new WaitForSeconds(waitTime);
        }
    }
}