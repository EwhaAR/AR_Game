using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;



public class DialogueSystem : MonoBehaviour
{

    public GameObject translucentBubble; // ������ ��ǳ��
    public GameObject greenBubble; // �ʷ� ��ǳ��
    public GameObject thinkingBubble; // ���� ��ǳ�� 
    public GameObject speechBubble; //  �����̿� ��ǳ�� 
    private Dialogue DialogueInfo; 

    Queue<Monologue> dialogues = new Queue<Monologue>();
    // ��ųʸ� ����
    Dictionary<int, GameObject> bubbleMap = new Dictionary<int, GameObject>();
    // ��ǳ�� Ÿ�� (0: ������ ���, 1: �ʷϹ�� �ȳ�����, 2: ������ǳ, 3: ��ǳ��)


    // Start is called before the first frame update

    private void Start()
    {
        // ��ųʸ��� ������ �߰�
        bubbleMap.Add(0, translucentBubble);
        bubbleMap.Add(1, greenBubble);
        bubbleMap.Add(2, thinkingBubble);
        bubbleMap.Add(3, speechBubble);
    }
    public void Begin(Dialogue dialogue)

    {  

        this.DialogueInfo = dialogue;


        dialogues.Clear();


        foreach(var monologue in dialogue.info)
        {
            dialogues.Enqueue(monologue);

        }

        // ù���� init 
        Next();
       
    }

    public void DeactivateAllTxtGui()
    {
        translucentBubble.SetActive(false);
        greenBubble.SetActive(false);
        thinkingBubble.SetActive(false);
        speechBubble.SetActive(false);
    }

    public void Next()
    {
        if (dialogues.Count == 0)
        {
            End();
        }
        else
        {

            DeactivateAllTxtGui();

            Monologue monologue = dialogues.Dequeue();

            TextMeshProUGUI txtSentence;

            
          
            switch (monologue.bubbleType)
            {

                // ��ǳ�� ������ ���� switch
                case 0:
                    translucentBubble.SetActive(true);
                   txtSentence = translucentBubble.GetComponentInChildren<TextMeshProUGUI>();
                
                    break;


                case 1:
                    greenBubble.SetActive(true);
                    txtSentence = greenBubble.GetComponentInChildren<TextMeshProUGUI>();
                
                    break;

                case 2:
                    thinkingBubble.SetActive(true);
                    txtSentence = thinkingBubble.GetComponentInChildren<TextMeshProUGUI>();
                  
                    break;

                case 3:
                    speechBubble.SetActive(true);
                    txtSentence = speechBubble.GetComponentInChildren<TextMeshProUGUI>();
                   
                    break;
    
             
            }
            txtSentence = translucentBubble.GetComponentInChildren<TextMeshProUGUI>(); // �⺻ ������
            txtSentence.text = monologue.sentence;
            // ���� ������ ������ ���� Ȱ��ȭ

            if (monologue.clip!=null)
            {
                monologue.clip.SetActive(true);
            }


        }
    }

    public void End()
    {

        DeactivateAllTxtGui();
        IntroGameManager introGameManager = FindObjectOfType<IntroGameManager>();
  
        if (IntroGameManager.doesDialogue1End == false)
        {
            IntroGameManager.doesDialogue1End = true;
            introGameManager.Dialogue1End();
        }
        else if (IntroGameManager.doesDialogue2End == false)
        {
            IntroGameManager.doesDialogue1End = false;
            introGameManager.Dialogue2End();
        }
        


    }
}
