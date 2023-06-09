using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MiddleStage : MonoBehaviour
{
    public CanvasGroup[] panels; // UI �гε��� ���� �迭

    private int stage = 0; // ���� �������� ����
    private bool isStage2Activated = false; // stage 2���� Ȱ��ȭ�� �г� ���� Ȯ���� ���� ����

    private float fadeTime = 1.5f; // ���̵� �ð� ����
    private float accumTime; // ��� �ð�


    // #JES
    private GameObject dontDestroy;
    private int gameStage;
    private int stageStep;
    public GameSceneManager gameSceneManager;


    private bool panelOn = false;
    private CanvasGroup SelectedPanel;



    public void Start() {
        dontDestroy = GameObject.Find("DontDestroy");
        gameStage = dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage;
        stageStep = dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep;

        // Debug.Log("Stage: " + gameStage);
        // Debug.Log("- Step: " + stageStep);

        // Game Scene Manager
        gameSceneManager = FindObjectOfType<GameSceneManager>();


        // if (gameStage == 2 && !isStage2Activated)
        if (gameStage == 2)
        {
            ActivateRandomPanel(panels.Length);
            isStage2Activated = true;
        }
        // else if (gameStage == 3 && isStage2Activated)
        else if (gameStage == 3)
        {
            ActivateRandomPanel(panels.Length - 1, gameStage);
            isStage2Activated = false;
        }

    }



    private void Update()
    {
        // ��ġ �Է��� �����Ͽ� stage ���� ����

        // #JES: Use mouse in Unity Editor
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))  // if mouse left is clicked 
        {

            // if (ARTrackedMultiImageManager.imageRecognized)
            {
                // ARTrackedMultiImageManager.imageRecognized = false;
                dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep++;
                gameSceneManager.convertScene();
            }
        }
        #else
        // If image perception succeeded 
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (panelOn)
            {
                panelOn = false;
                StartCoroutine(FadeOut(SelectedPanel));
            }

            if (ARTrackedMultiImageManager.imageRecognized)
            {
                ARTrackedMultiImageManager.imageRecognized = false;    
                // 이미지가 인식되었을 경우 stage 변수 변경
                dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep++;
                gameSceneManager.convertScene();
            }
        }
        #endif



    }

    private void checkData()
    {

    }

    private void ActivateRandomPanel(int panelCount, int excludedPanelIndex = -1)
    {
        // ��� �г� ��Ȱ��ȭ
        foreach (CanvasGroup panel in panels)
        {
            panel.alpha = 0f;
        }

        // Ȱ��ȭ�� �г� �ε���
        int panelIndex;

        if (excludedPanelIndex != -1)
        {
            // ������ �г� �ε����� �����ϰ� ������ �г� Ȱ��ȭ
            panelIndex = Random.Range(0, excludedPanelIndex);
            if (panelIndex >= excludedPanelIndex)
            {
                panelIndex += 1;
            }
        }
        else
        {
            // ��� �г� �� ������ �г� Ȱ��ȭ
            panelIndex = Random.Range(0, panelCount);
        }

        // #JES: Save place data in DontDestroyOnLoad Object
        dontDestroy.GetComponent<DontDestroyOnLoad>().visitedPlaces[panelIndex] = 1;
        
        
        // ������ �гο� FadeIn �ִϸ��̼� ����
        CanvasGroup selectedPanel = panels[panelIndex];
        StartCoroutine(FadeIn(selectedPanel, 0f));
        panelOn = true;
        SelectedPanel = selectedPanel;
    }

   /* private void OnEnable()
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

            imageRecognized = true;

        }
    }*/

  


    private IEnumerator FadeIn(CanvasGroup canvasGroup, float waitTime)
    {
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;

        float originalAlpha = canvasGroup.alpha;
        float targetAlpha = 1f;

        // Gradually fade in
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime); // ���̵� �� �ִϸ��̼�
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;

        // Keep it visible for the specified wait time
        yield return new WaitForSeconds(waitTime);

    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        accumTime = 0f;

        float originalAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        // Gradually fade out
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime); // ���̵� �ƿ� �ִϸ��̼�
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;
    }
}
