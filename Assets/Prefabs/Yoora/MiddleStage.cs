using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStage : MonoBehaviour
{
    public CanvasGroup[] panels; // UI �гε��� ���� �迭

    private int stage = 0; // ���� �������� ����
    private bool isStage2Activated = false; // stage 2���� Ȱ��ȭ�� �г� ���� Ȯ���� ���� ����

    private float fadeTime = 1f; // ���̵� �ð� ����
    private float accumTime; // ��� �ð�

    private void Update()
    {
        // ��ġ �Է��� �����Ͽ� stage ���� ����

        // #JES: Use mouse in Unity Editor
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))  // if mouse left is clicked 
        {
            stage++;
            Debug.Log("Stage " + stage);
        }
        #else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            stage++;
        }
        #endif

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

        // ������ �гο� FadeIn �ִϸ��̼� ����
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
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime); // ���̵� �� �ִϸ��̼�
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;

        // Keep it visible for the specified wait time
        yield return new WaitForSeconds(waitTime);

        // Call the FadeOut coroutine
        StartCoroutine(FadeOut(canvasGroup)); // ���̵� �ƿ� �ڷ�ƾ ����
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
