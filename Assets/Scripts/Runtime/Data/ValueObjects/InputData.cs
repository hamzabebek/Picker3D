using System;
using UnityEngine;

[Serializable]
public struct InputData
{
     public float HorizontalInputSpeed { get; set; }

     public Vector2 ClampValues { get; set; }

     public float  ClampSpeed { get; set; }
    
}