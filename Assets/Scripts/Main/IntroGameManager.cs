using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameManager : MonoBehaviour
{

    public  GameObject bg1;
    public  GameObject bg2;
    public  GameObject pictureGuide;


    public static bool doesDialogue1End=false;
    public static bool doesDialogue2End=false;

    public DialogueTrigger dialogueTrigger;

    /**
     * Dialogue1 : ���ΰ� ����
     * Dialogue2 : �ƻ���а� ���� ~ �̴ϰ�����
     * Dialogue3 : �̴ϰ��� ���� ��
     * 
     */

    // Start is called before the first frame update
    void Start()
    {

 
        dialogueTrigger.Dialogue1Trigger();// ��ȭ1 ����


    }

   
    public void Dialogue1End()
    {
        Debug.Log("Dialogue1End");
        pictureGuide.SetActive(true);

    }

    public  void ActivateTakePicture() // picture ���̵��� onClick �Լ��� ���
    {
        pictureGuide.SetActive(false);

        // ī�޶� ����

        Debug.Log("ActivateTakePicture");


        // ���� �νĵǸ� 

        var AsanPictureRecognized = true;

        if (AsanPictureRecognized)
        {
            Dialogue2Start();
        }
        
    } 

    public void Dialogue2Start() // �ƻ� ���а� �ʹ� ���� (��� ��ȯ)
    {

        bg1.SetActive(false);
        bg2.SetActive(true);
        dialogueTrigger.Dialogue2Trigger();// ��ȭ2 ����

       



    }

    public void Dialogue2End()
    {

        Debug.Log("Dialogue2End");
        FeedTheCatGameStart();


    }



    public void FeedTheCatGameStart()
    {

        Debug.Log("FeedTheCatGameStart");

        // ����ϸ� 
        var FeedTheCatGamePassed = true;

        if (FeedTheCatGamePassed)
        {
            Dialogue3Start();
        }

    }

    public void Dialogue3Start() // �ƻ� ���а� �ʹ� ���� (��� ��ȯ)
    {

   
        dialogueTrigger.Dialogue3Trigger();// ��ȭ2 ����





    }
}
