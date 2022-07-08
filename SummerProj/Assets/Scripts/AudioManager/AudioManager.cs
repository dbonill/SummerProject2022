using UnityEngine.Audio;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup MusicVolume;

	public AudioMixerGroup SoundEffectVolume;

	public Sound[] SoundEffects;

	public Sound[] MusicLevelOne;

	public List<string> levelOneChoices;


	void Awake()
	{
		gameObject.transform.parent = null;
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in SoundEffects)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.outputAudioMixerGroup = SoundEffectVolume;
		}

		foreach (Sound s in MusicLevelOne)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.outputAudioMixerGroup = MusicVolume;
		}

	}



	public void PlayEffect(string sound)
	{
		Sound s = Array.Find(SoundEffects, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}


		s.source.Play();
	}

	public void PlayMusic(string sound)
	{
		Sound s = Array.Find(MusicLevelOne, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}


		s.source.Play();
	}

	public void playRandomTrackLevel1()
	{
		int randomTrackIndex = UnityEngine.Random.Range(0, levelOneChoices.Count);
		PlayMusic(levelOneChoices[randomTrackIndex]);
		Debug.Log("Now Playing " + levelOneChoices[randomTrackIndex]);
	}


	private bool alternate = false;
	public void playPlayerSteps()
	{
		if (alternate)
		{
			PlayEffect("Step1");
			alternate = false;
		}
		else
		{
			PlayEffect("Step2");
			alternate = true;
		}
	}


	private AudioSource[] allAudioSources;

	public void StopAllAudio()
	{
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource audioS in allAudioSources)
		{
			audioS.Stop();
		}
	}


}
