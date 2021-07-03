using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour {


	struct DialogueLine{
		public string characterName;
		public string content;
		public int branch;
		public string dialogueType;
		public string spriteName;
		public string choice1;
		public int branchChoice1;
		public string choice2;
		public int branchChoice2;
		public string choice3;
		public int branchChoice3;
		public string isTrust;

		public DialogueLine(string cn, string c, int b, string dt, string sn, string c1, int bc1, string c2, int bc2, string c3, int bc3, string it){
			characterName = cn;
			content = c;
			branch = b;
			dialogueType = dt;
			spriteName = sn;
			choice1 = c1;
			branchChoice1 = bc1;
			choice2 = c2;
			branchChoice2 = bc2;
			choice3 = c3;
			branchChoice3 = bc3;
			isTrust = it;
		}
	}

	List <DialogueLine> lines;
	private bool isClick = true;
	private bool isStart = false;
	private bool isFreezed = false;
	private bool isFinish = false;
	private bool isDialogue = false;
	private int dialogueCounter = 0;
	private int branch = 0;
	private int dialogueLine = 0;
	private string characterName = "";

	//UI
	public Text dialogueText;
	public Text characterNameText;
	public Text titleEvidenceText;
	public Text narationEvidenceText;
	public Image characterSprite;
	public Image characterSpriteEvidenceFound;
	public Image imageItem;
	public Image evidencePopUpImage;
	public SpriteRenderer hoverChoice;
	public SpriteRenderer unhoverChoice;
	public SpriteRenderer hoverQuestion;
	public SpriteRenderer unhoverQuestion;
	public SpriteRenderer hoverInventory;
	public SpriteRenderer unhoverInventory;
	public GameObject arrow;
	public GameObject dialogueGameObject;
	public GameObject evidenceFoundScreenGameObject;
	public GameObject evidencePopUpGameObject;
	public GameObject [] buttonChoice = new GameObject[4];
	public GameObject [] buttonQuestion = new GameObject[4];
	public GameObject [] buttonQuestionLock = new GameObject[4];
	public GameObject buttonCancelQuestion;
	public GameObject Inventory;
	public SoundManager sm;
	Color c;

	void Start(){
		string file = "";
		string currentScene = Application.loadedLevelName;
		file += currentScene;
		file += ".txt";

		lines = new List<DialogueLine> ();
		LoadDialogue (file);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Space)){
			if(isDialogue){
				if(!isStart){
					isStart = true;
					dialogueGameObject.SetActive (true);
				}
				if (isClick == false) {
					characterSprite.transform.localPosition = new Vector2(249,-21);
					//set full dialogue
					SetFullDialogue();
				} else {
					characterSprite.transform.localPosition = new Vector2(249,-21);
					//next dialogue
					if(!isFreezed){
						NextDialogue ();
					}
				}
			}
		}
	}
	public void ShowEvidencePopUp(string spriteName){
		evidencePopUpImage.sprite = GameObject.Find (spriteName).GetComponent<SpriteRenderer> ().sprite;
		evidencePopUpGameObject.SetActive (true);
	}
	public void ShowEvidenceFoundScreen(string narationText, string titleNaration, string spriteName, string charterSprite){
		imageItem.sprite = GameObject.Find (spriteName).GetComponent<SpriteRenderer> ().sprite;
		characterSpriteEvidenceFound.sprite = GameObject.Find (charterSprite).GetComponent<SpriteRenderer> ().sprite;
		titleEvidenceText.text = "" + titleNaration;
		narationEvidenceText.text = ""+narationText;
		evidenceFoundScreenGameObject.SetActive (true);
		sm.PlaySFXRegisteredEvidence();
	}
	public void StartDialogue(int dialogueCounter, int branch){
		SetIsStart (false);
		SetIsClick (true);
		SetIsFinish (false);
		SetDialogueCounter (dialogueCounter);
		SetBranch (branch);
		SetIsDialogue (true);
	}
	public void SetFullDialogue(){
		if (GetDialogueType (dialogueCounter) == "choice" || GetDialogueType (dialogueCounter) == "question") {
			isFreezed = true;
		} else {
			isFreezed = false;
		}
		if(GetDialogueType(dialogueCounter) == "normal" || GetDialogueType(dialogueCounter) == "choice" || GetDialogueType(dialogueCounter) == "question"){
			arrow.SetActive (true);
			StopAllCoroutines();
			dialogueText.text = GetContent(dialogueCounter);
			if(GetDialogueType(dialogueCounter) == "choice"){
				CreateChoice (GetChoice1(dialogueCounter), GetChoice2(dialogueCounter), GetChoice3(dialogueCounter), GetBranchChoice1(dialogueCounter), GetBranchChoice2(dialogueCounter), GetBranchChoice3(dialogueCounter));
			}else if(GetDialogueType(dialogueCounter) == "question"){
				CreateQuestion (GetChoice1(dialogueCounter), GetChoice2(dialogueCounter), GetChoice3(dialogueCounter), GetBranchChoice1(dialogueCounter), GetBranchChoice2(dialogueCounter), GetBranchChoice3(dialogueCounter));
			}
			isClick = true;
			dialogueCounter++;
		}else if(GetDialogueType(dialogueCounter) == "evidence_found_screen" || GetDialogueType(dialogueCounter) == "evidence_pop_up"){
			isClick = true;
			dialogueCounter++;
			NextDialogue ();
		}


	}
	public void NextDialogue(){
		arrow.SetActive (false);
		isFinish = false;
		dialogueText.text = "";
		dialogueGameObject.SetActive (false);
		evidenceFoundScreenGameObject.SetActive (false);
		evidencePopUpGameObject.SetActive (false);
		for(int i=dialogueCounter; i<=dialogueLine; i++){
			dialogueCounter = i;
			if((GetBranch(i) == branch || GetBranch(i) == 0) && i!=dialogueLine){
				if(GetIsTrust(i) == "yesTrust"){
					PlayerPrefs.SetInt ("trust", PlayerPrefs.GetInt("trust") + 1);
					Debug.Log ("trust: "+PlayerPrefs.GetInt("trust"));
				}
				if(GetDialogueType(i) == "normal" || GetDialogueType(i) == "choice" || GetDialogueType(i) == "question"){
					StopAllCoroutines ();
					StartCoroutine(DisplayDialogue(GetContent(i),GetCharacterName(i),GetSpriteName(i)));
					dialogueGameObject.SetActive (true);
				}else if(GetDialogueType(i) == "evidence_found_screen"){
					ShowEvidenceFoundScreen (GetContent (i), GetCharacterName (i), GetSpriteName (i), GetSpriteName(i-1));
				}else if(GetDialogueType(i) == "evidence_pop_up"){
					ShowEvidencePopUp (GetSpriteName (i));
				}
				if(GetBranch(i) == 0){
					branch = 0;
				}
				break;
			}
		}
		isClick = false;
		if(dialogueCounter >= dialogueLine){
			isFinish = true;
			GameObject g = GameObject.Find ("Main Camera");
			if(PlayerPrefs.GetString("currentScene") == "Alleyway"){
				g.GetComponent<Alleyway> ().FinishDialogue ();
			}else if(PlayerPrefs.GetString("currentScene") == "Preschool"){
				g.GetComponent<Preschool> ().FinishDialogue ();
			}else if(PlayerPrefs.GetString("currentScene") == "Police_Station"){
				g.GetComponent<PoliceStation> ().FinishDialogue ();
			}
		}
	}

	IEnumerator DisplayDialogue(string text, string characterName, string spriteName){
		if (spriteName == "") {
			c = characterSprite.color;
			c.a = 0;
			characterSprite.color = c;
			characterSprite.sprite = null;
		} else {
			characterSprite.gameObject.SetActive (true);
			c = characterSprite.color;
			c.a = 1;
			characterSprite.color = c;
			characterSprite.sprite = GameObject.Find (spriteName).GetComponent<SpriteRenderer> ().sprite;
		}
		characterNameText.text = ""+characterName;
		for(int i=0; i<text.Length; i++){
			if(!Inventory.activeSelf)sm.PlaySFXDialogueNext ();
			dialogueText.text = dialogueText.text + text [i];
			if (text [i] == ',' || text [i] == '-') {
				yield return new WaitForSeconds (0.4f);
			} else if (text [i] == '.' || text[i] == '?' || text[i] == '!') {
				yield return new WaitForSeconds (0.7f);
			} else {
				yield return new WaitForSeconds (0.03f);
			}
		}
		SetFullDialogue ();
	}
	void CreateChoice(string choice1, string choice2, string choice3, int branch1, int branch2, int branch3){
		for(int i=0; i<3; i++){
			buttonChoice [i].GetComponent<Button> ().onClick.RemoveAllListeners ();
		}
		int branchTemp = 0;
		if(choice1 != ""){
			buttonChoice [0].SetActive (true);
			buttonChoice [0].GetComponent<Button> ().onClick.AddListener (
				delegate {
					branchTemp = branch1;
					ButtonChoice(branchTemp);
					for(int i=0; i<3; i++){
						UnhoverChoiceButton(i);
						buttonChoice [i].SetActive (false);
					}
				});
			GameObject.Find (buttonChoice[0].name+"Text").GetComponent<Text>().text = ""+choice1;
		}
		if(choice2 != ""){
			branchTemp = 0;
			buttonChoice [1].SetActive (true);
			buttonChoice [1].GetComponent<Button> ().onClick.AddListener (
				delegate {
					branchTemp = branch2;
					ButtonChoice(branchTemp);
					for(int i=0; i<3; i++){
						UnhoverChoiceButton(i);
						buttonChoice [i].SetActive (false);
					}
				});
			GameObject.Find (buttonChoice[1].name+"Text").GetComponent<Text>().text = ""+choice2;
		}
		if(choice3 != ""){
			branchTemp = 0;
			buttonChoice [2].SetActive (true);
			buttonChoice [2].GetComponent<Button> ().onClick.AddListener (
				delegate {
					branchTemp = branch3;
					ButtonChoice(branchTemp);
					for(int i=0; i<3; i++){
						UnhoverChoiceButton(i);
						buttonChoice [i].SetActive (false);
					}
				});
			GameObject.Find (buttonChoice[2].name+"Text").GetComponent<Text>().text = ""+choice3;
		}
	}
	void CreateQuestion(string question1, string question2, string question3, int branch1, int branch2, int branch3){
		for(int i=0; i<3; i++){
			buttonQuestion [i].GetComponent<Button> ().onClick.RemoveAllListeners ();
		}
		int branchTemp = 0;
		if (PlayerPrefs.GetInt ("" + question1) == 1 && question1 != "") {
			buttonQuestionLock [0].SetActive (false);
			buttonQuestion [0].SetActive (true);
			buttonQuestion[0].GetComponent<Button>().onClick.AddListener(
				delegate{
					branchTemp = branch1;
					ButtonChoice(branchTemp);
					for(int i=0; i<3; i++){
						buttonQuestion [i].SetActive (false);
					}
					for(int i=0; i<2; i++){
						UnhoverQuestionButton(i);
						buttonQuestionLock [i].SetActive (false);
					}
				});
			GameObject.Find (buttonQuestion [0].name + "Text").GetComponent<Text> ().text = "" + question1;
		} else {
			buttonQuestionLock [0].SetActive (true);
		}
		branchTemp = 0;
		if (PlayerPrefs.GetInt ("" + question2) == 1 && question2 != "") {
			buttonQuestionLock [1].SetActive (false);
			buttonQuestion [1].SetActive (true);
			buttonQuestion[1].GetComponent<Button>().onClick.AddListener(
				delegate{
					branchTemp = branch2;
					ButtonChoice(branchTemp);
					for(int i=0; i<3; i++){
						buttonQuestion [i].SetActive (false);
					}
					for(int i=0; i<2; i++){
						UnhoverQuestionButton(i);
						buttonQuestionLock [i].SetActive (false);
					}
				});
			GameObject.Find (buttonQuestion [1].name + "Text").GetComponent<Text> ().text = "" + question2;
		} else {
			buttonQuestionLock [1].SetActive (true);
		}
		branchTemp = 0;
		if (PlayerPrefs.GetInt ("" +question3+"_"+characterNameText.text.ToString()+"_"+Application.loadedLevelName) == 1 && question3 != "") {
			//buttonQuestionLock [2].SetActive (false);
			buttonQuestion [2].SetActive (true);
			buttonQuestion[2].GetComponent<Button>().onClick.AddListener(
				delegate{
					UnhoverInventoryButton();
					ShowEvidence();
				});
			//GameObject.Find (buttonQuestion [2].name + "Text").GetComponent<Text> ().text = "" + question3;
		} else {
			buttonQuestionLock [2].SetActive (true);
		}
		buttonCancelQuestion.GetComponent<Button> ().onClick.RemoveAllListeners ();
		buttonCancelQuestion.GetComponent<Button> ().onClick.AddListener (
			delegate {
				CancelQuestion();
			});
		buttonCancelQuestion.SetActive (true);
	}
	public void HoverChoiceButton(int index){
		buttonChoice[index].GetComponent<Image>().sprite = hoverChoice.sprite;
		buttonChoice[index].transform.localScale = new Vector2(1.2f,1.2f);
	}
	public void UnhoverChoiceButton(int index){
		buttonChoice[index].GetComponent<Image>().sprite = unhoverChoice.sprite;
		buttonChoice[index].transform.localScale = new Vector2(1f,1f);
	}
	public void HoverQuestionButton(int index){
		buttonQuestion[index].GetComponent<Image>().sprite = hoverQuestion.sprite;
		buttonQuestion[index].transform.localScale = new Vector2(1.2f,1.2f);
	}
	public void UnhoverQuestionButton(int index){
		buttonQuestion[index].GetComponent<Image>().sprite = unhoverQuestion.sprite;
		buttonQuestion[index].transform.localScale = new Vector2(1f,1f);
	}
	public void HoverInventoryButton(){
		buttonQuestion[2].GetComponent<Image>().sprite = hoverInventory.sprite;
		buttonQuestion[2].transform.localScale = new Vector2(1.2f,1.2f);
	}
	public void UnhoverInventoryButton(){
		buttonQuestion[2].GetComponent<Image>().sprite = unhoverInventory.sprite;
		buttonQuestion[2].transform.localScale = new Vector2(1f,1f);
	}
	public void ShowInventory(){
		sm.PlaySFXButtonClick();
		isFreezed = true;
		CancelQuestion ();
		Inventory.SetActive (true);
		Inventory.GetComponent<InventoryController> ().RespawnInventory (0);
	}
	public void ShowEvidence(string type = "ShowEvidence", int isShowButton = 1){
		//sm.PlaySFXButtonClick();
		PlayerPrefs.SetString ("ShowEvidenceType",""+type);
		isFreezed = true;
		CancelQuestion ();
		Inventory.SetActive (true);
		Inventory.GetComponent<InventoryController> ().RespawnInventory (isShowButton);
	}

	public void ButtonChoice(int choice){
		//sm.PlaySFXButtonClick();
		branch = choice;
		isFreezed = false;
		buttonCancelQuestion.SetActive (false);
		NextDialogue ();
	}
	public void CancelQuestion(){
		//sm.PlaySFXButtonClick();
		dialogueCounter = dialogueLine;
		for(int i=0; i<3; i++){
			buttonQuestion [i].SetActive (false);
		}
		for(int i=0; i<2; i++){
			UnhoverQuestionButton(i);
			buttonQuestionLock [i].SetActive (false);
		}
		isFreezed = false;
		buttonCancelQuestion.SetActive (false);
		arrow.SetActive (false);
		isFinish = false;
		dialogueText.text = "";
		dialogueGameObject.SetActive (false);
		evidenceFoundScreenGameObject.SetActive (false);
		evidencePopUpGameObject.SetActive (false);
		characterSprite.gameObject.SetActive (false);
		//NextDialogue ();
	}

	void LoadDialogue(string filename){
		string file = "Dialogue/"+filename;
		string line;
		StreamReader r = new StreamReader (file);
		using (r) {
			do{
				line = r.ReadLine();
				if(line != null){
					string [] line_values = SplitCsvLine(line);
					DialogueLine line_entry = new DialogueLine(line_values[0], line_values[1], int.Parse(line_values[2]), line_values[3], line_values[4], line_values[5], int.Parse(line_values[6]), line_values[7], int.Parse(line_values[8]), line_values[9], int.Parse(line_values[10]), line_values[11]);
					lines.Add(line_entry);
					dialogueLine++;
				}
			}while(line != null);
			r.Close ();
		}
	}

	string[] SplitCsvLine(string line){
		string pattern = @"
	     # Match one value in valid CSV string.
	     (?!\s*$)                                      # Don't match empty last value.
	     \s*                                           # Strip whitespace before value.
	     (?:                                           # Group for value alternatives.
	       '(?<val>[^'\\]*(?:\\[\S\s][^'\\]*)*)'       # Either $1: Single quoted string,
	     | ""(?<val>[^""\\]*(?:\\[\S\s][^""\\]*)*)""   # or $2: Double quoted string,
	     | (?<val>[^,'""\s\\]*(?:\s+[^,'""\s\\]+)*)    # or $3: Non-comma, non-quote stuff.
	     )                                             # End group of value alternatives.
	     \s*                                           # Strip whitespace after value.
	     (?:,|$)                                       # Field ends on comma or EOS.
	     ";
		string[] values = (from Match m in Regex.Matches(line, pattern, 
			RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
			select m.Groups[1].Value).ToArray();
		return values;        
	}

	public string GetCharacterName(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].characterName;
		}
		return "";
	}

	public string GetContent(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].content;
		}
		return "";
	}
	public int GetBranch(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].branch;
		}
		return 0;
	}
	public string GetDialogueType(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].dialogueType;
		}
		return "";
	}
	public string GetSpriteName(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].spriteName;
		}
		return "";
	}
	public string GetChoice1(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].choice1;
		}
		return "";
	}
	public int GetBranchChoice1(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].branchChoice1;
		}
		return 0;
	}
	public string GetChoice2(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].choice2;
		}
		return "";
	}
	public int GetBranchChoice2(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].branchChoice2;
		}
		return 0;
	}
	public string GetChoice3(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].choice3;
		}
		return "";
	}
	public int GetBranchChoice3(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].branchChoice3;
		}
		return 0;
	}
	public string GetIsTrust(int lineNumber){
		if(lineNumber < lines.Count){
			return lines[lineNumber].isTrust;
		}
		return "";
	}

	public void SetDialogueCounter(int dialogueCounter){
		this.dialogueCounter = dialogueCounter;
	}
	public int GetDialogueCounter(){
		return this.dialogueCounter;
	}
	public void SetBranch(int branch){
		this.branch = branch;
	}
	public int GetBranch(){
		return this.branch;
	}
	public int GetDialogueLine(){
		return this.dialogueLine;
	}
	public void SetIsStart(bool isStart){
		this.isStart = isStart;
	}
	public bool GetIsStart(){
		return this.isStart;
	}
	public void SetIsFreezed(bool isFreezed){
		this.isFreezed = isFreezed;
	}
	public bool GetIsFreezed(){
		return this.isFreezed;
	}
	public void SetIsFinish(bool isFinish){
		this.isFinish = isFinish;
	}
	public bool GetIsFinish(){
		return this.isFinish;
	}
	public void SetIsDialogue(bool isDialogue){
		this.isDialogue = isDialogue;
	}
	public bool GetIsDialogue(){
		return this.isDialogue;
	}
	public void SetIsClick(bool isClick){
		this.isClick = isClick;
	}
	public bool GetIsClick(){
		return this.isClick;
	}
}
