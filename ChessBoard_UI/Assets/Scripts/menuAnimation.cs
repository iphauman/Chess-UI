using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuAnimation : MonoBehaviour {
	public Button moveButton,attackButton,skillButton,confirmButton;
	RectTransform t;
	float scaleX, scaleY;
	public float scaleSpeed = 0.05f;
	public float invokeRepeatingSpeed = 0.001f;
	void Start () {
		t = GetComponent<RectTransform>();	//just get the RectTransform object
		//get the original scale size
		scaleX = t.localScale.x;
		scaleY = t.localScale.y;
		//set the pivot to the circle point(in the menu ui) for scaling
		t.pivot = new Vector2(0.5f,0.5f);
	}

	void Update ()
	{
		//testing
		/*if (Input.GetKeyDown (KeyCode.A)) {
			InvokeRepeating ("scalerDown", invokeRepeatingSpeed, invokeRepeatingSpeed);
		}*/

		if (scaleX <=0.6f && scaleY <=0.6f &&GameManager.enableMenu ==true) {
			InvokeRepeating ("ScalerUp", invokeRepeatingSpeed, invokeRepeatingSpeed);
		}

		if (GameManager.enableMenu == true) {
			if (!GameManager.currentSelectedObj.GetComponent<Character> ().Attacked && !GameManager.currentSelectedObj.GetComponent<Character> ().Moved)
				Reset ();
			else if (!GameManager.currentSelectedObj.GetComponent<Character> ().Moved && !GameManager.moveFirst) {
				moveButton.gameObject.SetActive (true);
			}
			if (GameManager.currentSelectedObj.GetComponent<Character> ().Attacked && GameManager.currentSelectedObj.GetComponent<Character> ().Moved) {
				confirmButton.transform.localPosition = new Vector3 (0, -35, 0);
				attackButton.gameObject.SetActive (false);
				skillButton.gameObject.SetActive (false);
				moveButton.gameObject.SetActive (false);
			}else
			if (GameManager.currentSelectedObj.GetComponent<Character> ().Moved) {
				moveButton.gameObject.SetActive (false);
				confirmButton.transform.localPosition = new Vector3 (0, -95, 0);
				skillButton.transform.localPosition = new Vector3 (0, -65, 0);
				attackButton.transform.localPosition = new Vector3 (0, -35, 0);
			}else
			if (GameManager.currentSelectedObj.GetComponent<Character> ().Attacked) {
				confirmButton.transform.localPosition = new Vector3 (0, -65, 0);
				attackButton.gameObject.SetActive (false);
				skillButton.gameObject.SetActive (false);
			} 
		}
	}
		
	

	void ScalerDown(){
		//scale UI
		scaleX -= scaleSpeed;	scaleY -= scaleSpeed;
		t.localScale = new Vector3(scaleX, scaleY,1f);

		//check if the UI is minimum or not 
		if (scaleX <= 0 || scaleY <= 0)
			CancelInvoke ();
	}
	
	void ScalerUp(){
		//scale UI
		scaleX += scaleSpeed;	scaleY += scaleSpeed;
		t.localScale = (new Vector3(scaleX, scaleY,1f));
		//check if the UI is maximum or not 
		if (scaleX >= 0.6f || scaleY >=0.6f)
			CancelInvoke ();
	}
	public void Reset(){
		confirmButton.transform.localPosition = new Vector3(0,-125,0);
		skillButton.transform.localPosition = new Vector3(0,-95,0);
		attackButton.transform.localPosition = new Vector3(0,-65,0);
		attackButton.gameObject.SetActive (true);
		skillButton.gameObject.SetActive (true);
		moveButton.gameObject.SetActive (true);
	}
}