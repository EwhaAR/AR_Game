using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public GameObject scoreToSpawn;
    public GameObject enemyToSpawn;
    Vector3 killPos;
    Quaternion killRot;
    public float waitTime = 3.0f;
    bool bulletCollission = false; // to avoid hittimg multiple dpiders with same bullet

    ////Scoring
    //TextMeshProUGUI scoreText;
    //GameObject scoreBoardUI;
    //TextMeshProUGUI gameSuccessText;
    //public static int score;

    //Audio
    AudioSource audioSoure;

    //GameSceneManger
    GameSceneManager gameSceneManager;

    void Start()
    {
        audioSoure = GetComponent<AudioSource>();

        //gameObject.GetComponent<Shoot>().enabled = true;
        //scoreBoardUI = GameObject.FindGameObjectWithTag("ScoreCanvas");
        //scoreText = GameObject.FindGameObjectWithTag("ScoreOnBanner").GetComponent<TextMeshProUGUI>();
        //gameSuccessText = GameObject.FindGameObjectWithTag("GameSuccessText").GetComponent<TextMeshProUGUI>();
        //gameSuccessText.gameObject.SetActive(false);  
    }

    void Update()
    {
        //scoreText.text = "Score: " + score.ToString();

        //score += 1;

        //if (score >= 500) 
        //{
        //    //Game Success Text ����
        //    gameSuccessText.gameObject.SetActive(true);

        //    //�Ź� �� ���ֱ�
        //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Spider");
        //    foreach (GameObject enemy in enemies)
        //        Destroy(enemy);

        //    gameSceneManager.convertScene();
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Spider" && bulletCollission == false)
        {
            Destroy(collision.transform.gameObject); // destroy spider
            //Scoring.score += 5;
            Shoot.currentScore += 5;
            audioSoure.Play(); //�Ź� �׾����� �Ҹ� ���

            bulletCollission = true;

            killPos = collision.transform.position;
            killRot = collision.transform.rotation;
            StartCoroutine(SpawnEnemyAgain());
            Destroy(Instantiate(explosion, collision.transform.position, collision.transform.rotation), waitTime);
            Destroy(Instantiate(scoreToSpawn, collision.transform.position, collision.transform.rotation), waitTime);
        }
    }
    IEnumerator SpawnEnemyAgain()
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemyToSpawn, killPos, killRot);
        bulletCollission = false;
        Destroy(gameObject); // destroy bullet
    }
}

