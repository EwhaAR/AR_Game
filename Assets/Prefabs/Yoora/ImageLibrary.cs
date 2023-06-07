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

    //fadein,out 관련
    //fade 시간 inspector에서 설정 가능
    public float fadeTime = 1.5f;
    private float accumTime = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {



            UpdateImage(trackedImage);



        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name; // 추적 중인 이미지의 이름을 가져옴
        GameObject trackedUi = spawnedUis[name]; // 추적 중인 이미지와 연결된 UI를 가져옴
        CanvasGroup canvasGroup = trackedUi.GetComponent<CanvasGroup>();

        
            StartCoroutine(FadeIn(canvasGroup, 5f));
        
        
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
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime);
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;

        // Keep it visible for the specified wait time
        yield return new WaitForSeconds(waitTime);

        // Call the FadeOut coroutine
        StartCoroutine(FadeOut(canvasGroup));
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        accumTime = 0f;

        float originalAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        // Gradually fade out
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime);
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;
    }

}
