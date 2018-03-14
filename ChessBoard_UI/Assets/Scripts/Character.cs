using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	private Animator anim;
	private int health , power;
	public int getHP(){return health;}
	public void setHP(int damage){health -= damage;}
	public int Power{get{ return power; }}
	private float angleX , angleZ,speed;
	private bool moved,attacked,doneX, doneZ,doneRotationX,doneRotationZ,moving,confirmed,AI;
	public bool Moved{get{ return moved; }set { moved = value;}}
	public bool Attacked{get{ return attacked; } set{ attacked = value;}}
	public bool Confirmed{get{ return confirmed; } set{ confirmed = value;}}
	public bool Moving(){
		return moving;
	}
	private string currentFacing ,facingZ;
	private float moveDistance , attackDistance;
	public float MoveDistance{get{ return moveDistance; }}
	public float AttackDistance{get{ return attackDistance; }}
	private Vector3 position;
	public void SetPosition(Vector3 pos, bool nouse){
		position.x = pos.x;
		position.z = pos.z;
		//print (currentFacing);
	}
	public void SetPosition(Vector3 pos){
		if (position.x < pos.x) {
			//print (currentFacing);
			if (currentFacing == "up")angleX = 90f;
			else if (currentFacing == "down")angleX = -90f;
			else if (currentFacing == "left")angleX = 180f;
			else if (currentFacing == "right")angleX = 0f;
			currentFacing = "right";
			doneRotationX = false;
			doneX = false;
			//print (angleX);
		} else if (position.x > pos.x) {
			//print (currentFacing);
			if (currentFacing == "up")angleX = -90f;
			else if (currentFacing == "down")angleX = 90f;
			else if (currentFacing == "left")angleX = 0;
			else if (currentFacing == "right")angleX = 180f;
			currentFacing = "left";
			doneRotationX = false;
			doneX = false;
		} 
		position.x = pos.x;
		if (position.z < pos.z) {
			if (currentFacing == "up")
				angleZ = 0f;
			else if (currentFacing == "down")
				angleZ = 180f;
			else if (currentFacing == "left")
				angleZ = 90;
			else if (currentFacing == "right")
				angleZ = -90f;
			facingZ = "up";
			doneZ = false;
			doneRotationZ = false;
		} else if (position.z > pos.z) {
			if (currentFacing == "up")
				angleZ = 180f;
			else if (currentFacing == "down")
				angleZ = 0f;
			else if (currentFacing == "left")
				angleZ = -90;
			else if (currentFacing == "right")
				angleZ = 90f;
			facingZ = "down";
			doneZ = false;
			doneRotationZ = false;
		}
		position.z = pos.z;
		moved = true;
		moving = true;
		anim.SetBool ("isWalking", true);
		anim.SetBool ("isIdle", false);
		//print (doneRotationX + " " + doneRotationZ);
		//print (doneX + " " + doneZ);
		//print (GameManager.isMoving);
		//	print ("updated position" + position);
	}
    public Vector3 GetPosition(){return position;}
	public float GetPositionX(){return position.x;}
	public float GetPositionZ(){return position.z;}
	// Use this for initialization
	void Start () {
		moved = false;
		attacked = false;
		moveDistance = 3;
		attackDistance = 5;
		moving = false;
		speed = 3f;
		angleX = 0f;
		angleZ = 0f;
		currentFacing = "up";
		position = transform.position;
		doneX = true;
		doneZ = true;
		doneRotationX = true;
		doneRotationZ = true;
		health = 100;
		power = 10;
		anim = GetComponent<Animator> ();
		confirmed = false;
		if (gameObject.tag == "character")
			AI = false;
		else if (gameObject.tag == "enemy")
			AI = true;
		}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (gameObject);
		}
		//for testing only
		//print (gameObject + ":" + health);
		//if(!AI)
		//health -= 1;
		if (!doneRotationX) {
			if (angleX > 0) {
				transform.Rotate (0, speed, 0);
				angleX -= speed;
			} else if (angleX < 0) {
				transform.Rotate (0, -speed, 0);
				angleX += speed;
			}else if (angleX == 0) doneRotationX = true;
		}

		//Debug.Log (selected);
		if (transform.position.x != position.x && !doneX && doneRotationX) {
			//print (transform.position.x + "   " + position.x);
			if (currentFacing == "right") {
				transform.Translate (0, 0, speed * Time.deltaTime);
				GetComponentInParent<GameManager> ().camera.transform.Translate(speed*Time.deltaTime,0,0);
				if (transform.position.x > position.x) {
					Vector3 pos = new Vector3 (position.x, 0, transform.position.z);
					transform.position = pos;
				}
			} else if (currentFacing == "left") {
				transform.Translate (0, 0, speed * Time.deltaTime);
				GetComponentInParent<GameManager> ().camera.transform.Translate(-speed*Time.deltaTime,0,0);
				if (transform.position.x < position.x) {
					Vector3 pos = new Vector3 (position.x, 0, transform.position.z);
					transform.position = pos;
				}
			}
		} else if (transform.position.x == position.x) {
			doneX = true;
		}
		if (!doneRotationZ && doneX) {
			if (angleZ > 0) {
				transform.Rotate (0, speed, 0);
				angleZ -= speed;
			} else if (angleZ < 0) {
				transform.Rotate (0, -speed, 0);
				angleZ += speed;
			} else if (angleZ == 0) {
				doneRotationZ = true;
				currentFacing = facingZ;
				//print (currentFacing);
			}
		}

		if (transform.position.z != position.z && !doneZ && doneRotationZ) {
			//print (transform.position.z + "   " + position.z);
			if (currentFacing == "up") {
				transform.Translate (0, 0, speed * Time.deltaTime);
				GetComponentInParent<GameManager> ().camera.transform.Translate(0,speed*Time.deltaTime,0);
				if (transform.position.z > position.z) {
					Vector3 pos = new Vector3 (transform.position.x, 0, position.z);
					transform.position = pos;
				}
			} else if (currentFacing == "down") {
				transform.Translate (0, 0, speed * Time.deltaTime);
				GetComponentInParent<GameManager> ().camera.transform.Translate(0,-speed*Time.deltaTime,0);
				if (transform.position.z < position.z) {
					Vector3 pos = new Vector3 (transform.position.x, 0, position.z);
					transform.position = pos;
				}
			}
		} else if (transform.position.z == position.z) {
			doneZ = true;
		}
		if (doneX && doneZ) {
			moving = false;
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", true);
		}
		if (!AI&&!confirmed &&doneX && doneZ && !moving && !GameManager.choosingAttack && !GameManager.choosingMove&&((attacked &&!moved) ||(moved&&!attacked) || (attacked&& moved))) {
			//print (confirmed);
			GetComponentInParent<GameManager> ().ShowMenu();
			GetComponentInParent<GameManager> ().SetCameraPos(new Vector3(position.x,7,position.z));
		}
		//print (GameManager.isMoving);
	}

	public void Reset(){
		moved = false;
		attacked = false;
		confirmed = false;
	}
}
