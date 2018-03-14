using UnityEngine;

public class GameOverPanelController : MonoBehaviour {

	public Animator GameOverPanel;
	public Animator GameWinPanel;

	public void IsGameOver()
	{
		GameOverPanel.Play("GameOverPanelFadeIn");
	}
	
	public void IsGameWin()
	{
		GameWinPanel.Play("GameWinPanelFadeIn");
	}
}
