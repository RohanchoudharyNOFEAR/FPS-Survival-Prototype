using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
   
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip[] _attack_clips;
    [SerializeField]
    private AudioClip _Screme_clip, _die_clip;



    // Start is called before the first frame update
    void Start()
    {
       _AudioSource =  GetComponent<AudioSource>();
        
    }

    public void Play_Scremesounds()
    {
        _AudioSource.clip = _Screme_clip;
        _AudioSource.Play();
    }

   public  void Play_AttackSound()
    {
        _AudioSource.clip =_attack_clips[ Random.Range(0, _attack_clips.Length)];
        _AudioSource.Play();
    }

   public  void Play_deadsound()
    {
        _AudioSource.clip = _die_clip;
        _AudioSource.Play();
    }
}
