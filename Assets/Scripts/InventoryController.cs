using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

	public GameObject InventoryDetailGameObject;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject buttonShowEvidence;
	public GameObject rightButtonHover;
	public Text pageText;
	public Text NameText;
	public Text DescriptionText;
	public Image ImageItem;
	public DialogueController dc;
	public SoundManager sm;
	GameObject temp;
	Color c;

	//pie, wanda case files, cctv, tire, paint, spray paint, angel of death, owl doll, timetable
	string [] itemName = {"Crushed snacks", "Wanda case files", "Aunty Eva\'s Photograph", "Tire marks", "Paint", "Spray paint", "Angel of death case files", "Owl doll", "Timetable"};
	string [] item = {"A box of crushed cream puffs found near the location of the murders. \n" +
		"Despite being crushed up, it still seems freshly baked with no signs of rotting.\n" +
		"The box indicates that it was purchased at Aunty Eva’s Snacks, " +
		"and two slices of cherry pie seem to be inside the box. Yum!\n\n", 
		"A hastily put together case files of Wanda’s incident, especially created for " +
		"Widya and Jo’s investigational purpose. Not much information has been found. —" +
		"These notes were hastily scribbled by Officer Taufik:\n\n22/9. " +
		"The victim, W, was found at 3:50 PM by Officer Taufik, posed under the “Angel Wings” mural. —" +
		"W suffered mild blunt force trauma to the abdomen.  As of 4:00 PM, W is in stable condition and under " +
		"medical care.—Case has several similarities to the Angel of Death Cases.\n", "A shot from the CCTV footage taken from the store Aunty Eva’s Pies at the approximate time " +
		"of the incident.—It is slightly grainy, but the captured shot shows the “punk” that Aunty Eva suspected buying " +
		"pies in her shop.\n", "A photograph of the tire marks of the police motorcycle Officer " +
		"Taufik used at the day of the incident. —" +
		"The motorcycle was commonly used by different officers that is patrolling the area during their " +
		"allocated shifts. The pattern looks awfully similar to something that was seen before.", "A paint tube with the same colour as the mural in the alleyway. " +
		"Found in Wanda’s Preschool. Does it have a connection to the mural?","A spray paint can found in the alleyway. \n\t" +
		"Its colour matches a wet patch of paint near Wanda when she was initially found.—" +
		"The owner of the spray paint might be a witness, or even a suspect of the case.\n", "Case files of the Angel of Death murders documented by the police officers. " +
		"It has steadily became more bulky with every case. Three suspected related cases were attached inside." +
		"—VICTIM L (redacted)\n\nEstimated Time of Death: 11/7. 2:00 to 4:00 PM.\nCause:  " +
		"Strangulation.—Murder Weapon: Rope.\n\n" +
		"Victim was found under the “Angel Wings” mural in District 8 (photographed).—" +
		"No witnesses had come forward and little evidence was found.—VICTIM C (redacted)\n\n" +
		"Estimated Time of Death: 3/8. 2:00 to 4:00 PM.\nCause: Strangulation.—Murder Weapon: Rope.\n\n" +
		"Victim was found under the “Angel Wings” mural.—Despite disparities in murder weapon, the posing of the victim’s" +
		"body bears a striking resemblance to a previous case.—It has to be noted that the case may be related to" +
		"VICTIM L’s case (11/7). \nNo witnesses had come forward.—VICTIM J (redacted)\n\nEstimated Time of Death: " +
		"20/9. 2:00 to 4:00 PM.\nCause: Strangulation.—Murder Weapon: Rope.\n\nVictim was found under the “Angel Wings” " +
		"mural.—Similarity in murder weapon and location links case to VICTIM C (3/8), and possibly VICTIM L (11/7). \n" +
		"No witnesses had come forward.\n", "Wanda’s precious owl doll. It was somehow in the possession of Aunty Eva.\n" +
		"It is made of a squishy, soft material.—Patterned dirt marks was found on the owl doll’s body," +
		" which suggests a common source.\n", "The police officer’s timetable, hanging on the walls of the police station.—" +
		"It might help with confirming the time of death.\n\n" +
		"On 22/9, Officer Taufik was on patrol duty from 3:30 PM to 6:00 PM as reported.—" +
		"The officer’s patrol duty shifts to different timings each date. \n"};

	//aunty, taufik, wanda, linda, punk, chief
	string [] profileName = {"Aunty", "Taufik", "Wanda", "Linda", "Kris", "Chief"};
	string[] profile = {"Never reluctant to offer her opinions about current news and neighbouring residents, Aunty Eva " +
		"is an iconic figure of District 8’s shophouses.—The one word that people would describe her as is “loud”. —" +
		"She sells different kinds of pies, freshly baked and homemade, at her shop Aunty Eva’s Pies,—a popular place " +
		"for young children and even adults with a sweet tooth.","Originally from District 1, Taufik is the Chief’s trusty assistant, who often handles “tedious” " +
		"paperworks and carries all the case files and evidences for the Chief everywhere he goes.—" +
		"And thus, it is a matter of fact that the Chief selected Taufik to relocate to District 8 with him.—" +
		"Clumsy, awkward, and a worrywart, sometimes he would stumble into hijinks that lightens the mood for other " +
		"officers.\n","Widya’s little sister who is the victim of the crime.\n" +
		"Often described as bubbly and carefree, she’s chatty around people, even those who are strangers.—" +
		"She always carries an owl doll with her, and thus, it was odd that she was found without it. \n","A teacher in the preschool that Wanda goes to. It also happens to be the only preschool in the " +
		"district. Thus, she is often overwhelmed.—Even so, Linda always does her best, often carrying a hand puppet " +
		"with her to cheer up crying children. It is said that she remembers each and every single student’s names.", "Not much is known about Kris. Often spotted hanging around the seedy alleyways " +
		"of District 8 and not much elsewhere, residents had many speculations of his identity.—" +
		"A popular theory is that he is a founder of a gang, but they are unsure of what gang that would be, " +
		"or what their activities were.","As the District 1 Chief of the Police Force, it is also his job to overlook the other district’s " +
		"branches, and help out in times of need.—Due to the Angel of Death cases and the rise of other criminal " +
		"activities in District 8, the Chief had to relocate, bringing several officers, Taufik, and his daughter Jo " +
		"with him.—Charismatic and extremely well-respected, there are very few officers who dared to go against him."};
	string [] tempDesc = new string[100];
	string itemTemp = "";
	string profileTemp = "";
	int maxIndex = 0;
	int currIndex = 0;

	public void HoverRightButton(){
		rightButtonHover.SetActive (true);
	}
	public void UnhoverRightButton(){
		rightButtonHover.SetActive (false);
	}
	public void RespawnInventory(int isShowButton){
		itemTemp = "";
		profileTemp = "";
		if (isShowButton == 0) {
			buttonShowEvidence.SetActive (false);
		} else {
			buttonShowEvidence.SetActive (true);
		}
		for(int i=1; i<=6; i++){
			//PlayerPrefs.SetInt ("Profile"+i,1);
			if (PlayerPrefs.GetInt ("Profile" + i) == 0) {
				temp = GameObject.FindGameObjectWithTag ("Profile" + i);
				c = temp.GetComponent<Image> ().color;
				c.a = 0;
				temp.GetComponent<Image> ().color = c;
			} else {
				temp = GameObject.FindGameObjectWithTag ("Profile" + i);
				c = temp.GetComponent<Image> ().color;
				c.a = 1;
				temp.GetComponent<Image> ().color = c;
			}
		}

		//item
		for(int i=1; i<=9; i++){
			//PlayerPrefs.SetInt ("Item"+i,1);
			if (PlayerPrefs.GetInt ("Item" + i) == 0) {
				temp = GameObject.FindGameObjectWithTag ("Item" + i);
				c = temp.GetComponent<Image> ().color;
				c.a = 0;
				temp.GetComponent<Image> ().color = c;
			} else {
				temp = GameObject.FindGameObjectWithTag ("Item" + i);
				c = temp.GetComponent<Image> ().color;
				c.a = 1;
				temp.GetComponent<Image> ().color = c;
			}
		}
	}
	public void Show(int index){
		//sm.PlaySFXButtonClick();
		InventoryDetailGameObject.SetActive (false);
		dc.Inventory.SetActive (false);
		dc.dialogueGameObject.SetActive (true);
		dc.StartDialogue (0,index);
		dc.NextDialogue ();
	}
	public void ItemDetail(int index){
		if(PlayerPrefs.GetInt("Item"+(index+1)) == 1){
			sm.PlaySFXButtonClick();
			itemTemp = "Item"+(index+1);
			NameText.text = itemName[index];
			tempDesc = item[index].Split(new string[] {"—"}, StringSplitOptions.None);
			maxIndex = tempDesc.Length;
			pageText.text = "1/"+maxIndex;
			currIndex = 0;
			leftArrow.SetActive (false);
			rightArrow.SetActive (true);
			if(maxIndex < 2){
				rightArrow.SetActive (false);
			}
			DescriptionText.text = tempDesc[0];
			ImageItem.sprite = GameObject.FindGameObjectWithTag("DetailItem"+(index+1)).GetComponent<Image>().sprite;
			InventoryDetailGameObject.SetActive (true);
			buttonShowEvidence.GetComponent<Button> ().onClick.RemoveAllListeners ();
			buttonShowEvidence.GetComponent<Button> ().onClick.AddListener (
				delegate{
					if(Application.loadedLevelName == "Alleyway"){
						if(dc.characterNameText.text.ToString() == "taufik"){
							if(itemTemp == "Item7"){
								Show(15);
							}else if(itemTemp == "Item2"){
								Show(16);
							}else{
								Show(17);
							}
						}else{
							if(PlayerPrefs.GetString("ShowEvidenceType") == "ShowEvidence"){
								if(itemTemp == "Item1"){
									Show(25);
								}else if(itemTemp == "Item3"){
									Show(24);
								}else if(itemTemp == "Item6"){
									Show(26);
								}else{
									Show(27);
								}
							}else if(PlayerPrefs.GetString("ShowEvidenceType") == "Question"){
								//question 1
								if(dc.GetBranch() == 31){
									Show(33);
								}
								//question 2
								if(dc.GetBranch() == 35 || dc.GetBranch() == 36){
									if(itemTemp == "Item1" || itemTemp == "Item6"){
										Show(37);
									}else{
										Show(38);
									}
								}
							}
						}
					}
					if(Application.loadedLevelName == "Preschool"){
						if(itemTemp == "Item2"){ //wanda case files
							Show(7);
						}else if(itemTemp == "Item5"){ //paint
							Show(8);
						}else if(itemTemp == "Item7"){ //angel of death
							Show(9);
						}else{
							Show(11);
						}
					}
					if(Application.loadedLevelName == "Police_Station"){
						if(PlayerPrefs.GetString("ShowEvidenceType") == "ShowEvidence"){
							if(dc.characterNameText.text.ToString() == "taufik"){
								if(itemTemp == "Item7"){
									Show(39);
								}else if(itemTemp == "Item2"){
									Show(44);
								}else{
									Show(45);
								}
							}else if(dc.characterNameText.text.ToString() == "aunty eva"){
								if(itemTemp == "Item8"){
									Show(23);
								}else if(itemTemp == "Item1"){
									Show(25);
								}else{
									Show(26);
								}
							}
						}else if(PlayerPrefs.GetString("ShowEvidenceType") == "Question"){
							if(PlayerPrefs.GetInt("policeStationPhase") == 2){
								if(dc.GetBranch() == 48 || dc.GetBranch() == 67){
									if(itemTemp == "Item4"){ //tire marks
										Show(49);
									}else{ //other evidence
										Show(66);
									}
								}else if(dc.GetBranch() == 49 || dc.GetBranch() == 62){
									if(itemTemp == "Item8"){
										Show(50);
									}else{
										Show(61);
									}
								}else if(dc.GetBranch() == 52 || dc.GetBranch() == 59 || dc.GetBranch() == 60){
									if(itemTemp == "Item7"){
										Show(53);
									}else{
										Show(58);
									}
								}else{
									//awal2 player kliknya item
								}
							}else{
								if(itemTemp == "Item8"){
									Show(29);
								}else{
									Show(30);
								}
							}
						}
					}
				}
			);
		}
	}
	public void ProfileDetail(int index){
		if(PlayerPrefs.GetInt("Profile"+(index+1)) == 1){
			sm.PlaySFXButtonClick();
			profileTemp = "Profile"+(index+1);
			NameText.text = profileName[index];
			tempDesc = profile[index].Split(new string[] {"—"}, StringSplitOptions.None);
			maxIndex = tempDesc.Length;
			pageText.text = "1/"+maxIndex;
			currIndex = 0;
			leftArrow.SetActive (false);
			rightArrow.SetActive (true);
			if(maxIndex < 2){
				rightArrow.SetActive (false);
			}
			DescriptionText.text = tempDesc[0];
			ImageItem.sprite = GameObject.FindGameObjectWithTag("DetailProfile"+(index+1)).GetComponent<Image>().sprite;
			InventoryDetailGameObject.SetActive (true);
			buttonShowEvidence.GetComponent<Button> ().onClick.RemoveAllListeners ();
			buttonShowEvidence.GetComponent<Button> ().onClick.AddListener (
				delegate{
					if(Application.loadedLevelName == "Alleyway"){
						if(dc.characterNameText.text.ToString() == "taufik"){
							Show(17);
						}else{
							if(PlayerPrefs.GetString("ShowEvidenceType") == "Question"){
								if(profileTemp == "Profile4"){
									if(PlayerPrefs.GetInt ("isKrisShownToLinda") == 0){
										Show(32);
									}else{
										Show(44);
									}
								}else{
									Show(33);
								}
							}else if(PlayerPrefs.GetString("ShowEvidenceType") == "ShowEvidence"){
								Show(27);
							}
						}
					}else if(Application.loadedLevelName == "Preschool"){
						if(profileTemp == "Profile5"){ //wanda case files
							Show(10);
						}else{
							Show(11);
						}
					}else if(Application.loadedLevelName == "Police_Station"){
						if(PlayerPrefs.GetString("ShowEvidenceType") == "ShowEvidence"){
							if(dc.characterNameText.text.ToString() == "aunty eva"){
								if(profileTemp == "Profile5"){
									Show(24);
								}else{
									Show(26);
								}
							}else if(dc.characterNameText.text.ToString() == "taufik"){
								if(profileTemp == "Profile3"){
									Show(44);
								}else{
									Show(45);
								}
							}
						}else if(PlayerPrefs.GetString("ShowEvidenceType") == "Question"){
							if(PlayerPrefs.GetInt("policeStationPhase") == 2){
								if(dc.GetBranch() == 46){
									if(profileTemp == "Profile5"){
										Show(47);
									}else if(profileTemp == "Profile2"){
										Show(48);
									}else if(profileTemp == "Profile6"){
										Show(73);
									}else if(profileTemp == "Profile1"){
										Show(75);
									}else if(profileTemp == "Profile3"){
										Show(76);
									}else if(profileTemp == "Profile4"){
										Show(74);
									}
								}
							}else{
								if(profileTemp == "Profile3"){
									Show(27);
								}else{
									Show(30);
								}
							}
						}
					}
				}
			);
		}
	}
	public void nextDesc(){
		if(currIndex + 1 < maxIndex){
			sm.PlaySFXNotepadTurn();
			leftArrow.SetActive (true);
			currIndex++;
			pageText.text = (currIndex+1)+"/"+maxIndex;
			DescriptionText.text = tempDesc[currIndex];
			if(currIndex == maxIndex - 1){
				UnhoverRightButton ();
				rightArrow.SetActive (false);
			}
		}
	}
	public void prevDesc(){
		if(currIndex - 1 >= 0){
			sm.PlaySFXNotepadTurn();
			rightArrow.SetActive (true);
			currIndex--;
			pageText.text = (currIndex+1)+"/"+maxIndex;
			DescriptionText.text = tempDesc[currIndex];
			if(currIndex == 0){
				leftArrow.SetActive (false);
			}
		}
	}
	public void BackToInventory(){
		sm.PlaySFXButtonClick();
		InventoryDetailGameObject.SetActive (false);
		itemTemp = "";
		profileTemp = "";
	}
	public void CloseInventory(){
		sm.PlaySFXButtonClick();
		this.gameObject.SetActive (false);
		itemTemp = "";
		profileTemp = "";
	}
}