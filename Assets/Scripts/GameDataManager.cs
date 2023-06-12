using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static int[] stageClearInfo = {0,0,0,0,0};
    

 
    // �������� �ҷ�����
    private void Awake() 
    {
        int [] serializedData=Load();

        if (serializedData != null) // �����ѵ����Ͱ� ���� ���
        {

            stageClearInfo = serializedData;
        }
       
        Debug.Log(stageClearInfo[0]);

    }
    public int[] Load()
    {

        string clearedStage = PlayerPrefs.GetString("stageClearData");
        string[] stringArray = clearedStage.Split(',');

       
        if (clearedStage!=string.Empty)
        {
            Debug.Log("����� �����Ͱ� �ֽ��ϴ�.");
            // ���� �迭�� ��ȯ
            int[] serializedData = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                int.TryParse(stringArray[i], out serializedData[i]);
            }


            return serializedData;
          
        }
     
         Debug.Log("����� �����Ͱ� �����ϴ�.");
   

        return null;
       
    }
   
    public int[] getClearedStage()
    {

        return stageClearInfo;

    }
    public void ClearStage(int stage_num)
    {
        stageClearInfo[stage_num] = 1;
        
    }

    public void Save()
    {
        Debug.Log("����");
        string serializedData = string.Join(",", stageClearInfo.Select(x => x.ToString()).ToArray());
        PlayerPrefs.SetString("stageClearData", serializedData);
    }

    public int isCleared(int stage_num)
    {
        return stageClearInfo[stage_num];
    }

 
}
