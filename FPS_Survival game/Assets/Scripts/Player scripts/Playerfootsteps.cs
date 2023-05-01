using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfootsteps : MonoBehaviour
{
    private AudioSource _footsteps_sounds;
    [SerializeField]
    private AudioClip[] _footsteps_clip;
    private CharacterController _characterController;
    [HideInInspector]
    public float Vol_Min, Vol_Max;
    private float _accumilated_distance;
    [HideInInspector]
    public float Step_distance;




    private void Awake()
    {
        _characterController = GetComponentInParent<CharacterController>();
        _footsteps_sounds = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChecToPlayeFootstepsSound();
    }


    void ChecToPlayeFootstepsSound()
    {
        if (!_characterController.isGrounded)

            return;

        if (_characterController.velocity.sqrMagnitude > 0)
        {
            _accumilated_distance += Time.deltaTime;
            if (_accumilated_distance > Step_distance)
            {
                _footsteps_sounds.volume = Random.Range(Vol_Min, Vol_Max);
                _footsteps_sounds.clip = _footsteps_clip[Random.Range(0, _footsteps_clip.Length)];
                _footsteps_sounds.Play();

                _accumilated_distance = 0f;
            }


        }
        else
        { _accumilated_distance = 0f; }
    }
}
