using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	private Ctrl ctrl;
	public AudioClip cursor;
	/// <summary>
	/// 播放鼠标音效
	/// </summary>
	public void PlayCursor()
	{
		PlayAudio(cursor);
	}

	public AudioClip drop;
	/// <summary>
	/// 播放方块下降音效
	/// </summary>
	public void PlayDrop()
	{
		PlayAudio(drop);
	}

	public AudioClip control;
	/// <summary>
	/// 播放方块左右移动音效
	/// </summary>
	public void PlayControl()
	{
		PlayAudio(control);
	}

	public AudioClip lineClear;
	/// <summary>
	/// 播放方块消除音效
	/// </summary>
	public void PlayLineClear()
	{
		PlayAudio(lineClear);
	}

	private void PlayAudio(AudioClip clip)
	{
		if (isMute) return;
		audioSource.clip = clip;
		audioSource.Play();
	}
	private AudioSource audioSource;
	private bool isMute = false;
	private void Awake()
	{
		ctrl = GetComponent<Ctrl>();
		audioSource = GetComponent<AudioSource>();
	}

	public void OnAudioButtonClick()
	{
		isMute = !isMute;
		ctrl.view.SetMuteActive(isMute);
		if (isMute == false)
		{
			PlayCursor();
		}
	}
}
