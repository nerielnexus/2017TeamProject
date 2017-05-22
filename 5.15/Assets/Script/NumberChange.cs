using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberChange : MonoBehaviour
{
	public Image AreaBG;
	public Image AreaTarget;
	public Canvas AreaParent;
	public GameObject user;

	// 게이지 최대값
	private float count;
	public float _Count
	{
		get
		{
			return count;
		}
	}

	// 게이지 현재 값
	private float number;
	public float _Number
	{
		get
		{
			return number;
		}
	}

	// 게이지 판정영역 설정
	private int ActionSize;
	public int _ActionSize
	{
		get
		{
			return ActionSize;
		}
	}

	// 게이지 최고점 확인용
	private bool isHigh;

	// 게이지 증가량
	public float rate;

	// 피버 확인용
	private bool isFever;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(RoundTrip());

		rate = 0.034f;
		count = 3.4f;
		number = 0.0f;
		isHigh = false;
		ActionSize = 1;
		isFever = false;

		AreaTarget.rectTransform.sizeDelta = new Vector2(60, 100);

		user = GameObject.FindWithTag("Player") as GameObject;
	}

	// Update is called once per frame
	void Update()
	{
		// 플레이어의 피버 상태를 받아옴
		isFever = user.GetComponent<PlayerScript>( ).IsFever;

		// 플레이어의 콤보 현황에 따라 판정영역 조절
		// 피버 상태일때는 -1 로 고정
		if( !isFever )
			ActionSize = user.GetComponent<PlayerScript>( ).Level;
		else
			ActionSize = -1;
		
		
		switch (ActionSize)
		{
			case -1:
				AreaTarget.rectTransform.sizeDelta = new Vector2(100 , 100);
				rate = 3.4f;
				break;

			case 0:
				AreaTarget.rectTransform.sizeDelta = new Vector2(60, 100);
				rate = 0.034f;
				break;

			case 1:
				AreaTarget.rectTransform.sizeDelta = new Vector2(40, 100);
				rate = 0.051f;
				break;

			case 2:
			default:
				AreaTarget.rectTransform.sizeDelta = new Vector2(20, 100);
				rate = 0.051f;
				break;
		}
	}

	// 게이지 이동 코루틴
	public IEnumerator RoundTrip()
	{
		while (true)
		{
			if (number <= 0)
				isHigh = false;
			else if (number >= count)
				isHigh = true;

			if (isHigh)
			{
				number -= rate;
				transform.position -= new Vector3(rate, 0, 0);
			}
			else
			{
				number += rate;
				transform.position += new Vector3(rate, 0, 0);
			} 

			yield return null;
		}
	}

	// 게이지 성공 판정
	public bool DoGaugeAction()
	{
		int tempAS = 0;

		if( ActionSize >= 3 )
			tempAS = 2;
		else if( ActionSize <= -1 )
			tempAS = -2;
		else
			tempAS = ActionSize;

		float min = count * 0.1f * (2 + tempAS);
		float max = count * (1.0f - 0.1f * (2 + tempAS));

		if(number >= min && number <= max)
			return true;
		else
			return false;
	}
}