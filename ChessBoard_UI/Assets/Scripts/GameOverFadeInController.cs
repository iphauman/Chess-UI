using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFadeInController : MonoBehaviour
{
	public Animator GameoverAnmiAnimator;
	// Use this for initialization
	void Start ()
	{
		GameoverAnmiAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IsGameOver()
	{
		GameoverAnmiAnimator.Play("gameOverFadeIn");
	}
}
