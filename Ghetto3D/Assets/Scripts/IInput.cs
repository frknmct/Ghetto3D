using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jw
{
    public interface IInput 
    {
        public Action<Vector2> OnMovementInput { get; set; }
        public Action<Vector3> OnMovementDirectionInput{ get; set; }
    }
}


