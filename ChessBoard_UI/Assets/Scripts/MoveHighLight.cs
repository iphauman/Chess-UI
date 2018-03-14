using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHighLight : MonoBehaviour {	
	void Start(){
		foreach (Transform t in this.transform.parent) {
			if (t.gameObject.tag != "map") {
				if (t.transform.position.x == transform.position.x && t.transform.position.z == transform.position.z) {
					Destroy (this.gameObject);
				}
			}
		}
        //print (this.transform.position.x + "  " + this.transform.position.z);
    }
}
