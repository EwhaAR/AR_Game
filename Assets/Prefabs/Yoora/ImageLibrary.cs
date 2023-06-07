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
    private GameObject[] uiObjects; // �� �̹����� �����ϴ� ��Ȱ��ȭ�� UI ���� ������Ʈ���� ������ �迭
    private Dictionary<string, GameObject> spawnedUis = new Dictionary<string, GameObject>(); // �����ϰ� �ִ� �̹����� ����� ���� ������Ʈ���� ��� ��ųʸ�

    //fadein,out ����
    //fade �ð� inspector���� ���� ����
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
        string name = trackedImage.referenceImage.name; // ���� ���� �̹����� �̸��� ������
        GameObject trackedUi = spawnedUis[name]; // ���� ���� �̹����� ����� UI�� ������
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
