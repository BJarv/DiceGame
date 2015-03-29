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
		if(dice.Count < 5){
			Vector3 pos = new Vector3((transform.position.x + (dieOffset * (dice.Count))), transform.position.y, transform.position.z);
			GameObject newDie = (GameObject)Instantiate (dieFab, pos, Quaternion.identity);
			dice.Add (newDie);
			newDie.transform.parent = transform;
			newDie.transform.localScale = transform.localScale;
			newDie.GetComponent<Die>().roll ();
		} else {
			Debug.Log ("Adding more than five dice, don't do that");
		}
	}

	public void removeDie(){
		if(dice.Count > 0) {
			GameObject tempDie = (GameObject)dice[dice.Count - 1];
			dice.Remove(tempDie);
			Destroy (tempDie);
		} else {
			Debug.Log ("Removing less than zero dice, don't do that");
		}

	}

	public void rollDice() {
		BroadcastMessage("roll", SendMessageOptions.DontRequireReceiver);
	}

	public int diceCount() {
		return dice.Count;
	}
}
