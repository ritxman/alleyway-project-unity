using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

	private Color c;
	private GameObject temp;
	private string sceneName = "";

	public Text locationName;
	public GameObject fadingImage;
	public GameObject alleyway;
	public GameObject preschool;
	public GameObject policeStation;
	public SoundManager sm;
	public GameObject parent;
	// Use this for initialization
	void Start(){
		parent.SetActive(true);
		locationName.text = "";
		PlayerPrefs.SetString("currentScene","Map");
		if (PlayerPrefs.GetInt ("alleywayMap") == 1) {
			alleyway.SetActive (false);
		} else {
			alleyway.SetActive (true);
		}
		if (PlayerPrefs.GetInt ("preschoolMap") == 1) {
			preschool.SetActive (false);
		} else {
			preschool.SetActive (true);
		}
		if (PlayerPrefs.GetInt ("policeStationMap") == 1) {
			policeStation.SetActive (false);
		} else {
			policeStation.SetActive (true);
		}
	}

	public void buttonHover(string target){
		temp = GameObject.Find ("" + target);
		temp.transform.localScale = new Vector2 (1.1f,1.1f);
		locationName.text = ""+target;
	}
	public void buttonOut(string target){
		temp = GameObject.Find ("" + target);
		temp.transform.localScale = new Vector2 (1f,1f);
		locationName.text = "";
	}
	public void GoTo(string target){
		sm.PlaySFXButtonClick();
		fadingImage.SetActive (true);
		sceneName = target;
		Invoke ("LoadScene",3f);
	}
	public void LoadScene(){
		Application.LoadLevel (""+sceneName);
	}
}
