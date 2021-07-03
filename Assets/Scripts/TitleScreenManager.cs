using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

	private Color c;
	private GameObject temp;

	public GameObject imgHoverContinue;
	public GameObject fadingImage;
	public GameObject parent;
	public SoundManager sm;
	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetString ("currentScene","Alleyway");
		parent.SetActive(true);
		if (PlayerPrefs.GetString ("currentScene") == "") {
			imgHoverContinue.SetActive (true);
		} else {
			imgHoverContinue.SetActive (false);
		}
	}

	public void StartGame(){
		PlayerPrefs.DeleteAll ();
		if(PlayerPrefs.GetString ("currentScene") == ""){
			PlayerPrefs.SetString ("currentScene","District_8");
		}
		sm.PlaySFXButtonClick ();
		fadingImage.SetActive (true);
		Invoke ("LoadScene",4f);
	}
	public void ContinueGame(){
		sm.PlaySFXButtonClick ();
		fadingImage.SetActive (true);
		Invoke ("LoadScene",4f);
	}
	public void LoadScene(){
		Application.LoadLevel (""+PlayerPrefs.GetString ("currentScene"));
	}
	public void ExitGame(){
		sm.PlaySFXButtonClick ();
		Application.Quit ();
	}
	public void hoverButton(string target){
		temp = GameObject.Find ("" + target);
		c = temp.GetComponent<Text>().color;
		c.a = 0.5f;
		temp.GetComponent<Text> ().color = c;
	}
	public void nonHoverButton(string target){
		temp = GameObject.Find ("" + target);
		c = temp.GetComponent<Text>().color;
		c.a = 1f;
		temp.GetComponent<Text> ().color = c;
	}
}
