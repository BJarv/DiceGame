using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {

	public Sprite[] dieFaces;
	public int value = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int roll() {
		value = Random.Range (1, 7);
		Debug.Log (value);
		GetComponent<SpriteRenderer>().sprite = dieFaces[value - 1];
		return value;
	}
}
