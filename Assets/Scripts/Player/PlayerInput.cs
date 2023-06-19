using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void PlayerInputHandler(Vector3 movement);
    public static event PlayerInputHandler OnPlayerInput;


     private FloatingJoystick floatingJoystick;
    private Vector3 movementVector;
    private void Awake()
    {
        if(floatingJoystick == null)
        {
            floatingJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
        }
    }

    private void FixedUpdate()
    {
        JoyStickMovement(movementVector);
    }

    private void JoyStickMovement(Vector3 moveVector)
    {
        moveVector = Vector3.zero;
        moveVector.x = floatingJoystick.HorizontalAxis();
        moveVector.z = floatingJoystick.VerticalAxis();
        OnPlayerInput?.Invoke(moveVector);

    }
}
