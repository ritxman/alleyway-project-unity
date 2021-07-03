using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation : MonoBehaviour {

	private bool isGetOut = true;
	private bool isGetIn = true;
	private bool isSpriteGetIn = true;
	private bool isTaufikClicked = false;
	Color c;

	public DialogueController dc;
	public SoundManager sm;
	public GameObject buttonBack;
	public GameObject gameOver;
	public GameObject aunty;
	public GameObject taufik;
	public GameObject aunty2;
	public GameObject taufik2;
	public GameObject parent;

	public void ChiefClicked(){
		if (PlayerPrefs.GetInt ("policeStationPhase") == 2) {
			dc.StartDialogue (0, 46);
		} else {
			if (PlayerPrefs.GetInt ("isChiefClicked") == 0) {
				dc.StartDialogue (0, 3);
			}
		}
	}
	public void BackToMap(){
		//sm.PlaySFXButtonClick();
		Application.LoadLevel ("Map");
	}
	public void TimeTableClicked(){
		GameObject.Find ("TimeTable").SetActive(false);
		dc.StartDialogue (0,1);
	}
	public void OwlDollClicked(){
		GameObject.Find ("OwlDoll").SetActive(false);
		dc.StartDialogue (0,2);
	}
	public void AuntyClicked(){
		PlayerPrefs.SetString("LastCharacterName","Aunty Eva");
		PlayerPrefs.SetString ("ShowEvidenceType","Question");
		if (PlayerPrefs.GetInt("isAuntyClicked") == 0) {
			dc.StartDialogue (0,8);	
		} else if(PlayerPrefs.GetInt("isAuntyClicked") == 1){
			dc.StartDialogue (0,9);
		} else if(PlayerPrefs.GetInt("isAuntyClicked") == 2){
			dc.StartDialogue (0,15);
		} else if(PlayerPrefs.GetInt("isAuntyClicked") == 3){
			dc.StartDialogue (0,20);
		} else if(PlayerPrefs.GetInt("isAuntyClicked") == 4){
			dc.StartDialogue (0,21);
		}
	}
	public void TaufikClicked(){
		PlayerPrefs.SetString("LastCharacterName","Taufik");
		PlayerPrefs.SetString ("ShowEvidenceType","Question");
		if (PlayerPrefs.GetInt ("isTaufikClicked") == 1) {
			isTaufikClicked = true;
			dc.StartDialogue (0, 31);
		} else if(PlayerPrefs.GetInt ("isTaufikClicked") == 2){
			isTaufikClicked = true;
			dc.StartDialogue (0, 32);
		}else if(PlayerPrefs.GetInt ("isTaufikClicked") == 3){
			isTaufikClicked = true;
			dc.StartDialogue (0, 33);
		}else if(PlayerPrefs.GetInt ("isTaufikClicked") == 4){
			isTaufikClicked = true;
			dc.StartDialogue (0, 34);
		}
	}
	// Use this for initialization
	void Start () {
		parent.SetActive(true);
		/*
		PlayerPrefs.DeleteAll ();
		for(int i=1; i<7; i++){
			PlayerPrefs.SetInt("Profile"+i,1);
		}
		for(int i=1; i<10; i++){
			PlayerPrefs.SetInt("Item"+i,1);
		}
		PlayerPrefs.SetInt ("trust",3);
		PlayerPrefs.SetInt ("policeStationPhase",2);
		*/
		PlayerPrefs.SetString("currentScene","Police_Station");
		if(PlayerPrefs.GetInt("isTimeTableClicked") == 1){
			PlayerPrefs.SetInt ("Item9",1);
			GameObject.Find ("TimeTable").SetActive(false);
			PlayerPrefs.Save ();
		}
		if(PlayerPrefs.GetInt("isOwlDollClicked") == 1){
			PlayerPrefs.SetInt ("Item8",1);
			GameObject.Find ("OwlDoll").SetActive(false);
			PlayerPrefs.Save ();
		}
		if (PlayerPrefs.GetInt ("policeStationPhase") != 2) {
			if(PlayerPrefs.GetInt("auntyUnlocked") == 1){
				aunty2.SetActive (true);
			}
			if(PlayerPrefs.GetInt("taufikUnlocked") == 1){
				taufik2.SetActive (true);
			}
		}
		if (PlayerPrefs.GetInt ("buttonBackPoliceStation") == 1){
			buttonBack.SetActive (true);
		}
		dc.SetIsDialogue (false);
	}
	public void FinishDialogue(){
		if(dc.GetBranch() == 64 || dc.GetBranch() == 65 || dc.GetBranch() == 69 || dc.GetBranch() == 70
			|| dc.GetBranch() == 71 || dc.GetBranch() == 72 || dc.GetBranch() == 73
			|| dc.GetBranch() == 77 || dc.GetBranch() == 78){
			Debug.Log("Bad Ending");
		}
		if(dc.GetBranch() == 47 || dc.GetBranch() == 51){
			Debug.Log("Normal Ending");
		}
		if(dc.GetBranch() == 56){
			Debug.Log("True Ending");
		}
		if(dc.GetBranch() == 57){
			Debug.Log("Good Ending");
		}
		if(dc.GetBranch() == 1){
			PlayerPrefs.SetInt ("isTimeTableClicked",1);
			PlayerPrefs.SetInt ("Item9",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 2){
			PlayerPrefs.SetInt ("isOwlDollClicked",1);
			PlayerPrefs.SetInt ("Item8",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 7){
			gameOver.SetActive (true);
		}
		if(dc.GetBranch() == 4 || dc.GetBranch() == 6){
			PlayerPrefs.SetInt ("Profile6",1);
			PlayerPrefs.SetInt ("auntyUnlocked",1);
			PlayerPrefs.SetInt ("taufikUnlocked",1);
			PlayerPrefs.SetInt ("isTaufikAlleywayGone",1);
			PlayerPrefs.SetInt ("isChiefClicked",1);
			PlayerPrefs.SetInt ("Show Evidence_taufik_Police_Station",1);
			aunty.SetActive (true);
			taufik.SetActive (true);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 8){
			PlayerPrefs.SetInt ("Profile1",1);
			PlayerPrefs.SetInt ("isAuntyClicked",1);
			PlayerPrefs.SetInt ("What did you see at 2PM?",1);
			PlayerPrefs.SetInt ("Show Evidence_aunty eva_Police_Station",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 10 || dc.GetBranch() == 11){
			PlayerPrefs.SetInt ("buttonBackPoliceStation",1);
			PlayerPrefs.SetInt ("isAuntyClickedq1",1);
			PlayerPrefs.SetInt ("alleywayMap",1);
			PlayerPrefs.SetInt ("Item3",1);
			buttonBack.SetActive (true);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 18){
			dc.SetIsFreezed (true);
			dc.StartDialogue (124,17);
			dc.NextDialogue();
		}
		if(dc.GetBranch() == 30){
			dc.StartDialogue (127,19);
			dc.NextDialogue();
		}
		if(dc.GetBranch() == 23){
			PlayerPrefs.SetInt("Where did you find the owl doll?",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch () == 40 || dc.GetBranch () == 41){
			PlayerPrefs.SetInt ("isTaufikClicked1", 1);
		}
		if(dc.GetBranch () == 42 || dc.GetBranch () == 43){
			PlayerPrefs.SetInt ("isTaufikClicked2", 1);	
		}
		if(dc.GetBranch() == 19 || dc.GetBranch() == 46 || dc.GetBranch() == 48 || dc.GetBranch() == 49 || dc.GetBranch() == 52 || 
			dc.GetBranch() == 59 || dc.GetBranch() == 60 || dc.GetBranch() == 62 || dc.GetBranch() == 67){
			dc.ShowEvidence ("Question");
		}
		if(dc.GetBranch() == 54){
			dc.StartDialogue (187,53);
			dc.NextDialogue();
		}
		if(dc.GetBranch() == 55){
			if (PlayerPrefs.GetInt ("trust") >= 3) {
				dc.StartDialogue (0,56);
				dc.NextDialogue();
			} else {
				dc.StartDialogue (0,57);
				dc.NextDialogue();
			}
		}
		if(dc.GetBranch() == 63){
			if (PlayerPrefs.GetInt ("trust") >= 3) {
				dc.StartDialogue (0,64);
				dc.NextDialogue();
			} else {
				dc.StartDialogue (0,65);
				dc.NextDialogue();
			}
		}
		if(dc.GetBranch() == 68){
			if (PlayerPrefs.GetInt ("trust") >= 3) {
				dc.StartDialogue (0,69);
				dc.NextDialogue();
			} else {
				dc.StartDialogue (0,70);
				dc.NextDialogue();
			}
		}
		if(dc.GetBranch() == 74 || dc.GetBranch() == 75 || dc.GetBranch() == 76){
			if (PlayerPrefs.GetInt ("trust") >= 3) {
				dc.StartDialogue (0,77);
				dc.NextDialogue();
			} else {
				dc.StartDialogue (0,78);
				dc.NextDialogue();
			}
		}

		if (PlayerPrefs.GetInt ("isTaufikClicked1") == 1 && PlayerPrefs.GetInt ("isTaufikClicked2") == 0) {
			PlayerPrefs.SetInt ("isTaufikClicked", 2);
		} else if (PlayerPrefs.GetInt ("isTaufikClicked1") == 0 && PlayerPrefs.GetInt ("isTaufikClicked2") == 1) {
			PlayerPrefs.SetInt ("isTaufikClicked", 3);
		} else if (PlayerPrefs.GetInt ("isTaufikClicked1") == 1 && PlayerPrefs.GetInt ("isTaufikClicked2") == 1) {
			PlayerPrefs.SetInt ("isTaufikClicked", 4);
		}
		if(dc.GetBranch() == 28 || dc.GetBranch() == 29 || dc.GetBranch() == 31){
			PlayerPrefs.SetInt ("isAuntyClickedq2", 1);
		}
		if(PlayerPrefs.GetInt("isAuntyClickedq1") == 1 && PlayerPrefs.GetInt("isAuntyClickedq2") == 0){
			PlayerPrefs.SetInt ("isAuntyClicked",2);
		}else if(PlayerPrefs.GetInt("isAuntyClickedq1") == 0 && PlayerPrefs.GetInt("isAuntyClickedq2") == 1){
			PlayerPrefs.SetInt ("isAuntyClicked",3);
		}else if(PlayerPrefs.GetInt("isAuntyClickedq1") == 1 && PlayerPrefs.GetInt("isAuntyClickedq2") == 1){
			PlayerPrefs.SetInt ("isAuntyClicked",4);
		}
	}
	// Update is called once per frame
	void Update () {
		if(dc.GetDialogueCounter() == 15 && isGetOut){
			if(dc.characterSprite.transform.localPosition.x <= 640){
				sm.PlaySFXCharacterMove ();
				dc.arrow.SetActive (false);
				dc.characterSprite.transform.localPosition = new Vector2 (dc.characterSprite.transform.localPosition.x+(450*Time.deltaTime),dc.characterSprite.transform.localPosition.y);
				dc.SetIsDialogue (false);
			}else{
				sm.UnplaySFXCharacterMove ();
				isGetOut = false;
				dc.SetIsDialogue (true);
				dc.SetIsClick (true);
				dc.arrow.SetActive (true);
			}
		}
		if((dc.GetDialogueCounter() == 165 || dc.GetDialogueCounter() == 181 || dc.GetDialogueCounter() == 203
			|| dc.GetDialogueCounter() == 219 || dc.GetDialogueCounter() == 227 || dc.GetDialogueCounter() == 258
			|| dc.GetDialogueCounter() == 267 || dc.GetDialogueCounter() == 276) && isGetIn){
			if(isSpriteGetIn){
				isSpriteGetIn = false;
				dc.characterSprite.transform.localPosition = new Vector2 (700, dc.characterSprite.transform.localPosition.y);
			}
			dc.characterSprite.gameObject.SetActive (true);
			c = dc.characterSprite.color;
			c.a = 1;
			dc.characterSprite.color = c;
			if (dc.GetDialogueCounter () == 165 || dc.GetDialogueCounter () == 181 || dc.GetDialogueCounter () == 203) {
				dc.characterSprite.sprite = GameObject.Find ("sprite_widya_happy").GetComponent<SpriteRenderer> ().sprite;
			} else {
				dc.characterSprite.sprite = GameObject.Find ("sprite_widya_sad").GetComponent<SpriteRenderer> ().sprite;
			}
			if(dc.characterSprite.transform.localPosition.x >= 249){
				sm.PlaySFXCharacterMove ();
				dc.arrow.SetActive (false);
				dc.characterSprite.transform.localPosition = new Vector2 (dc.characterSprite.transform.localPosition.x-(550*Time.deltaTime),dc.characterSprite.transform.localPosition.y);
				dc.SetIsDialogue (false);
			}else{
				sm.UnplaySFXCharacterMove ();
				dc.characterSprite.transform.localPosition = new Vector2 (249, dc.characterSprite.transform.localPosition.y);
				isGetIn = false;
				dc.SetIsDialogue (true);
				dc.SetIsClick (true);
				dc.NextDialogue();
			}
		}
	}
}
