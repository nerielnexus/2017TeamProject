using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseControl : MonoBehaviour {

	#region 변수와 프로퍼티
	private GameObject User;

	// 기준거리 설정
	public float dist = 2.0f;

	private bool IsCreated = false;
	public bool CheckIsCreated
	{
		get
		{
			return IsCreated;
		}

		set
		{
			IsCreated = value;
		}
	}

	private GameObject gauge = null;
	public GameObject CheckGauge
	{
		get
		{
			return gauge;
		}

		set
		{
			gauge = value;
		}
	}

	private GameObject Result = null; 
	#endregion



	// Use this for initialization
	void Start () {
		if((User = GameObject.FindWithTag("MyPlayer") as GameObject) == null)
		{
			Debug.Log("Cannot find Player!");
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// space 를 누름
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// 기준거리 안에 들어왔고 게이지 생성이 안된 상태라면
			if (CalcDistance() && !IsCreated)
			{
				IsCreated = true;
				gauge = Instantiate(Resources.Load("ActionGauge")) as GameObject;
				gauge.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
				gauge.transform.rotation = Camera.main.transform.rotation;
			} 
			// 게이지 생성이 된 상태라면
			else if(IsCreated)
			{
				// 게이지 액션을 실행하고 결과를 저장
				bool isSuccess = GameObject.Find("ActionBar").GetComponent<NumberChange>().DoGaugeAction(); ;

				if(isSuccess)
				{
					Debug.Log("Success");
					Destroy(gauge.gameObject);

					Result = Instantiate(Resources.Load("NotifySuccess")) as GameObject;
					Result.transform.position = this.transform.position + new Vector3(0, 1.0f, 0);
					Result.transform.rotation = Camera.main.transform.rotation;
					Destroy(Result.gameObject, 2.0f);
					
					IsCreated = false;
				}
				else
				{
					Debug.Log("Fail");
					Destroy(gauge.gameObject);

					Result = Instantiate(Resources.Load("NotifyFailure")) as GameObject;
					Result.transform.position = this.transform.position + new Vector3(0, 1.0f, 0);
					Result.transform.rotation = Camera.main.transform.rotation;
					Destroy(Result.gameObject, 2.0f);

					IsCreated = false;
				}
			}
		}
		
		// 게이지 생성 후 기준거리보다 멀어지면
		if(!CalcDistance() && IsCreated)
		{
			if (gauge != null)
				Destroy(gauge.gameObject);

			IsCreated = false;
		}

		
	}

	// 거리 계산
	bool CalcDistance()
	{
		if (dist >= Vector3.Distance(User.transform.position, transform.position))
			return true;

		return false;
	}


}
