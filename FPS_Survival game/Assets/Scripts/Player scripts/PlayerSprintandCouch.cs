using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintandCouch : MonoBehaviour
{
    public float Sprint_Speed = 10f;
    public float Move_Speed = 5f;
    public float Crouch_Speed = 2f;
    public Transform Look_Root;
    private PlayerMovement _playermovement;
    private bool _is_crouching;
    private float _stand_Height = 0.62f;
    private float _crouch_Height = 0.30f;
    private Playerfootsteps _playerfootsteps;
    private float _sprint_vol = 1f;
    private float _crouch_vol = 0.1f;
    private float _walk_vol_min = 0.2f, _walk_vol_max = 0.6f;
    private float _walk_step_distance = 0.4f;
    private float _sprint_step_distance = 0.25f;
    private float _crouch_step_distance = 0.5f;

    private PlayerStats _Playerstats;
    private float Sprintvalue = 100f;
    public float Sprint_threshold = 10f;
    private CharacterController _CharacterController;



    private void Awake()
    {
        
        _playermovement = GetComponent<PlayerMovement>();
        Look_Root = transform.GetChild(0);
        _playerfootsteps = GetComponentInChildren<Playerfootsteps>();
        _Playerstats = GetComponent<PlayerStats>();
        _CharacterController = GetComponent<CharacterController>();

    }
    // Start is called before the first frame updates
    void Start()
    {
        _is_crouching = false;
        _playerfootsteps.Vol_Min = _walk_vol_min;
        _playerfootsteps.Vol_Max = _walk_vol_max;
        _playerfootsteps.Step_distance = _walk_step_distance;


    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if(Sprintvalue > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_is_crouching  )
            {
                _playermovement.Speed = Sprint_Speed;

                _playerfootsteps.Step_distance = _sprint_step_distance;
                _playerfootsteps.Vol_Max = _sprint_vol;
                _playerfootsteps.Vol_Min = _sprint_vol;
            }
        }
         if(Input.GetKeyUp(KeyCode.LeftShift)&& !_is_crouching)
        {
            _playermovement.Speed = Move_Speed;

            _playerfootsteps.Step_distance = _walk_step_distance;
            _playerfootsteps.Vol_Min = _walk_vol_min;
            _playerfootsteps.Vol_Max = _walk_vol_max;
           

        }

        if(Input.GetKey(KeyCode.LeftShift) && !_is_crouching && _CharacterController.velocity.sqrMagnitude > 0)
        {
            Sprintvalue -= Sprint_threshold * Time.deltaTime;
            if (Sprintvalue <= 0)
            {
                Sprintvalue = 0; 
                _playermovement.Speed = Move_Speed;

                _playerfootsteps.Step_distance = _walk_step_distance;
                _playerfootsteps.Vol_Min = _walk_vol_min;
                _playerfootsteps.Vol_Max = _walk_vol_max;



            }

            _Playerstats.Display_Staminastats(Sprintvalue);
        }
        else
        {
            if (Sprintvalue != 100f)
            {
                Sprintvalue += (Sprint_threshold / 2) * Time.deltaTime;
                _Playerstats.Display_Staminastats(Sprintvalue);
            }
            if (Sprintvalue >= 100f)
            {
                Sprintvalue = 100f;
            }
        }


    }


    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_is_crouching == true)
            {
                Look_Root.localPosition = new Vector3(0f, _stand_Height, 0f);
                _playermovement.Speed = Move_Speed;

                _playerfootsteps.Vol_Min = _walk_vol_min;
                _playerfootsteps.Vol_Max = _walk_vol_max;
                _playerfootsteps.Step_distance = _walk_step_distance;

                _is_crouching = false;
            }
            else
            {
                Look_Root.localPosition = new Vector3(0f, _crouch_Height, 0f);
                _playermovement.Speed = Crouch_Speed;

                _playerfootsteps.Step_distance = _crouch_step_distance;
                _playerfootsteps.Vol_Max = _crouch_vol;
                _playerfootsteps.Vol_Min = _crouch_vol;

                _is_crouching = true;
            }

        }
        
    }

}
