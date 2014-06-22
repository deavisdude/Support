using UnityEngine;
using System.Collections;

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

	public AudioClip gameMusic;

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
	private AudioSource mGameMusic;
	private AudioSource mRainMusic;
	private AudioSource mGateSource;
	private AudioSource mLaughAudioSource;
	
	private bool mIsFadingCreditsOut = false;
	private bool mIsFadingMenuOut = false;
	private bool mIsFadingGameMusicOut = false;

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
		if (mGameMusic.isPlaying) {
			if (mIsFadingGameMusicOut) {
				if (mGameMusic.volume > 0) {
					mGameMusic.volume -= 0.03f;
				}
				
				if (mGameMusic.volume < 0.05) {
					mGameMusic.Stop ();
				}
			} else if (mGameMusic.volume < 1) {
				mGameMusic.volume += 0.03f;
			}
		}
	}

	// =========================
	// Music Methods
	// =========================

	public void onHitFollower ()
	{
		if (!mGameMusic.isPlaying) {
			mGameMusic.Play ();
		}
	}

	public void Update ()
	{
		handleMenuMusic ();
		handleCredits ();
		handleGameMusic ();
	}

	public void playGameMusic ()
	{

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
		if (Obstacle.enemyIsBoy) {
			mLaughAudioSource.pitch = 1;
		} else {
			mLaughAudioSource.pitch = 1.75f;
		}

		mLaughAudioSource.Play ();
	}
	
	public void playGateOpenSound ()
	{
		if (!mGateSource.isPlaying) {
			mGateSource.Play ();
		}
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
		mBirdsAmbience.clip = birdsAndTheBees;
		mBirdsAmbience.loop = true;

		mCreditsMusic = gameObject.AddComponent<AudioSource> ();
		mCreditsMusic.clip = creditsClip;
		mCreditsMusic.loop = true;

		mGameMusic = gameObject.AddComponent<AudioSource> ();
		mGameMusic.loop = true;
		mGameMusic.clip = gameMusic;

		mGateSource = gameObject.AddComponent<AudioSource> ();
		mGateSource.clip = gateOpen;
		mGateSource.loop = false;

		mLaughAudioSource = gameObject.AddComponent<AudioSource> ();
		mLaughAudioSource.clip = evilLaugh;
		mLaughAudioSource.loop = false;

		mMenuMusic = gameObject.AddComponent<AudioSource> ();
		mMenuMusic.clip = menuMusic;
		mMenuMusic.loop = true;

		mRainMusic = gameObject.AddComponent<AudioSource> ();
		mRainMusic.clip = rainLoop;
		mRainMusic.loop = true;

		playMenuMusic ();
	}
}
