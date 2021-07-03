using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class District_8 : MonoBehaviour {

	public GameObject fadingImage;
	public GameObject titleText;
	public DialogueController dc;
	public GameObject parent;

	void Start(){
		parent.SetActive(true);
		PlayerPrefs.DeleteAll ();
		dc.SetIsDialogue (true);
	}
	void Update(){
		if(dc.GetIsStart()){
			titleText.SetActive (false);
		}
		if(dc.GetIsFinish()){
			fadingImage.SetActive (true);
			Invoke ("nextScene",3f);
		}
	}
	void nextScene(){
		SceneManager.LoadScene ("Alleyway");
	}
}
