using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preschool : MonoBehaviour {

	private bool isPaintClicked = false;
	private bool isGetOut = true;

	public DialogueController dc;
	public GameObject buttonBack;
	public GameObject parent;
	public SoundManager sm;

	public void BackToMap(){
		//sm.PlaySFXButtonClick();
		Application.LoadLevel ("Map");
	}
	public void PaintClicked(){
		GameObject.Find ("paint_tube").SetActive(false);
		dc.StartDialogue (0,1);
		isPaintClicked = true;
	}
	public void PictureFrameClicked(){
		dc.StartDialogue (0,2);
	}
	public void ChildrenPaintClicked(){
		dc.StartDialogue (0,3);
	}
	public void LindaClicked(){
		if (PlayerPrefs.GetInt ("isLindaClicked") == 0) {
			dc.StartDialogue (0, 4);
		} else if (PlayerPrefs.GetInt ("isLindaClicked") == 1) {
			dc.StartDialogue (0, 5);
		}else if(PlayerPrefs.GetInt ("isLindaClicked") == 2){
			dc.StartDialogue (0, 24);
		}else if(PlayerPrefs.GetInt ("isLindaClicked") == 3){
			dc.StartDialogue (0, 25);
		} else if(PlayerPrefs.GetInt ("isLindaClicked") == 4){
			dc.StartDialogue (0, 17);
		}
	}
	// Use this for initialization
	void Start () {
		parent.SetActive(true);
		//PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetString("currentScene","Preschool");
		PlayerPrefs.SetInt("Item7",1);
		if(PlayerPrefs.GetInt("isPaintClicked") == 1){
			isPaintClicked = true;
			PlayerPrefs.SetInt ("Item5",1);
			GameObject.Find ("paint_tube").SetActive(false);
		}
		if(PlayerPrefs.GetInt("buttonBackPreschool") == 1){
			buttonBack.SetActive (true);
		}
		dc.SetIsDialogue (false);
	}
	public void FinishDialogue(){
		if(dc.GetBranch() == 4){
			PlayerPrefs.SetInt ("isLindaClicked",1);
			PlayerPrefs.SetInt ("When did you last see Wanda?",1);
			PlayerPrefs.SetInt ("Show Evidence_linda_Preschool",1);
			PlayerPrefs.SetInt ("Profile4",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 19 || dc.GetBranch() == 20){
			PlayerPrefs.SetInt ("isLindaClickedq1",1);
		}else if(dc.GetBranch() == 13 || dc.GetBranch() == 16){
			PlayerPrefs.SetInt ("isLindaClickedq2",1);
		}
		if(PlayerPrefs.GetInt ("isLindaClickedq1") == 1 && PlayerPrefs.GetInt ("isLindaClickedq2") == 0){
			PlayerPrefs.SetInt ("isLindaClicked",2);
		}else if(PlayerPrefs.GetInt ("isLindaClickedq1") == 0 && PlayerPrefs.GetInt ("isLindaClickedq2") == 1){
			PlayerPrefs.SetInt ("isLindaClicked",3);
		}else if(PlayerPrefs.GetInt ("isLindaClickedq1") == 1 && PlayerPrefs.GetInt ("isLindaClickedq2") == 1){
			PlayerPrefs.SetInt ("isLindaClicked",4);
		}
		if(isPaintClicked){
			PlayerPrefs.SetInt ("isPaintClicked",1);
			PlayerPrefs.SetInt ("Item5",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 19 || dc.GetBranch() == 20 || dc.GetBranch() == 21){
			//UNLOCK POLICE STATION & BUTTON BACK
			PlayerPrefs.SetInt("buttonBackPreschool",1);
			PlayerPrefs.SetInt ("policeStationMap",1);
			buttonBack.SetActive (true);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 9){
			PlayerPrefs.SetInt ("About the Angel of Death Case...",1);
			PlayerPrefs.Save ();
		}
		if(dc.GetBranch() == 10){
			PlayerPrefs.SetInt ("isKrisShownToLinda",1);
			PlayerPrefs.Save ();
		}
	}
	// Update is called once per frame
	void Update () {
		if(dc.GetDialogueCounter() == 84 && isGetOut){
			if(dc.characterSprite.transform.localPosition.x <= 640){
				sm.PlaySFXCharacterMove ();
				dc.arrow.SetActive (false);
				dc.characterSprite.transform.localPosition = new Vector2 (dc.characterSprite.transform.localPosition.x+(400*Time.deltaTime),dc.characterSprite.transform.localPosition.y);
				dc.SetIsDialogue (false);
			}else{
				sm.UnplaySFXCharacterMove ();
				isGetOut = false;
				dc.SetIsDialogue (true);
				dc.SetIsClick (true);
				dc.arrow.SetActive (true);
			}
		}

	}
}
