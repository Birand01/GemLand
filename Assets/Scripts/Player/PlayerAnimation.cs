using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {

        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerInput.OnPlayerInput += MovementAnimation;
    }

    private void MovementAnimation(Vector3 movementVector)
    {
        if (movementVector.x != 0 || movementVector.z != 0)
        {
            anim.SetFloat("Speed", 1.0f);
        }
        else
        {
            anim.SetFloat("Speed", 0.0f);
        }

    }
}
