using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescriptionTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public String Description;
	private GameObject _gameObject;

	private void Start()
	{
		_gameObject = GameObject.FindWithTag("Description");
		_gameObject.GetComponent<Text>().text = "";
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
//		Debug.Log("Over!");
		_gameObject.SetActive(true);
		_gameObject.GetComponent<Text>().text = Description;
	}
	
	
	public void OnPointerExit(PointerEventData eventData)
	{
//		Debug.Log("Exit!");
		_gameObject.SetActive(false);
		_gameObject.GetComponent<Text>().text = "";
	}
}