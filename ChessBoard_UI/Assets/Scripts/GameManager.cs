using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
	//for Inital Game only
	public Text winText,loseText;
	public List<GameObject> character = new List<GameObject>();
	public GameObject menuManager ,menu, camera,btnTurnEnd;
	public void SetCameraPos(Vector3 pos){
		camera.transform.position = new Vector3(pos.x , pos.y , pos.z);
	}
	Vector3[] pos;
	//for saving what you are selecting
	public static GameObject currentSelectedObj;
	private Vector3 currentPosition;
	//store hightlight
	//public GameObject moveHighLight,attackHighLight;
	//check any gameobject is moving
	public static bool isMoving , enableMenu , choosingAttack , choosingMove, moveFirst, endTurn, winGame ,loseGame;


	// Use this for initialization
	void Start () {
		InitGame ();
	}
	
	// Update is called once per frame
	void Update () {
		//set cursor
		menu.SetActive(enableMenu);
		if (currentSelectedObj != null || endTurn||GameObject.Find("SystemMenuController").GetComponent<SystemMenuController>().IsEnteredMenu) {
			btnTurnEnd.SetActive (false);
		}else btnTurnEnd.SetActive(true);
		if (isMoving ||endTurn)
			Cursor.visible = false;
		else
			Cursor.visible = true;
		
		if (endTurn) {
			//DoAI ();
		}
		//calculate any gameObject ismoving
		foreach (Transform t in this.transform) {
			if (t.gameObject.tag =="character" || t.gameObject.tag =="enemy") {
				if (t.gameObject.GetComponent<Character> ().Moving ()) {
					isMoving = true;
					break;
				} else
					isMoving = false;
			}
		}

		foreach (Transform t in transform)
		{
			if (t.gameObject.tag == "enemy")
			{
				winGame = false;
				break;
			}
			else winGame = true;
		}
		foreach (Transform t in transform)
		{
			if (t.gameObject.tag == "character")
			{
				loseGame = false;
				break;
			}
			else loseGame = true;
		}

		if (choosingAttack || choosingMove)
		{
			SetCameraPos (new Vector3(currentSelectedObj.transform.position.x, 9, currentSelectedObj.transform.position.z));
		}

		if (winGame)
		{
			winText.gameObject.SetActive(true);
		}else if (loseGame)
		{
			loseText.gameObject.SetActive(true);
		}

		//calculate all alley gameObject moved and get alley gameObject values
		//if mouse right click , cancel current move
		if (!isMoving &&!endTurn&&!winGame&&!loseGame) {
			if (Input.GetMouseButtonDown (1)&&currentSelectedObj!=null) {
				CancelMove ();
			}
			//if mouse left click , do function
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hitInfo;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hitInfo)) {
					// Do what u want
					//print(hitInfo.point.x+"  " + hitInfo.point.y);
					if (!IsPressingUI () && enableMenu == false) {
						if (currentSelectedObj == null && hitInfo.collider.gameObject.tag == "character" && !hitInfo.collider.gameObject.GetComponent<Character> ().Confirmed) {
							currentSelectedObj = hitInfo.collider.gameObject;
							currentPosition = currentSelectedObj.transform.position;
							SetCameraPos (new Vector3(currentSelectedObj.transform.position.x, 7, currentSelectedObj.transform.position.z));
							ShowMenu ();
							//print ("hit position" + currentPosition + "  " + "hit object" + currentSelectedObj);
						} else if (currentSelectedObj != null) {
							if (hitInfo.collider.gameObject.tag == "map") {
								float x = hitInfo.collider.gameObject.transform.position.x;
								float z = hitInfo.collider.gameObject.transform.position.z;
								//print (x + "  " + z);
								currentSelectedObj.GetComponent<Character> ().SetPosition (new Vector3 (x, 0, z));
								isMoving = true;
								choosingMove = false;
								foreach (Transform t in transform) {
									if (t.gameObject.tag == "map")
										Destroy (t.gameObject);
								}
							} else if (hitInfo.collider.gameObject.tag == "enemy"
							          && FindDistance (currentSelectedObj.transform.position, hitInfo.collider.transform.position) <= currentSelectedObj.GetComponent<Character> ().AttackDistance) {
								if (currentSelectedObj.GetComponent<Character> ().Moved)
									moveFirst = true;
								currentSelectedObj.GetComponent<Character> ().Attacked = true;
								hitInfo.collider.gameObject.GetComponent<Character> ().setHP (currentSelectedObj.GetComponent<Character> ().Power);
								print (hitInfo.collider.gameObject.GetComponent<Character> ().getHP ());
								choosingAttack = false;
								ShowMenu ();
								foreach (Transform t in transform) {
									if (t.gameObject.tag == "EditorOnly")
										Destroy (t.gameObject);
								}
							}
						}
						//print (currentSelectedObj);
					}
				}
			}
		}
	}

	public void InitGame()
	{
		winGame = false;
		loseGame = false;
		currentPosition = Vector3.zero;
		endTurn = false;
		enableMenu = false;
		isMoving = false;
		choosingMove = false;
		choosingAttack = false;
		int i = 0;
		pos = new Vector3[character.Count];
		foreach (GameObject obj in character) {
			GameObject g;
			pos[i] = new Vector3 (Mathf.Round (Random.Range (-3, 7)) + Grid.TILE_OFFEST, 0, Mathf.Round (Random.Range (-5, 5)) + Grid.TILE_OFFEST);
			g = Instantiate(obj,pos[i],Quaternion.identity);
			g.transform.parent = this.transform;
			i++;
		}
		i = 0;
		foreach (Transform t in transform) {
			if (t.tag == "character" || t.tag == "enemy") {
				t.gameObject.GetComponent<Character> ().SetPosition (pos [i], true);
				i++;
			}
		}
	}

	public void CancelMove(){
		if (choosingMove || choosingAttack) {
			foreach (Transform t in transform) {
				if (t.gameObject.tag == "map" || t.gameObject.tag == "EditorOnly")
					Destroy (t.gameObject);
			}
			ShowMenu ();
			choosingMove = false;
			choosingAttack = false;
			SetCameraPos (new Vector3( currentSelectedObj.transform.position.x,7,currentSelectedObj.transform.position.z));
		} else if (currentSelectedObj.GetComponent<Character> ().Moved && enableMenu == true && !moveFirst) {
			currentSelectedObj.transform.position = currentPosition;
			currentSelectedObj.GetComponent<Character> ().Moved = false;
			currentSelectedObj.GetComponent<Character> ().SetPosition (currentPosition,true);
			ShowMenu ();
			SetCameraPos (new Vector3( currentSelectedObj.transform.position.x,7,currentSelectedObj.transform.position.z));
		} else if (enableMenu == true &&!currentSelectedObj.GetComponent<Character> ().Moved && !currentSelectedObj.GetComponent<Character> ().Attacked) {
			//print (currentSelectedObj.GetComponent<Character> ().Attacked);
			currentSelectedObj = null;
			currentPosition = Vector3.zero;
			enableMenu = false;	
			SetCameraPos(new Vector3(2, 9, 0));
		}
	}

	public void Reset(){
		foreach (Transform t in transform) {
			if (t.gameObject.tag == "map" || t.gameObject.tag == "EditorOnly")
				Destroy (t.gameObject);
			/*else if (t.gameObject.tag == "enemy" || t.gameObject.tag == "character")
				t.GetComponent<Character> ().reset ();*/ // for testing only
		}
		currentSelectedObj.GetComponent<Character> ().Confirmed = true;
		currentSelectedObj = null;
		currentPosition = Vector3.zero;
		enableMenu = false;
		SetCameraPos(new Vector3 (2, 9, 0));
		moveFirst = false;
	}

	public float FindDistance(Vector3 posC , Vector3 posE){
		float distance = Mathf.Abs(posC.x - posE.x) + Mathf.Abs(posC.z - posE.z);
		//print (distance);
		return distance;
	}

	public void ShowMenu (){
		enableMenu = true;
		menu.transform.parent.position = new Vector3 (currentSelectedObj.transform.position.x +1.5f, 2, currentSelectedObj.transform.position.z-0.8f);
	}
		
	public bool IsPressingUI(){
		PointerEventData eventPosition = new PointerEventData (EventSystem.current);
		eventPosition.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (eventPosition , results);
		return results.Count > 0;
	}

	public void EndTurn(){
		endTurn = true;
		foreach (Transform t in transform) {
			if (t.gameObject.tag == "character")
				t.GetComponent<Character> ().Reset ();
			else if (t.gameObject.tag == "map" || t.gameObject.tag == "EditorOnly")
				Destroy (t.gameObject);
		}
		currentSelectedObj = null;
		currentPosition = Vector3.zero;
		enableMenu = false;
		SetCameraPos(new Vector3 (2, 9, 0));
		moveFirst = false;
	}

	public void DoAI(){
			List<GameObject> spawnedCharacter = new List<GameObject> ();
			GameObject chasedCharacter;
			float closestPath = 10;
			foreach (Transform t in transform) {
				if (t.gameObject.tag == "character") {
					spawnedCharacter.Add (t.gameObject);	
				}
			}
			print (spawnedCharacter.Count);

		if (!isMoving) {
			// doMove doAttack doConfirm
			foreach (Transform t in transform) {
				if (t.gameObject.tag == "enemy" && !t.GetComponent<Character>().Confirmed) {
					float enemyMove = t.GetComponent<Character> ().MoveDistance;
					float enemyAttack = t.GetComponent<Character> ().AttackDistance;
					currentSelectedObj = t.gameObject;
					SetCameraPos (new Vector3(currentSelectedObj.transform.position.x, 7, currentSelectedObj.transform.position.z));
					currentSelectedObj.GetComponent<Character> ().SetPosition (new Vector3(currentSelectedObj.transform.position.x+enemyMove,0,currentSelectedObj.transform.position.z));
					//if(!currentSelectedObj.GetComponent<Character>().Moved)
					//menu.GetComponent<menuAnimation> ().moveButton.onClick.Invoke ();
					

					/*for (int i = 0; i < spawnedCharacter.Count; i++) {
						float distanceBetween = FindDistance (spawnedCharacter [i].transform.position, t.transform.position);
						if (distanceBetween < closestPath) {
							closestPath = distanceBetween;
							chasedCharacter = spawnedCharacter[i];
							print (closestPath);
							print (chasedCharacter);
						}
					}*/

				}
			}


		}
		foreach (Transform t in transform) {
			if (transform.gameObject.tag == "map") {
				Destroy (t.gameObject);
			}
		}
		endTurn = false;
	}
}