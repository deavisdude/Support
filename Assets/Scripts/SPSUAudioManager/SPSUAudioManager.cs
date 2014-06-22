﻿using UnityEngine;
using System.Collections;

public class SPSUAudioManager : MonoBehaviour
{
	// ==================================================
	// Variables
	// ==================================================

	public AudioClip mBirdsAndTheBees;
	public AudioClip mBodyHit;

	public AudioClip mCreditsClip;

	public AudioClip mDoorOpen;
	public AudioClip mEvilLaugh;

	public AudioClip mGameMusicLoop1;
	public AudioClip mGameMusicLoop2;
	public AudioClip mGameMusicLoop3;
	public AudioClip mGameMusicLoop4;
	public AudioClip mGameMusicLoop5;

	public AudioClip mGateOpen;

	public AudioClip mJump;

	public AudioClip mLevelUp;

	public AudioClip mMenuSound;

	public AudioClip mPressurePlateActivated;
	public AudioClip mPressurePlateDeactivated;

	public AudioClip mRainLoop;

	public AudioClip mSigh;

	private AudioSource mMusicLoop;
	private AudioSource mRainMusic;
	private AudioSource mCreditsMusic;

	private bool mGameMusicHasBeenStarted = false;
	private bool mIsFadingCreditsOut = false;

	private float mCurrentClipLength = 0;
	private float mCurrentTime = 0;

	private int mCurrentTrackIndex = 0;

	// ==================================================
	// Methods
	// ==================================================

	// =========================
	// Music Methods
	// =========================

	public void Update ()
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
				mCreditsMusic.volume += 0.03f;
			}
		}

		if (!mGameMusicHasBeenStarted) {
			return;
		}

		mCurrentTime += Time.deltaTime;

		if (mCurrentTime > mCurrentClipLength) {
			mCurrentTime = 0;
			play ();
		}
	}

	public void play ()
	{
		switch (mCurrentTrackIndex) {
		case 1:
			mMusicLoop.clip = mGameMusicLoop1;
			mRainMusic.volume = 1;
			break;

		case 2:
			mMusicLoop.clip = mGameMusicLoop2;
			mRainMusic.volume = .8f;
			break;

		case 3:
			mMusicLoop.clip = mGameMusicLoop3;
			mRainMusic.volume = .6f;
			break;
		case 4:
			mMusicLoop.clip = mGameMusicLoop4;
			mRainMusic.volume = .4f;
			break;
		case 5:
			mMusicLoop.clip = mGameMusicLoop5;
			mRainMusic.volume = .2f;
			break;
		}

		mCurrentClipLength = mMusicLoop.clip.length;
		mMusicLoop.Play ();
	}

	public void incrementTrackIndex ()
	{
		mCurrentTrackIndex ++;

		if (!mGameMusicHasBeenStarted) {
			mGameMusicHasBeenStarted = true;
			play ();
		}
	}

	public void playRainLoop ()
	{
		if (mRainMusic == null) {
			mRainMusic = gameObject.AddComponent<AudioSource> ();
			mRainMusic.clip = mRainLoop;
			mRainMusic.loop = true;
		} 

		if (!mRainMusic.isPlaying) {
			mRainMusic.Play ();
		}
	}

	public void playCreditsMusic ()
	{
		if (mCreditsMusic == null) {
			mCreditsMusic = gameObject.AddComponent<AudioSource> ();
			mCreditsMusic.clip = mCreditsClip;
			mCreditsMusic.loop = true;
		} 

		if (!mCreditsMusic.isPlaying) {
			mCreditsMusic.Play ();
			mIsFadingCreditsOut = false;
			mCreditsMusic.volume = 0;
		}
	}

	public void fadeCreditsMusicOut ()
	{
		mIsFadingCreditsOut = true;
	}

	public void startGameMusic ()
	{
		fadeCreditsMusicOut ();
		playRainLoop ();
	}

	// =========================
	// Sound Effect Methods
	// =========================

	public void playBirdsAndBeesSound ()
	{
		AudioSource.PlayClipAtPoint (mBirdsAndTheBees, transform.position);
	}
	
	public void playBodyHitSound ()
	{
		AudioSource.PlayClipAtPoint (mBodyHit, transform.position);
	}
	
	public void playDoorOpenSound ()
	{
		AudioSource.PlayClipAtPoint (mDoorOpen, transform.position);
	}
	
	public void playEvilLaughSound ()
	{
		AudioSource.PlayClipAtPoint (mEvilLaugh, transform.position);
	}
	
	public void playGateOpenSound ()
	{
		AudioSource.PlayClipAtPoint (mGateOpen, transform.position);
	}
	
	public void playJumpSound ()
	{
		AudioSource.PlayClipAtPoint (mJump, transform.position, .5f);
	}
	
	public void playMenuSound ()
	{
		AudioSource.PlayClipAtPoint (mMenuSound, transform.position);
	}
	
	public void playPressurePlateActivatedSound ()
	{
		AudioSource.PlayClipAtPoint (mPressurePlateActivated, transform.position);
	}
	
	public void playPressurePlateDeactivedSound ()
	{
		AudioSource.PlayClipAtPoint (mPressurePlateDeactivated, transform.position);
	}
	
	public void playSigh ()
	{
		AudioSource.PlayClipAtPoint (mSigh, transform.position);
	}

	// =========================
	// Lifecycle Methods
	// =========================

	protected void Start ()
	{
		mMusicLoop = gameObject.AddComponent<AudioSource> ();
		GameObject.DontDestroyOnLoad (gameObject);
		playCreditsMusic ();
	}
}
