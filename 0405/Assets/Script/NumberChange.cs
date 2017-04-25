using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberChange : MonoBehaviour
{
	public Image AreaBG;
	public Image AreaTarget;
	public Canvas AreaParent;

	public float rate;

	private float count;
	public float _Count
	{
		get
		{
			return count;
		}
	}

	private float number;
	public float _Number
	{
		get
		{
			return number;
		}
	}

	private int ActionSize;
	public int _ActionSize
	{
		get
		{
			return ActionSize;
		}
	}

	private bool isHigh;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(RoundTrip());

		rate = 0.034f;
		count = 3.4f;
		number = 0f;
		isHigh = false;
		ActionSize = 1;

		AreaTarget.rectTransform.sizeDelta = new Vector2(60, 100);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Alpha1))
			ActionSize = 1;

		if (Input.GetKey(KeyCode.Alpha2))
			ActionSize = 2;

		if (Input.GetKey(KeyCode.Alpha3))
			ActionSize = 3;

		switch (ActionSize)
		{
			case 1:
				AreaTarget.rectTransform.sizeDelta = new Vector2(60, 100);
				break;

			case 2:
				AreaTarget.rectTransform.sizeDelta = new Vector2(40, 100);
				break;

			case 3:
				AreaTarget.rectTransform.sizeDelta = new Vector2(20, 100);
				break;

			default:
				break;
		}
	}

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
				yield return new WaitForSeconds(0.0f);
			}
			else
			{
				number += rate;
				transform.position += new Vector3(rate, 0, 0);
				yield return new WaitForSeconds(0.0f);
			}

			yield return null;
		}
	}

	public bool DoGaugeAction()
	{
		float min = count * 0.1f * (1 + ActionSize);
		float max = count * (1.0f - 0.1f * (1 + ActionSize));

		if(number >= min && number <= max)
			return true;
		else
			return false;
	}
}