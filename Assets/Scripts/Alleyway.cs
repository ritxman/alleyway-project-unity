using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alleyway : MonoBehaviour {

	private bool isTaufikClicked = false;
	private bool isSprayClicked = false;
	private bool isSnackClicked = false;
	private bool isFadeInFadeOut = true;
	private bool isKrisGetOut = true;

	public DialogueController dc;
	public SoundManager sm;
	public GameObject buttonBack;
	public GameObject fadeInFadeOut;
	public GameObject KrisGameObject;
	public GameObject parent;
	public void BackToMap(){
		//sm.PlaySFXButtonClick();
		Application.LoadLevel ("Map");
	}
	public void TaufikClicked(){
		if (PlayerPrefs.GetInt ("isTaufikClicked") == 0) {
			isTaufikClicked = true;
			dc.StartDialogue (0, 5);
		} else if (PlayerPrefs.GetInt ("isTaufikClicked") == 1) {
			isTaufikClicked = true;
			dc.StartDialogue (0, 6);
		} else if(PlayerPrefs.GetInt ("isTaufikClicked") == 2){
			isTaufikClicked = true;
			dc.StartDialogue (0, 21);
		}else if(PlayerPrefs.GetInt ("isTaufikClicked") == 3){
			isTaufikClicked = true;
			dc.StartDialogue (0, 20);
		}else if(PlayerPrefs.GetInt ("isTaufikClicked") == 4){
			isTaufikClicked = true;
			dc.StartDialogue (0, 12);
		}
	}
	public void KrisClicked(){
		if(PlayerPrefs.GetInt("isKrisClicked") == 0){
			dc.StartDialogue (0, 23);
		}else if(PlayerPrefs.GetInt("isKrisClicked") == 1){
			dc.StartDialogue (0, 28);
		}else if(PlayerPrefs.GetInt("isKrisClicked") == 2){
			dc.StartDialogue (0, 39);
		}else if(PlayerPrefs.GetInt("isKrisClicked") == 3){
			dc.StartDialogue (0, 40);
		}else if(PlayerPrefs.GetInt("isKrisClicked") == 4){
			dc.StartDialogue (0, 41);
		}
	}
	public void MotorcycleClicked(){
		if(PlayerPrefs.GetInt("Item8") == 1){
			if(PlayerPrefs.GetInt("Item4") == 0){
				dc.StartDialogue (0,22);
			}
		}else{
			dc.StartDialogue (0,19);
		}
	}
	public void SnackClicked(){
		GameObject.Find ("Snack").SetActive(false);
		isSnackClicked = true;
		dc.StartDialogue (0,18);
	}
	public void MissingChildrenPosterClicked(){
		dc.StartDialogue (0,4);
	}
	public void OtherMuralClicked(){
		dc.StartDialogue (0,3);
	}
	public void AngelMuralClicked(){
		dc.StartDialogue (0,2);
	}
	public void SprayPaintClicked(){
		GameObject.Find ("Spray_paint").SetActive(false);
		isSprayClicked = true;
		dc.StartDialogue (0,1);
	}
	// Use this for initialization
	void Start () {
		parent.SetActive(true);
		//PlayerPrefs.DeleteAll ();
		//TEST PURPOSE ONLY!!

		/*
		PlayerPrefs.SetInt ("isTaufikAlleywayGone",1);
		PlayerPrefs.SetInt ("Item3",1);
		PlayerPrefs.SetInt ("Item1",1);
		PlayerPrefs.SetInt ("Item6",1);
		PlayerPrefs.SetInt ("Item5",1);
		PlayerPrefs.SetInt ("Profile4",1);
		*/
		////END OF TEST SCOPE
		PlayerPrefs.SetString("currentScene","Alleyway");
		if(PlayerPrefs.GetInt ("isTaufikAlleywayGone") == 1){
			GameObject.Find ("Taufik").SetActive(false);
			KrisGameObject.SetActive (true);
		}
		if(PlayerPrefs.GetInt("isSnackClicked") == 1){
			isSnackClicked = true;
			PlayerPrefs.SetInt ("Item1",1);
			PlayerPrefs.Save ();
			GameObject.Find ("Snack").SetActive(false);
		}
		if(PlayerPrefs.GetInt("isSprayClicked") == 1){
			isSprayClicked = true;
			PlayerPrefs.SetInt ("Item6",1);
			PlayerPrefs.Save ();
			GameObject.Find ("Spray_paint").SetActive(false);
		}
		if(PlayerPrefs.GetInt("isTaufikClicked") == 1 || PlayerPrefs.GetInt("isTaufikClicked") == 2 || 
			PlayerPrefs.GetInt("isTaufikClicked") == 3 || PlayerPrefs.GetInt("isTaufikClicked") == 4){
			PlayerPrefs.SetInt ("Profile2",1);
			PlayerPrefs.SetInt ("Item2",1);
			PlayerPrefs.SetInt ("Item7",1);
			PlayerPrefs.Save ();
			isTaufikClicked = true;
			buttonBack.SetActive (true);
		}
		if(PlayerPrefs.GetInt("isSnackClicked") == 1){
			PlayerPrefs.SetInt ("Item1",1);
			isSnackClicked = true;
		}
		dc.SetIsDialogue (false);
	}

	public void FinishDialogue(){
		if(isTaufikClicked){
			if (PlayerPrefs.GetInt ("isTaufikClicked") == 0) {
				PlayerPrefs.SetInt ("isTaufikClicked", 1);
				PlayerPrefs.SetInt ("About the Angel of Death case.", 1);
				PlayerPrefs.SetInt ("How did you find Wanda?", 1);
				PlayerPrefs.SetInt ("Show Evidence_taufik_Alleyway",1);
				PlayerPrefs.SetInt ("Profile2", 1);
				PlayerPrefs.SetInt ("Profile3", 1);
				PlayerPrefs.SetInt ("Item2", 1);
				PlayerPrefs.SetInt ("Item7", 1);
				PlayerPrefs.SetInt ("preschoolMap", 1);
				PlayerPrefs.SetInt ("alleywayMap", 1);
				isTaufikClicked = false;
				buttonBack.SetActive (true);
			} else {
				if (dc.GetBranch () == 8 || dc.GetBranch () == 9) {
					PlayerPrefs.SetInt ("isTaufikClicked1", 1);
				} else if (dc.GetBranch () == 11 || dc.GetBranch () == 12) {
					PlayerPrefs.SetInt ("isTaufikClicked2", 1);
				}
				if (PlayerPrefs.GetInt ("isTaufikClicked1") == 1 && PlayerPrefs.GetInt ("isTaufikClicked2") == 0) {
					PlayerPrefs.SetInt ("isTaufikClicked", 2);
				} else if (PlayerPrefs.GetInt ("isTaufikClicked1") == 0 && PlayerPrefs.GetInt ("isTaufikClicked2") == 1) {
					PlayerPrefs.SetInt ("isTaufikClicked", 3);
				} else if (PlayerPrefs.GetInt ("isTaufikClicked1") == 1 && PlayerPrefs.GetInt ("isTaufikClicked2") == 1) {
					PlayerPrefs.SetInt ("isTaufikClicked", 4);
				}
			}
			PlayerPrefs.Save ();
		}
		if(isSprayClicked){
			PlayerPrefs.SetInt ("isSprayClicked", 1);
			PlayerPrefs.SetInt ("Item6",1);
			PlayerPrefs.Save ();
			isSprayClicked = false;
		}
		if(isSnackClicked){
			PlayerPrefs.SetInt ("isSnackClicked", 1);
			PlayerPrefs.SetInt ("Item1",1);
			PlayerPrefs.Save ();
			isSnackClicked = false;
		}
		if(dc.GetBranch() == 22){
			PlayerPrefs.SetInt ("Item4",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 23){
			PlayerPrefs.SetInt ("Show Evidence_kris_Alleyway",1);
			PlayerPrefs.SetInt ("isKrisClicked",1);
			PlayerPrefs.SetInt ("Profile5",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 24){
			PlayerPrefs.SetInt ("What were you doing at 2PM?",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 32 || dc.GetBranch() == 44){ //question 1 done
			PlayerPrefs.SetInt ("How do you know Wanda?",1);
			PlayerPrefs.SetInt ("isQuestion1Kris",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 37 || dc.GetBranch() == 38 || dc.GetBranch() == 43){ //question 2 done
			PlayerPrefs.SetInt ("isQuestion2Kris",1);
			PlayerPrefs.SetInt ("policeStationPhase",2);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 30){ //looping
			dc.SetIsFreezed (true);
			dc.StartDialogue (133,29);
			dc.NextDialogue();
		}
		if(dc.GetBranch() == 33){ //looping
			dc.StartDialogue (135,31);
			dc.NextDialogue();
		}
		if(dc.GetBranch() == 31 || dc.GetBranch() == 35 || dc.GetBranch() == 36){
			dc.SetIsFinish (false);
			dc.ShowEvidence ("Question");
		}
		if (PlayerPrefs.GetInt ("isQuestion1Kris") == 1 && PlayerPrefs.GetInt ("isQuestion2Kris") == 0) {
			PlayerPrefs.SetInt ("isKrisClicked",2);
			PlayerPrefs.Save ();
		} else if (PlayerPrefs.GetInt ("isQuestion1Kris") == 0 && PlayerPrefs.GetInt ("isQuestion2Kris") == 1) {
			PlayerPrefs.SetInt ("isKrisClicked",3);
			PlayerPrefs.Save ();
		} else if (PlayerPrefs.GetInt ("isQuestion1Kris") == 1 && PlayerPrefs.GetInt ("isQuestion2Kris") == 1) {
			PlayerPrefs.SetInt ("isKrisClicked",4);
			PlayerPrefs.Save ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(dc.GetDialogueCounter() == 16 && isFadeInFadeOut){
			fadeInFadeOut.SetActive(true);
			dc.arrow.SetActive (false);
			dc.SetIsDialogue (false);
			if(fadeInFadeOut.GetComponent<Image>().color.a == 0){
				isFadeInFadeOut = false;
				fadeInFadeOut.SetActive(false);
				dc.SetIsDialogue (true);
				dc.SetIsClick (true);
				dc.arrow.SetActive (true);
			}
		}
		if(dc.GetDialogueCounter() == 105 && isKrisGetOut){
			if(dc.characterSprite.transform.localPosition.x <= 640){
				sm.PlaySFXCharacterMove ();
				dc.arrow.SetActive (false);
				dc.characterSprite.transform.localPosition = new Vector2 (dc.characterSprite.transform.localPosition.x+(400*Time.deltaTime),dc.characterSprite.transform.localPosition.y);
				dc.SetIsDialogue (false);
			}else{
				sm.UnplaySFXCharacterMove ();
				isKrisGetOut = false;
				dc.SetIsDialogue (true);
				dc.SetIsClick (true);
				dc.arrow.SetActive (true);
			}
		}
	}
}
