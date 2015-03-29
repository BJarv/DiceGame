using UnityEngine;
using System.Collections;

public class DiceHand : MonoBehaviour {

	ArrayList dice;
	public GameObject dieFab;
	public float dieOffset = 4f;

	// Use this for initialization
	void Start () {
		dice = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addDie() {
		Vector3 pos = new Vector3((transform.position.x + (dieOffset * (dice.Count))), transform.position.y, transform.position.z);
		GameObject newDie = (GameObject)Instantiate (dieFab, pos, Quaternion.identity);
		dice.Add (newDie);
		newDie.transform.parent = transform;
		newDie.transform.localScale = transform.localScale;
		newDie.GetComponent<Die>().roll ();
	}

	public void removeDie(){
		GameObject tempDie = (GameObject)dice[dice.Count - 1];
		dice.Remove(tempDie);
		Destroy (tempDie);
	}

	public void rollDice() {
		BroadcastMessage("roll");
	}

	public int diceCount() {
		return dice.Count;
	}
}
