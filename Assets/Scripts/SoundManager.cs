using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource talking;
	public AudioSource registeredEvidence;
	public AudioSource characterMove;
	public AudioSource buttonClick;
	public AudioSource dialogueNext;
	public AudioSource notepadTurn;

	public void PlaySFXTalking(){
		talking.Play ();
	}
	public void PlaySFXRegisteredEvidence(){
		registeredEvidence.Play ();
	}
	public void PlaySFXCharacterMove(){
		characterMove.gameObject.SetActive (true);
		//characterMove.Play ();
	}
	public void UnplaySFXCharacterMove(){
		characterMove.gameObject.SetActive (false);
	}
	public void PlaySFXButtonClick(){
		buttonClick.Play ();
	}
	public void PlaySFXDialogueNext(){
		dialogueNext.Play ();
	}
	public void PlaySFXNotepadTurn(){
		notepadTurn.Play ();
	}
}
