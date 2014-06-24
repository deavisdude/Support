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
	private AudioSource mLevelUpAudioSource;

	// ==================================================
	// Methods
	// ==================================================

	// =========================
	// Music Methods
	// =========================

	public void onHitFollower ()
	{
		if (!mGameMusic.isPlaying) {
			mGameMusic.Play ();
		}
	}

	public void playRainLoop ()
	{
		if (!mRainMusic.isPlaying) {
			mRainMusic.Play ();
		}
	}

	public void StopRainLoop()
	{
		mRainMusic.Stop();
	}

	public void StopGameMusic()
	{
		mGameMusic.Stop();
	}

	public void StopBirds()
	{
		mBirdsAmbience.Stop();
	}

	public void playCreditsMusic ()
	{
		if (!mCreditsMusic.isPlaying) {
			mCreditsMusic.Play ();
			FadeGameMusicOut();
			mBirdsAmbience.Stop();
			StopRainLoop();
		}
	}

	public void FadeGameMusicIn()
	{
		if(!mGameMusic.isPlaying)
			mGameMusic.Play();
		HOTween.Kill(mGameMusic);
		HOTween.To(mGameMusic, 1, "volume", 1);
	}

	public void FadeGameMusicOut()
	{
		HOTween.Kill(mGameMusic);
		HOTween.To(mGameMusic, 1, "volume", 0);
	}

	public void FadeMenuMusicIn()
	{
		if(!mMenuMusic.isPlaying)
			mMenuMusic.Play();
		HOTween.Kill(mMenuMusic);
		HOTween.To(mMenuMusic, 1, "volume", 1);
	}
	
	public void FadeMenuMusicOut()
	{
		HOTween.To(mMenuMusic, 1, "volume", 0);
	}

	public void FadeCreditsMusicIn()
	{
		if(!mCreditsMusic.isPlaying)
			mCreditsMusic.Play();
		HOTween.Kill(mCreditsMusic);
		HOTween.To(mCreditsMusic, 1, "volume", 1);
	}
	
	public void FadeCreditsMusicOut()
	{
		HOTween.Kill(mCreditsMusic);
		HOTween.To(mCreditsMusic, 1, "volume", 0);
	}
	
	public void playMenuMusic ()
	{ 
		if(mMenuMusic.isPlaying)
			mMenuMusic.Stop();

		mMenuMusic.volume = 1;
		mMenuMusic.Play ();
	}

	public void startGameMusic ()
	{
		FadeMenuMusicOut();
		playRainLoop ();
	}

	// =========================
	// Sound Effect Methods
	// =========================

	public void playBirdsAndBeesSound ()
	{
		StopRainLoop();
		mBirdsAmbience.Play ();
	}
	
	public void playBodyHitSound (float volume = 1)
	{
		AudioSource.PlayClipAtPoint (bodyHit, transform.position, volume);
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
	
	public void playJumpSound (float volume = 1)
	{
		AudioSource.PlayClipAtPoint (jumpNoise, transform.position, .5f * volume);
	}
	
	public void playMenuSound ()
	{
		AudioSource.PlayClipAtPoint (menuSound, transform.position);
	}

	public void playLevelUpSound()
	{
		mLevelUpAudioSource.Play();
	}
	
	public void playNewFriendSound ()
	{
		AudioSource.PlayClipAtPoint (newFriendSound, transform.position);
	}
	
	public void playPressurePlateActivatedSound (float volume = 1)
	{
		AudioSource.PlayClipAtPoint (pressurePlateActivated, transform.position, 0.5f * volume);
	}
	
	public void playPressurePlateDeactivedSound (float volume = 1)
	{
		AudioSource.PlayClipAtPoint (pressurePlateDeactivated, transform.position, 0.5f * volume);
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
		GameObject audioManager = GameObject.Find("Audio Manager");
		if(audioManager != null && audioManager != gameObject)
		{
			Debug.Log("Found a second audio manager");
			audioManager.GetComponent<SPSUAudioManager>().playMenuMusic();
			Destroy(gameObject); // commit suicide
		}

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

		mLevelUpAudioSource = gameObject.AddComponent<AudioSource> ();
		mLevelUpAudioSource.clip = levelUp;
		mLevelUpAudioSource.loop = false;
		
		mMenuMusic = gameObject.AddComponent<AudioSource> ();
		mMenuMusic.clip = menuMusic;
		mMenuMusic.loop = true;

		mRainMusic = gameObject.AddComponent<AudioSource> ();
		mRainMusic.clip = rainLoop;
		mRainMusic.loop = true;

		playMenuMusic ();
	}
}
