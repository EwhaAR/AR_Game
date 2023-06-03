using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Monologue
{
    public string name; // ��ȭ����
    public string sentence; // ����
    public int bubbleType; // ��ǳ�� Ÿ�� (0: �ʷϹ�� �ȳ�����, 1: ������ ���, 2: ������ǳ��)
    public GameObject clip; // �ο� �̹���
}

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Monologue> info = new List<Monologue>(); // ����ü�� ��� ����Ʈ ����


}
