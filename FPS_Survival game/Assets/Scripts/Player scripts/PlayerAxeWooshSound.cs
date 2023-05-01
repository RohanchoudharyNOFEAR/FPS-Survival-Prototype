using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeWooshSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] _wooshSounds;

    void PlayWooshSounds()
    {
        _audioSource.clip = _wooshSounds[Random.Range(0, _wooshSounds.Length)];
        _audioSource.Play();
    }
   
}
