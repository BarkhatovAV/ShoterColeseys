using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [field: SerializeField] public float Speed { get; protected set; } = 2f;
    [field: SerializeField] public int MaxHealth { get; protected set; } = 10;
    public Vector3 Velocity { get; protected set; }
}
