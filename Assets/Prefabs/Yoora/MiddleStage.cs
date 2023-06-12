using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStage : MonoBehaviour
{
    public CanvasGroup[] panels; // UI 패널들을 담을 배열

    private int stage = 0; // 현재 스테이지 변수
    private bool isStage2Activated = false; // stage 2에서 활성화된 패널 여부 확인을 위한 변수

    private float fadeTime = 1f; // 페이드 시간 설정
    private float accumTime; // 경과 시간

    private void Update()
    {
        // 터치 입력을 감지하여 stage 변수 증가
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            stage++;
        }

        if (stage == 2 && !isStage2Activated)
        {
            ActivateRandomPanel(panels.Length);
            isStage2Activated = true;
        }
        else if (stage == 3 && isStage2Activated)
        {
            ActivateRandomPanel(panels.Length - 1, stage);
            isStage2Activated = false;
        }
    }

    private void ActivateRandomPanel(int panelCount, int excludedPanelIndex = -1)
    {
        // 모든 패널 비활성화
        foreach (CanvasGroup panel in panels)
        {
            panel.alpha = 0f;
        }

        // 활성화할 패널 인덱스
        int panelIndex;

        if (excludedPanelIndex != -1)
        {
            // 제외할 패널 인덱스를 제외하고 랜덤한 패널 활성화
            panelIndex = Random.Range(0, excludedPanelIndex);
            if (panelIndex >= excludedPanelIndex)
            {
                panelIndex += 1;
            }
        }
        else
        {
            // 모든 패널 중 랜덤한 패널 활성화
            panelIndex = Random.Range(0, panelCount);
        }

        // 선택한 패널에 FadeIn 애니메이션 실행
        CanvasGroup selectedPanel = panels[panelIndex];
        StartCoroutine(FadeIn(selectedPanel, 0f));
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float waitTime)
    {
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
