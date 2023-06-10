using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageLibrary : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private GameObject[] uiObjects; // 각 이미지에 대응하는 비활성화된 UI 게임 오브젝트들을 저장할 배열
    private Dictionary<string, GameObject> spawnedUis = new Dictionary<string, GameObject>(); // 추적하고 있는 이미지와 연결된 게임 오브젝트들을 담는 딕셔너리
    private HashSet<string> detectedImages = new HashSet<string>(); // 이미지를 인식한 이미지의 이름을 저장하는 집합

    //fadein,out 관련
    //fade 시간 inspector에서 설정 가능
    public float fadeTime = 1.5f;
    private float accumTime = 0f;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; // 이미지 추적 상태 변화 이벤트에 대한 핸들러 등록
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; // 이미지 추적 상태 변화 이벤트 핸들러 제거
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if (!detectedImages.Contains(trackedImage.referenceImage.name))
            {
                detectedImages.Add(trackedImage.referenceImage.name); // 이미지 이름을 detectedImages 집합에 추가
                UpdateImage(trackedImage); // 이미지 업데이트 함수 호출
            }
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name; // 추적 중인 이미지의 이름을 가져옴
        GameObject trackedUi = spawnedUis[name]; // 추적 중인 이미지와 연결된 UI를 가져옴
        CanvasGroup canvasGroup = trackedUi.GetComponent<CanvasGroup>();

        StartCoroutine(FadeIn(canvasGroup, 5f)); // 페이드 인 코루틴 실행
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float waitTime)
    {
        //missionUISound.Play();
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;

        float originalAlpha = canvasGroup.alpha;
        float targetAlpha = 1f;

        // Gradually fade in
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime); // 페이드 인 애니메이션
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;

        // Keep it visible for the specified wait time
        yield return new WaitForSeconds(waitTime);

        // Call the FadeOut coroutine
        StartCoroutine(FadeOut(canvasGroup)); // 페이드 아웃 코루틴 실행
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        accumTime = 0f;

        float originalAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        // Gradually fade out
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime); // 페이드 아웃 애니메이션
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;
    }
}
