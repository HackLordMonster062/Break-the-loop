using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager> {
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource sfxSource;
	[SerializeField] private List<AudioClip> soundEffects;

	private Dictionary<string, AudioClip> sfxDict;

	private void Start() {
		InitializeSFXDictionary();

		musicSource.Play();
	}

	private void InitializeSFXDictionary() {
		sfxDict = new Dictionary<string, AudioClip>();
		foreach (var clip in soundEffects) {
			sfxDict[clip.name] = clip;
		}
	}

	public void PlaySound(string soundName) {
		if (sfxDict.TryGetValue(soundName, out AudioClip clip)) {
			sfxSource.PlayOneShot(clip);
		}
	}

	public void ToggleMusic(bool play = true) {
		if (play)
			musicSource.UnPause();
		else
			musicSource.Pause();
	}

	public void SetVolume(float musicVolume, float sfxVolume) {
		musicSource.volume = musicVolume;
		sfxSource.volume = sfxVolume;
	}
}
