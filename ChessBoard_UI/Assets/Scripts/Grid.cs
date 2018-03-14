using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	private const float tile_offest = 0.5f;
	public static float TILE_OFFEST{get {return tile_offest;}}
	public static int tile_value = 10;

	//Draw Board in game
	private void OnDrawGizmos(){
		for (int i = 0; i < tile_value+1; i++) {
			Vector3 start = new Vector3(-3,0,-5+i);
			Vector3 end = new Vector3 (7, 0, -5+i);
			Gizmos.DrawLine (start,end);
			start = new Vector3(-3+i,0,-5);
			end = new Vector3 (-3+i, 0, 5);
			Gizmos.DrawLine (start,end);
		}
	}
}
