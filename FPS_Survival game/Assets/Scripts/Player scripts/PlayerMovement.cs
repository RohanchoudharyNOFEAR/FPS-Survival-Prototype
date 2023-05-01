using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 5f;
    public float _jumpforce = 10f;
    private float _gravity = 20f;
    private float _vertical_Velocity;
    private CharacterController _characterController;
    private Vector3 move_Direction;





    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        move_Direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= Speed * Time.deltaTime;

        ApplyGravity();
   
        _characterController.Move(move_Direction);



    }

    void ApplyGravity()
    {
        _vertical_Velocity -= _gravity * Time.deltaTime;
        Jump();
        move_Direction.y = _vertical_Velocity * Time.deltaTime;
    }

    void Jump()
    {
        if(_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)){

            _vertical_Velocity = _jumpforce;
        }
    }
}
