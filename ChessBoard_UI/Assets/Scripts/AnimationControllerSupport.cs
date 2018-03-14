using UnityEngine;

public class AnimationControllerSupport : MonoBehaviour
{
	private GameObject _thePlayer;
	private SystemMenuController _playerScript;
	
	private void Start()
	{
		_thePlayer = GameObject.Find("SystemMenuController");
		_playerScript = _thePlayer.GetComponent<SystemMenuController>();
	}

	private void Event_EnterMenu()
	{
		_playerScript.IsEnteredMenu = true;
	}

	private void Event_LeaveMenu()
	{
		_playerScript.IsEnteredMenu = false;
	}
}
