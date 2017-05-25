using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// 게이지 레벨
	private int level;
	public int Level
	{
		get
		{
			return level;
		}
	}

	// 점수
	private int score;
	public int Score
	{
		get
		{
			return score;
		}
	}

	// 피버 카운트
	public bool isFever;
	public bool IsFever
	{
		get
		{
			return isFever;
		}
	}

	

	

	// 기준 거리
	public float dist;

	// 타겟이 되는 집
	private List<GameObject> house = new List<GameObject>();



	// Use this for initialization
	void Start()
	{
		score = 0;
		level = 0;
		dist = 10.0f;

		isFever = false;
	}

	// Update is called once per frame
	void Update()
	{
		if( SearchMinDistance() != null )
		{
			// 게이지 액션 하기
			SearchMinDistance( ).GetComponent<HouseControlV2>( ).DoGaugeAction( );
		}

		// 콤보상태를 확인해서 피버 여부를 결정
		FeverControl( );
	}

	// 게이지 영역 조절용
	public void ControlLevel(bool isSuccess)
	{
		if( isSuccess )
		{
			if( level >= 2 )
				return;

			level++;
		}
		else
			level = 0;

		return;
	}

	// 기준 거리 안에 들어와있는지 확인
	public bool inDistance(GameObject target)
	{
		if( target == null )
			return false;

		if( dist >= Vector3.Distance(transform.position , target.transform.position) )
			return true;

		return false;
	}

	// 거리 계산용
	private float CalcDistance(GameObject target)
	{
		return Vector3.Distance(transform.position , target.transform.position);
	}

	// 기준 거리와 비교해서 집 관리
	public void EnlistHouse(GameObject target)
	{
		if( target == null )
			return;

		GameObject temp = null;

		if( inDistance(target) )
			house.Add(target);
		else if( !inDistance(target) )
		{
			temp = house.Find(x => x.transform.position == target.transform.position);

			if( temp != null )
				house.Remove(temp);
			else
				return;
		}
	}

	// 리스트의 집들을 대상으로 최소거리 집 탐색
	private GameObject SearchMinDistance()
	{
		float minDist = dist;
		GameObject tempObj = null;

		if( house.Capacity == 0 )
			return null;

		foreach( GameObject srch in house )
		{
			if( minDist >= CalcDistance(srch) )
			{
				minDist = CalcDistance(srch);
				tempObj = srch;
			}
		}

		return tempObj;
	}

	// 게이지 액션의 성공 여부를 받은 후 level 에 반영
	public void MakeCombo(bool isSuccess)
	{
		// isSuccess == false 이면 콤보가 끊어진걸로 간주
		if(!isSuccess)
		{
			level = 0;
			/* 콤보 실패시 이펙트 같은걸 넣을거면 여기로 */
			return;
		}

		level++;

		if(!isFever)
			score += 10 * (level +1);
		else
			score += 300;
	}

	/*	피버 컨트롤의 콤보 조건, 피버카운트는 적당히 조절 필요
	 *	추후 지속시간을 가진 피버로 교체할 의향이 있음
	 */
	private void FeverControl()
	{
		if(level == 5)	// 5콤보 성공하면
		{
			isFever = true;
			level = -2;	// 피버상태 돌입
		}
		if( level == 0 )
		{
			isFever = false;
		}
			
	}
}
