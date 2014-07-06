using UnityEngine;
using System.Collections;

public class PauseMenu : SPSUGameJamScript
{

		private enum Buttons
		{
				menu,
				unpause,
				quit
	}
		;

		private Buttons currentButton;

		public static bool paused = false;
		public GameObject background;

		public GameObject menuButton;
		public GameObject unpauseButton;
		public GameObject quitButton;

		public GameObject menuButtonSelector;
		public GameObject unpauseButtonSelector;
		public GameObject quitButtonSelector;

		private float selectionTime;
		private float buttonTimeOut = .25f;
	
		new void Start ()
		{
				background.SetActive (false);
				base.Start ();
				currentButton = Buttons.unpause;
		}

		void OnEnable ()
		{
				menuButtonSelector.SetActive (false);
				unpauseButtonSelector.SetActive (true);
				quitButtonSelector.SetActive (false);
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						if (CreditsScript.creditsShowing)
								return;

						Pause (!paused);
				}

				if (paused) {
						float timeoutTimer = Time.realtimeSinceStartup - selectionTime;

						if (Input.GetAxisRaw ("Horizontal") > 0 && timeoutTimer >= buttonTimeOut) {
								if ((int)currentButton < System.Enum.GetValues (typeof(Buttons)).Length - 1) {
										currentButton++;
										SelectButton (currentButton);
								}
						} else if (Input.GetAxisRaw ("Horizontal") < 0 && timeoutTimer >= buttonTimeOut) {
								if ((int)currentButton > 0) {
										currentButton--;
										SelectButton (currentButton);
								}
						}
			
						if (Input.GetButtonUp ("Jump") || Input.GetKey (KeyCode.Return)) {
								if (menuButtonSelector.activeInHierarchy)
										LoadMenu ();
								else if (unpauseButtonSelector.activeInHierarchy)
										Pause (false);
								else if (quitButtonSelector.activeInHierarchy)
								{
#if UNITY_WEBPLAYER
									Application.ExternalEval("window.parent.location.href='http://support-game.com'");
#else
									Application.Quit ();
#endif
								}
						}
				}
		}

		private void SelectButton (Buttons button)
		{
				selectionTime = Time.realtimeSinceStartup;
				audioManager.playMenuSound ();
				setButtonActive (menuButtonSelector, menuButton, button == Buttons.menu);
				setButtonActive (unpauseButtonSelector, unpauseButton, button == Buttons.unpause);
				setButtonActive (quitButtonSelector, quitButton, button == Buttons.quit);
		}

		void resetPauseMenu ()
		{
				setButtonActive (menuButtonSelector, menuButton, false);
				setButtonActive (unpauseButtonSelector, unpauseButton, true);
				setButtonActive (quitButtonSelector, quitButton, false);
				currentButton = Buttons.unpause;
		}

		void setButtonActive (GameObject selector, GameObject button, bool active)
		{
				selector.SetActive (active);
				button.renderer.material.color = new Color (button.renderer.material.color.r,
		                                            button.renderer.material.color.r,
		                                            button.renderer.material.color.r,
		                                                   !active ? 1f : 0f);
		}
	
		private void Pause (bool pause)
		{
				if (pause) {
						paused = true;
						Time.timeScale = 0;
						audioManager.Pause (true);
						background.SetActive (true);
						resetPauseMenu ();
				} else {
						paused = false;
						Time.timeScale = 1;
						audioManager.Pause (false);
						background.SetActive (false);
				}
		}
	
		private void LoadMenu ()
		{
				Time.timeScale = 1;
				paused = false;
				Application.LoadLevel ("menu");
				audioManager.StopRainLoop ();
				audioManager.StopGameMusic ();
				audioManager.StopBirds ();
				audioManager.FadeMenuMusicIn ();
		}
}
