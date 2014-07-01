using UnityEngine;
using System.Collections;

public class PauseMenu : SPSUGameJamScript {

	private enum Buttons {menu, unpause, quit};
	private Buttons currentButton;

	public static bool paused = false;
	public GameObject background;
	public GameObject menuButtonSelector;
	public GameObject unpauseButtonSelector;
	public GameObject quitButtonSelector;

	private float selectionTime;
	private float buttonTimeOut = .25f;

	// Use this for initialization
	new void Start () {
		background.SetActive(false);
		base.Start();
		currentButton = Buttons.unpause;
	}

	void OnEnable ()
	{
		menuButtonSelector.SetActive(false);
		unpauseButtonSelector.SetActive(true);
		quitButtonSelector.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if(CreditsScript.creditsShowing)
				return;

			Pause(!paused);
		}

		if(paused)
		{
			float timeoutTimer = Time.realtimeSinceStartup - selectionTime;

			if (Input.GetAxisRaw ("Horizontal") > 0 && timeoutTimer >= buttonTimeOut) 
			{
				if((int)currentButton < System.Enum.GetValues(typeof(Buttons)).Length - 1)
				{
					currentButton++;
					SelectButton(currentButton);
				}
			} 
			else if (Input.GetAxisRaw ("Horizontal") < 0 && timeoutTimer >= buttonTimeOut) 
			{
				if((int)currentButton > 0)
				{
					currentButton--;
					SelectButton(currentButton);
				}
			}
			
			if (Input.GetButtonUp ("Jump") || Input.GetKey (KeyCode.Return)) {
				if(menuButtonSelector.activeInHierarchy)
					LoadMenu();
				else if(unpauseButtonSelector.activeInHierarchy)
					Pause (false);
				else if(quitButtonSelector.activeInHierarchy)
					Application.Quit();
			}
		}
	}

	private void SelectButton(Buttons button)
	{
		selectionTime = Time.realtimeSinceStartup;
		audioManager.playMenuSound();
		menuButtonSelector.SetActive(button == Buttons.menu);
		unpauseButtonSelector.SetActive(button == Buttons.unpause);
		quitButtonSelector.SetActive(button == Buttons.quit);
	}
	
	private void Pause(bool pause)
	{
		if(pause)
		{
			paused = true;
			Time.timeScale = 0;
			audioManager.Pause(true);
			background.SetActive(true);
		}
		else
		{
			paused = false;
			Time.timeScale = 1;
			audioManager.Pause(false);
			background.SetActive(false);
		}
	}
	
	private void LoadMenu()
	{
		Time.timeScale = 1;
		paused = false;
		Application.LoadLevel ("menu");
		audioManager.StopRainLoop();
		audioManager.StopGameMusic();
		audioManager.StopBirds();
		audioManager.FadeMenuMusicIn();
	}
}
