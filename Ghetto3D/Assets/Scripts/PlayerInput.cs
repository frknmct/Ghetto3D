using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jw
{
    public class PlayerInput : MonoBehaviour , IInput
    {
        public Action<Vector2> OnMovementInput { get; set; }
        public Action<Vector3> OnMovementDirectionInput{ get; set; }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            GetMovementInput();
            GetMovementDirection();
        }

        private void GetMovementDirection()
        {
            var cameraForwardDirection = Camera.main.transform.forward;
            Debug.DrawRay(Camera.main.transform.position,cameraForwardDirection*10,Color.red);
            var directionMoveIn = Vector3.Scale(cameraForwardDirection,(Vector3.right + Vector3.forward));
            Debug.DrawRay(Camera.main.transform.position,directionMoveIn*10,Color.blue);
            OnMovementDirectionInput?.Invoke(directionMoveIn);
        }

        private void GetMovementInput()
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            OnMovementInput?.Invoke(input);
        }
    }
}





