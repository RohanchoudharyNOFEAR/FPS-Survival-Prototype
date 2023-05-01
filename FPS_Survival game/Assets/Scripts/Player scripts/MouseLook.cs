using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private  Transform Playerroot, Lookroot;
    [SerializeField]
    private bool _invert;
    [SerializeField]
    private bool _canUnlock;
    [SerializeField]
    private float _sensitivity = 5f;
    [SerializeField]
    private int _smoothSteps = 10;
    [SerializeField]
    private float _smoothWeight = 0.4f;
    [SerializeField]
    private float rollAngle = 10f;
    [SerializeField]
    private float _rollSpeed = 3f;
    [SerializeField]
    private Vector2 _default_look_limits = new Vector2(-70f, 80f);

    private Vector2 _lookAngles;
    private Vector2 _currentMouseLook;
    private Vector2 _smoothMove;
    private float _currentRollAngle;
    private int _lastLookFrame;








    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        
    }

    // Update is called once per frame
    void Update()
    {
        LockandUnlockcursor();

        if(Cursor.lockState== CursorLockMode.Locked)
        {
            LookAround();
        }
        
    }


    void LockandUnlockcursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        //here we take input
        _currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        //here we add a value to input 
        _lookAngles.x += _currentMouseLook.x* _sensitivity * (_invert ? 1f : -1f);
        _lookAngles.y += _currentMouseLook.y * _sensitivity;
        //we are limiting move view angle using clamp
        _lookAngles.x = Mathf.Clamp(_lookAngles.x, _default_look_limits.x, _default_look_limits.y);
        //current roll angle is useless here it is just to make player look drunk
        _currentRollAngle = Mathf.Lerp(_currentRollAngle, Input.GetAxis("Mouse X") * rollAngle, Time.deltaTime * _rollSpeed);
        //here we are assigning the up and down movement (x for local scale) and current rollangle which we dont use
        Lookroot.localRotation = Quaternion.Euler(_lookAngles.x, 0f, _currentRollAngle);
        //here we give left right movemnt to player 
        Playerroot.localRotation = Quaternion.Euler(0f, _lookAngles.y, 0f);
    }
}
