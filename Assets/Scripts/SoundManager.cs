using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    
    public static SoundManager Instance { get; private set; }
    public enum Sound {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }
    
    private AudioSource _audioSource;
    private Dictionary<Sound, AudioClip> _audioClips;
    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        
        _audioClips = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            _audioClips[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

    }

    public void PlaySound(Sound sound)
    {
        _audioSource.PlayOneShot(_audioClips[sound]);        
    }
}
