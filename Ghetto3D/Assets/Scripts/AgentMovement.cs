using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{ 
    CharacterController controller;
    Animator animator;
    public float rotationSpeed, movementSpeed, gravity = 20;
    private Vector3 movementVector = Vector3.zero;
    private float desiredRotationAngle = 0;

    private float inputVerticalDirection = 0;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void HandleMovement(Vector2 input)
    {
        if (controller.isGrounded)
        {
            if (input.y != 0)
            {
                if (input.y > 0)
                {
                    inputVerticalDirection = Mathf.CeilToInt(input.y);
                }
                else
                {
                    inputVerticalDirection = Mathf.FloorToInt(input.y);
                }
                movementVector = input.y * transform.forward * movementSpeed;
            }
            else
            {
                movementVector = Vector3.zero;
                animator.SetFloat("Move",0);
            }
        }
    }

    public void HandleMovementDirection(Vector3 direction)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, direction);
        var crossProduct = Vector3.Cross(transform.forward, direction).y;
        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    private void RotateAgent()
    {
        Debug.Log(desiredRotationAngle);
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed* Time.deltaTime);
        }
    }

    private float SetCorrectAnimation(float inputVerticalDirectionn)
    {
        float currentAnimationSpeed = animator.GetFloat("Move");
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            if (Mathf.Abs(currentAnimationSpeed) < 0.2f)
            {
                currentAnimationSpeed += inputVerticalDirectionn * Time.deltaTime * 2;
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, -0.2f, 0.2f);
            }
            animator.SetFloat("Move",currentAnimationSpeed);
        }
        else
        {
            if (currentAnimationSpeed < 1)
            {
                currentAnimationSpeed +=  inputVerticalDirectionn * Time.deltaTime * 2;
            }
            animator.SetFloat("Move",Mathf.Clamp(currentAnimationSpeed,-1,1) );
        }
        return Mathf.Abs(currentAnimationSpeed);
    }
    private void Update()
    {
        if (controller.isGrounded)
        {
            if (movementVector.magnitude > 0)
            {
                var animationSpeedMultiplier = SetCorrectAnimation(inputVerticalDirection);
                RotateAgent();
                movementVector *= animationSpeedMultiplier;
            }
        }
        movementVector.y -= gravity;
        controller.Move(movementVector * Time.deltaTime);
    }
}
