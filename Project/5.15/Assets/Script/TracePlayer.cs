using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePlayer : MonoBehaviour {

    //Enemy 상태 정보
    public enum EnemyState { idle, trace, die };    //평소 상태, 추적 상태, 소멸 정도만?
    public EnemyState enemyState = EnemyState.idle;
    public float idle_time = 0.0f;		//idle 시간 측정  

    private Transform enemyTr;
    private Transform playerTr;
    private UnityEngine.AI.NavMeshAgent nvAgent;

    //추적 사정거리를 조정
    public float traceDist = 10.0f;

    //Enemy 사망 여부
    private bool Die = false;

    // Use this for initialization
    void Start() {
        enemyTr = this.gameObject.GetComponent<Transform>();                        //Enemy의 transform 할당
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();      //추적 대상인 tag="player"의 transform 할당
        nvAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();      //NavMeshAgent 컴포넌트 할당

        //추적 대상의 위치를 설정하면 바로 추적 시작
        nvAgent.destination = playerTr.position;

        //일정 간격으로 Enemy 행동 상태를 체크하는 함수, 쉽게 말하면 update랑 똑같음.
        StartCoroutine(this.CheckEnemyState());

        //일정 간격으로 Enemy 행동 상태에 따라 동작하는 함수, 쉽게 말하면 update랑 똑같음.
        StartCoroutine(this.EnemyAction());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator CheckEnemyState()
    {
        while (!Die)
        {
            yield return new WaitForSeconds(0.2f);  //0.2초 마다 발동된다

            //Enemy와 Player 거리 측정
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if (idle_time > 0.3f)
            {
                EnemyDie();
            }

            if(dist<=traceDist)
            {
                enemyState = EnemyState.trace;
                idle_time = 0.0f;
            }
            else if (dist > traceDist)
            {
                enemyState = EnemyState.idle;
                idle_time += Time.deltaTime;	//idle상태가 지속되면 시간 증가 
                Debug.Log(idle_time);
            }
            else if (dist == 0)
            {
                //enemyState = EnemyState.die;
            }        

        }
    }

    IEnumerator EnemyAction()
    {
        while (!Die)
        {
            switch (enemyState)
            {
                case EnemyState.idle:
                    nvAgent.Stop();
                    break;

                case EnemyState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();
                    break;
            }
            yield return null;
        }
    }

    void EnemyDie()		//시간이 다 되면 Enemy 소멸
    {
        Die = true;
        enemyState = EnemyState.die;
        Destroy(gameObject);
    }

 
}
