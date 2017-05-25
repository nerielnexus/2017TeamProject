using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseControlV2 : MonoBehaviour
{

	// 타겟 플레이어
	private GameObject user;

	// 게이지
	private GameObject gauge;

	private bool isCreated;
	private bool isChecked;
	public float invokeTimer;

	// 타겟 플레이어의 태그
	public string mytag;

	// Use this for initialization
	void Start()
	{
		isCreated = false;
		isChecked = false;
		invokeTimer = 3.0f;
		mytag = "Player";

		if ((user = GameObject.FindWithTag(mytag) as GameObject) == null)
		{
			Debug.Log("Cannot find Player!");
			Destroy(this.gameObject);
		}

		Debug.Log("Player is operational");
		Debug.Log(user.gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		// EnlistHouse() 를 호출
		Debug.Log("Enlist House");
		if (user.GetComponent<PlayerScript>().inDistance(this.gameObject))
			user.GetComponent<PlayerScript>().EnlistHouse(this.gameObject);

		if (gauge != null
			&& !user.GetComponent<PlayerScript>().inDistance(gauge.gameObject)
			&& isCreated)
		{
			Debug.Log("OutofDistance");
			Destroy(gauge.gameObject);
			isCreated = false;
			isChecked = false;
			return;
		}
	}

	public void DoGaugeAction()
	{
		if (isChecked)
			return;

		Debug.Log("HCV2/DoGaugeAction");

		if (!isCreated)
		{
			Debug.Log("DGA/!isCreated");

			if ((gauge = Instantiate(Resources.Load("ActionGauge")) as GameObject) == null)
			{
				Debug.Log("Failed to Instantiate Gauge!");
				return;
			}

			gauge.transform.position = this.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
			gauge.transform.rotation = Camera.main.transform.rotation;

			isCreated = true;

			return;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			isChecked = true;
			Debug.Log("Space");

			bool isSuccess = GameObject.Find("ActionBar").GetComponent<NumberChange>().DoGaugeAction();

			if (isSuccess)
			{
				Debug.Log("Success");
				user.GetComponent<PlayerScript>().MakeCombo(true);
				Vector3 temp = gauge.transform.position;
				Destroy(gauge.gameObject);

				GameObject result = Instantiate(Resources.Load("NotifySuccess")) as GameObject;
				result.transform.position = temp;
				result.transform.rotation = Camera.main.transform.rotation;
				Destroy(result.gameObject, 2.0f);

				isCreated = false;
			}
			else
			{
				Debug.Log("Fail");
				user.GetComponent<PlayerScript>().MakeCombo(false);
				Vector3 temp = gauge.transform.position;
				Destroy(gauge.gameObject);

				GameObject result = Instantiate(Resources.Load("NotifyFailure")) as GameObject;
				result.transform.position = temp;
				result.transform.rotation = Camera.main.transform.rotation;
				Destroy(result.gameObject, 2.0f);

				isCreated = false;
			}

			Invoke("InvokeIsChecked", invokeTimer);
		}
	}

	private void InvokeIsChecked()
	{
		isChecked = false;
	}
}
