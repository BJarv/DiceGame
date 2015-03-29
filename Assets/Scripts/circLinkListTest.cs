using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class circLinkListTest : MonoBehaviour {


	string[] words = {"can", "this", "even", "be", "so", "easy?"};
	LinkedList<string> sentence;
	// Use this for initialization
	void Start () {
		sentence = new LinkedList<string>(words);
		LinkedListNode<string> beg = sentence.First;
		/*int i = 0;
		for(;i < 100; beg = beg.Next ?? beg.List.First){
			Debug.Log (beg.Value);
			i++;
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
