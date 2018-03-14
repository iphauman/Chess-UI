using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemMenuController : MonoBehaviour
{
//	private Animator _panelFadeIn;
	public GameObject SystemButton;
	public Animator MenuAnimator;

	private ColorBlock _tempOriginalButtonColor;
	private GameObject _tempSelectedButton;
	private GameObject _tempSelectedPanel;
	public bool IsEnteredMenu { get; set; }

	
	private void Start () {
		_tempOriginalButtonColor = SystemButton.GetComponent<Button>().colors;
		GameObject.FindWithTag("Description").GetComponent<Text>().text = "";
		IsEnteredMenu = false;
		_tempSelectedButton = null;
		_tempSelectedPanel = null;
	}
	
	void Update () {
		InputUpdate();
		//Debug.Log(EventSystem.current.currentSelectedGameObject);
	}

	private void InputUpdate()
	{	
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!IsEnteredMenu)
			{
				MenuAnimator.Play("enterMenu");
			}
			else if (IsEnteredMenu)
			{
				MenuAnimator.Play("leaveMenu");
			}
		}

		
		if (Input.GetKeyDown(KeyCode.Z))
		{
			MenuAnimator.Play("rightEnterMenu");
		}
		
		if (Input.GetKeyDown(KeyCode.X))
		{
			MenuAnimator.Play("rightLeaveMenu");
		}
	}
	
//	public void ShowSystemMenu()
//	{
//		SystemPanel.SetActive(true);
//		
////		//change button color
////		ChangeColor();
////		ClearPanel();
//	}
	
//	public void ClearPanel()
//	{
//		SystemPanel.SetActive(false);
//		ResetColor();
//		
//	}
	
	public void ChangeColor()
	{
		GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
		Debug.Log("ChangeColor: " + selectedButton);
		if (selectedButton == null) 
			return;
		if(_tempSelectedButton != selectedButton)
			if(_tempSelectedButton != null)
				ResetColor(_tempSelectedButton);
		ColorBlock cb = _tempOriginalButtonColor;
		cb.normalColor = Color.gray;
		cb.highlightedColor = Color.gray;
		selectedButton.GetComponent<Button>().colors = cb;
		selectedButton.GetComponentInChildren<Text>().color = Color.white;
		_tempSelectedButton = selectedButton;
	}

	private void ResetColor(GameObject button)
	{
//		GameObject gameObject = EventSystem.current.currentSelectedGameObject;
		Debug.Log("ResetColor: " + button);
		button.GetComponent<Button>().colors = _tempOriginalButtonColor;
		button.GetComponentInChildren<Text>().color = Color.black;
	}

	public void SetActive(GameObject gameObject)
	{
		Debug.Log("SetActive: " + gameObject + ", Temp: " + _tempSelectedPanel);
		if (gameObject == _tempSelectedPanel)
			return;
		
		if(_tempSelectedPanel != null)
			_tempSelectedPanel.SetActive(false);

		gameObject.SetActive(true);
		_tempSelectedPanel = gameObject;
	}
	
}
