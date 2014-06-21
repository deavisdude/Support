using UnityEngine;
using System.Collections;

public class SPSUAudioManager : MonoBehaviour
{
	// ==================================================
	// Variables
	// ==================================================

	public AudioClip mBirdsAndTheBees;
	public AudioClip mBodyHit;
	public AudioClip mDoorOpen;
	public AudioClip mEvilLaugh;
	public AudioClip mGateOpen;
	public AudioClip mJump;
	public AudioClip mLevelUp;
	public AudioClip mMenuSound;
	public AudioClip mPressurePlateActivated;
	public AudioClip mPressurePlateDeactivated;
	public AudioClip mRainLoop;
	public AudioClip mSigh;

	// ==================================================
	// Methods
	// ==================================================

	// =========================
	// Music Methods
	// =========================
	
	public void playRainLoop ()
	{
		AudioSource rainSource = gameObject.AddComponent<AudioSource> ();
		rainSource.clip = mRainLoop;
		rainSource.loop = true;
		rainSource.Play ();
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
		GameObject.DontDestroyOnLoad (gameObject);
		playRainLoop ();
	}
}
