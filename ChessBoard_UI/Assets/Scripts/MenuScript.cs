using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
	private GameObject moveHighLight;
	private GameObject attackHighLight;

	void Start(){
		moveHighLight = Resources.Load<GameObject>("MoveHighLight");
		attackHighLight = Resources.Load<GameObject>("AttackHighLight");
	}
	public void ShowMoveHighLight(){
		GameManager.enableMenu = false;
		GameManager.choosingMove = true;
		float moveDistance = GameManager.currentSelectedObj.GetComponent<Character> ().MoveDistance;
		CreateHighLight (moveDistance, moveHighLight);
	}

	public void ShowAttackHighLight(){
		GameManager.enableMenu = false;
		GameManager.choosingAttack = true;
		float attackDistance = GameManager.currentSelectedObj.GetComponent<Character> ().AttackDistance;
		//print (attackDistance);
		CreateHighLight (attackDistance, attackHighLight);
	}

	public void CreateHighLight(float distance, GameObject typeOfHightLight){
		int row = 0;
		GameObject g;
		for (int i = 1; i <= distance; i++) {
			if(GameManager.currentSelectedObj.transform.position.x + i<7){
				g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x + i, 0.01f, GameManager.currentSelectedObj.transform.position.z), Quaternion.identity);
				g.transform.parent = this.transform.parent;
			}
			if(GameManager.currentSelectedObj.transform.position.x - i>-3){
				g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x - i, 0.01f, GameManager.currentSelectedObj.transform.position.z), Quaternion.identity);
				g.transform.parent = this.transform.parent;
			}
			if(GameManager.currentSelectedObj.transform.position.z + i<5){
				g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x, 0.01f, GameManager.currentSelectedObj.transform.position.z + i), Quaternion.identity);
				g.transform.parent = this.transform.parent;
			}
			if(GameManager.currentSelectedObj.transform.position.z - i>-5){
				g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x, 0.01f, GameManager.currentSelectedObj.transform.position.z - i), Quaternion.identity);
				g.transform.parent = this.transform.parent;
			}
		}
		distance--;
		row++;
		while (distance != 0) {
			for (int i =(int)distance; i > 0; i--) {
				if(GameManager.currentSelectedObj.transform.position.x - i>-3 && GameManager.currentSelectedObj.transform.position.z +row<5){
					g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x - i, 0.01f, GameManager.currentSelectedObj.transform.position.z + row), Quaternion.identity);
					g.transform.parent = this.transform.parent;
				}
				if(GameManager.currentSelectedObj.transform.position.x + i<7 && GameManager.currentSelectedObj.transform.position.z +row<5){
					g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x + i, 0.01f, GameManager.currentSelectedObj.transform.position.z + row), Quaternion.identity);
					g.transform.parent = this.transform.parent;
				}
				if(GameManager.currentSelectedObj.transform.position.x - i>-3 && GameManager.currentSelectedObj.transform.position.z -row>-5){
					g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x - i, 0.01f, GameManager.currentSelectedObj.transform.position.z - row), Quaternion.identity);
					g.transform.parent = this.transform.parent;
				}
				if(GameManager.currentSelectedObj.transform.position.x + i<7 && GameManager.currentSelectedObj.transform.position.z -row>-5){
					g = Instantiate (typeOfHightLight, new Vector3 (GameManager.currentSelectedObj.transform.position.x + i, 0.01f, GameManager.currentSelectedObj.transform.position.z - row), Quaternion.identity);
					g.transform.parent = this.transform.parent;
				}
			}
			row++;
			distance--;
		}
	}
}
