using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class SPSUAudioManager : MonoBehaviour
{
	// ==================================================
	// Variables
	// ==================================================

	public AudioClip birdsAndTheBees;
	public AudioClip bodyHit;

	public AudioClip creditsClip;

	public AudioClip doorOpen;
	public AudioClip evilLaugh;

	public AudioClip gameMusicLoop1;
	public AudioClip gameMusicLoop2;
	public AudioClip gameMusicLoop3;
	public AudioClip gameMusicLoop4;
	public AudioClip gameMusicLoop5;
	public AudioClip gameMusicLoop1Intro;

	public AudioClip gateOpen;

	public AudioClip jumpNoise;

	public AudioClip levelUp;

	public AudioClip menuMusic;
	public AudioClip menuSound;

	public AudioClip newFriendSound;

	public AudioClip pressurePlateActivated;
	public AudioClip pressurePlateDeactivated;

	public AudioClip rainLoop;

	public AudioClip sighNoise;

	private AudioSource mBirdsAmbience;
	private AudioSource mCreditsMusic;
	private AudioSource mMenuMusic;
	private AudioSource mMusicLoop;
	private AudioSource mRainMusic;
	private AudioSource mLaughAudioSource;

	private bool mGameMusicHasBeenStarted = false;
	private bool mIsFadingCreditsOut = false;
	private bool mIsFadingMenuOut = false;
	private bool mIsFadingGameMusicOut = false;

	private int mCurrentTrackIndex = 0;

	// ==================================================
	// Methods
	// ==================================================

	private void handleCredits ()
	{
		if (mCreditsMusic.isPlaying) {
			if (mIsFadingCreditsOut) {
				if (mCreditsMusic.volume > 0) {
					mCreditsMusic.volume -= 0.03f;
				}
				
				if (mCreditsMusic.volume < 0.05) {
					mCreditsMusic.Stop ();
				}
			} else if (mCreditsMusic.volume < 1) {
				mCreditsMusic.volume += 0.01f;
			}
		}
	}

	private void handleMenuMusic ()
	{
		if (mMenuMusic.isPlaying) {
			if (mIsFadingMenuOut) {
				if (mMenuMusic.volume > 0) {
					mMenuMusic.volume -= 0.03f;
				}
				
				if (mMenuMusic.volume < 0.05) {
					mMenuMusic.Stop ();
				}
			} else if (mMenuMusic.volume < 1) {
				mMenuMusic.volume += 0.03f;
			}
		}
	}

	private void handleGameMusic ()
	{
		if (mMusicLoop.isPlaying) {
			if (mIsFadingGameMusicOut) {
				if (mMusicLoop.volume > 0) {
					mMusicLoop.volume -= 0.03f;
				}
				
				if (mMusicLoop.volume < 0.05) {
					mMusicLoop.Stop ();
				}
			} else if (mMusicLoop.volume < 1) {
				mMusicLoop.volume += 0.03f;
			}
		}
		else
		{
			if(mMusicLoop.clip == gameMusicLoop1Intro)
			{
				Debug.Log("play loop 1");
				mMusicLoop.clip = gameMusicLoop1;
				mMusicLoop.loop = true;
				mMusicLoop.Play();
			}
		}
	}

	// =========================
	// Music Methods
	// =========================

	public void Update ()
	{
		handleMenuMusic ();
		handleCredits ();
		handleGameMusic ();
	}

	public void play ()
	{
		AudioClip newClip = null;

		switch (mCurrentTrackIndex) {
		case 1:
			newClip = gameMusicLoop1Intro;
			mRainMusic.volume = 1;
			mMusicLoop.loop = false;
			mMusicLoop.Play();
			break;

		case 2:
			newClip = gameMusicLoop2;
			mRainMusic.volume = .8f;
			break;

		case 3:
			newClip = gameMusicLoop3;
			mRainMusic.volume = .6f;
			break;

		case 4:
			newClip = gameMusicLoop4;
			mRainMusic.volume = .4f;
			break;

		case 5:
			newClip = gameMusicLoop5;
			mRainMusic.volume = .2f;
			break;
		}

		if (mMusicLoop.clip != newClip) {
			mMusicLoop.clip = newClip;

			if(newClip != gameMusicLoop1Intro)
			{
				Invoke ("play", mMusicLoop.clip.length);	
			}
		}
	}

	public void incrementTrackIndex ()
	{
		HOTween.To(mMenuMusic, 1, "volume", 0);
		mCurrentTrackIndex = Application.loadedLevel;

		if (mCurrentTrackIndex > 5) {
			mCurrentTrackIndex = 5;
		}

		play ();
	}

	public void playRainLoop ()
	{
		if (!mRainMusic.isPlaying) {
			mRainMusic.Play ();
		}
	}

	public void playCreditsMusic ()
	{
		if (!mCreditsMusic.isPlaying) {
			mCreditsMusic.Play ();
			mIsFadingCreditsOut = false;
			mCreditsMusic.volume = 0;
			mIsFadingGameMusicOut = true;
		}
	}

	public void playMenuMusic ()
	{
		if (!mMenuMusic.isPlaying) {
			mMenuMusic.Play ();
			mIsFadingMenuOut = false;
			mMenuMusic.volume = 0;
		}
	}

	public void fadeCreditsMusicOut ()
	{
		mIsFadingCreditsOut = true;
	}

	public void fadeMenuMusicOut ()
	{
		mIsFadingMenuOut = true;
	}

	public void startGameMusic ()
	{
		fadeMenuMusicOut ();
		playRainLoop ();
	}

	// =========================
	// Sound Effect Methods
	// =========================

	public void playBirdsAndBeesSound ()
	{
		mBirdsAmbience.Play ();
	}
	
	public void playBodyHitSound ()
	{
		AudioSource.PlayClipAtPoint (bodyHit, transform.position);
	}
	
	public void playDoorOpenSound ()
	{
		AudioSource.PlayClipAtPoint (doorOpen, transform.position);
	}
	
	public void playEvilLaughSound ()
	{
		if(Obstacle.enemyIsBoy)
			mLaughAudioSource.pitch = 1;
		else
			mLaughAudioSource.pitch = 1.75f;

		mLaughAudioSource.Play();
	}
	
	public void playGateOpenSound ()
	{
		AudioSource.PlayClipAtPoint (gateOpen, transform.position);
	}
	
	public void playJumpSound ()
	{
		AudioSource.PlayClipAtPoint (jumpNoise, transform.position, .5f);
	}
	
	public void playMenuSound ()
	{
		AudioSource.PlayClipAtPoint (menuSound, transform.position);
	}
	
	public void playNewFriendSound ()
	{
		AudioSource.PlayClipAtPoint (newFriendSound, transform.position);
	}
	
	public void playPressurePlateActivatedSound ()
	{
		AudioSource.PlayClipAtPoint (pressurePlateActivated, transform.position);
	}
	
	public void playPressurePlateDeactivedSound ()
	{
		AudioSource.PlayClipAtPoint (pressurePlateDeactivated, transform.position);
	}
	
	public void playSigh ()
	{
		AudioSource.PlayClipAtPoint (sighNoise, transform.position);
	}

	// =========================
	// Lifecycle Methods
	// =========================

	protected void Start ()
	{
		GameObject.DontDestroyOnLoad (gameObject);
		mBirdsAmbience = gameObject.AddComponent<AudioSource> ();
		mCreditsMusic = gameObject.AddComponent<AudioSource> ();
		mMusicLoop = gameObject.AddComponent<AudioSource> ();
		mRainMusic = gameObject.AddComponent<AudioSource> ();
		mMenuMusic = gameObject.AddComponent<AudioSource> ();
		mLaughAudioSource = gameObject.AddComponent<AudioSource>();
		mLaughAudioSource.clip = evilLaugh;
		mBirdsAmbience.clip = birdsAndTheBees;
		mCreditsMusic.clip = creditsClip;
		mRainMusic.clip = rainLoop;
		mMenuMusic.clip = menuMusic;
		mBirdsAmbience.loop = true;
		mCreditsMusic.loop = true;
		mMusicLoop.loop = true;
		mRainMusic.loop = true;
		mMenuMusic.loop = true;
		mLaughAudioSource.loop = false;
		if(Application.loadedLevelName == "menu")
			playMenuMusic ();
	}
}
