using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{

    public Transform arCamera;
    public GameObject projectile;

    public float shootForce = 700.0f;

    // Scene ��ȯ
    private GameObject dontDestroy;
    private int gameStage;
    private int stageStep;
    GameSceneManager gameSceneManager;

    // Player Guide
    private Text playerGuideText;

    // Score
    public static int currentScore = 0;
    private int targetScore = 100;
    private Text scoreText;

    private void Awake()
    {
        scoreText = GameObject.Find("SuccessScoreText").GetComponent<Text>();
        scoreText.text = "����: " + currentScore + "/" + targetScore;

        // Player Guide
        playerGuideText = GameObject.Find("PlayerGuideText").GetComponent<Text>();
        playerGuideText.text = "����� �ν� ���Դϴ�.";
    }

    void Start()
    {
        dontDestroy = GameObject.Find("DontDestroy");
        gameStage = dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage;
        stageStep = dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep;
        gameSceneManager = FindObjectOfType<GameSceneManager>();

        // Player Guide
        //playerGuideText = GameObject.Find("PlayerGuideText").GetComponent<Text>();
        playerGuideText.text = "����� �ν� ���Դϴ�."; 
    }

    void Update()
    {
        //���̵���� ���ֱ�
        playerGuideText.text = " ";

        // touch �� �Ѿ� �߻�
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GameObject bullet = Instantiate(projectile, arCamera.position, arCamera.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(arCamera.forward * shootForce);
        }

        //if (Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Began))
        //{
        //    dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep = 1;
        //    dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage = 3;
        //    gameSceneManager.convertScene();
        //}

        // ���� ǥ��
        scoreText.text = "����: " + currentScore + "/" + targetScore;

        // ���� Ȯ��
        if (currentScore >= targetScore) 
        {
            dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep = 1;
            dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage = 3 ;
            gameSceneManager.convertScene();
        }
    }
}
