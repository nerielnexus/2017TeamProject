using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseControl : MonoBehaviour {

	private GameObject User;
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



	// Use this for initialization
	void Start () {
		if((User = GameObject.FindWithTag("Player") as GameObject) == null)
		{
			Debug.Log("Cannot find Player!");
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (CalcDistance() && !IsCreated)
			{
				IsCreated = true;
				gauge = Instantiate(Resources.Load("ActionGauge")) as GameObject;
				gauge.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
				gauge.transform.rotation = Camera.main.transform.rotation;
			} 
			else if(IsCreated)
			{
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

		if(!CalcDistance() && IsCreated)
		{
			if (gauge != null)
				Destroy(gauge.gameObject);

			IsCreated = false;
		}
	}

	bool CalcDistance()
	{
		if (dist >= Vector3.Distance(User.transform.position, transform.position))
			return true;

		return false;
	}


}
