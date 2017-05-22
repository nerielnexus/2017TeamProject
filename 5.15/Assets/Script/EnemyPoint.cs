using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour {

    //Enemy가 출현할 위치를 담을 배열
    public Transform[] points;
    public Transform[] House;
    //Enemy프리팹을 할당할 변수
    public GameObject EnemyPrefab;

    //Enemy를 발생시킬 시간
    public float createTime = 2.0f;
    //최대 몇 마리?
    public int maxEnemy = 10;
    //게임 종료 여부 변수
    public bool isGameOver = false;

	// Use this for initialization
	void Start () {

        //points = GameObject.Find("House").GetComponentsInChildren<Transform>();

        //StartCoroutine(this.CreateEnemy());
        /*
        if (points.Length > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }
        */
    }
    /*
    IEnumerator CreateEnemy()
    {
        while (!isGameOver)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount < maxEnemy)
            {
                //createTime만큼 기다려라
                yield return new WaitForSeconds(createTime);

                //위치 랜덤하게
                int idx = Random.Range(1, points.Length);
                //enemy 동적 생성
                Instantiate(EnemyPrefab, points[idx].position, points[idx].rotation);

            }
            else
            {
                yield return null;
            }
        }
    }
	*/
	// Update is called once per frame
	void Update () {
		
	}
}
