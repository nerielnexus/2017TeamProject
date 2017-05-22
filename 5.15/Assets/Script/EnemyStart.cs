using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour {
    public Transform point;
    public GameObject EnemyPrefab;
    public bool Success;
    public bool Fail;
	// Use this for initialization
	void Start () {
        point = this.transform.FindChild("Point");
	}
	
	// Update is called once per frame
	void Update () {
        if (Fail)
        {
            Instantiate(EnemyPrefab, point.position, point.rotation);
        }
	}
}
