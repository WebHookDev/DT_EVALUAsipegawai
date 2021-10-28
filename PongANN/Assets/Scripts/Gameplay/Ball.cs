
﻿using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float InitialSpeed = 10f;
    [SerializeField]
    private float MaxSpeed = 30f;
    [SerializeField]
    private float HitAcceleration = 1f;
    float currentSpeed;
    Rigidbody2D rigidBody;
    public Rigidbody2D Rigidbody { get { return rigidBody; } }
