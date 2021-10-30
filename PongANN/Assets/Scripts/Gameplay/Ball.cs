
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

    public delegate void BallEventHandler();
    public event BallEventHandler OnBallThrown;
    public event BallEventHandler OnBallCollidePaddle;

    #region tools
    static public float GetAngleInt(Vector2 dir)
    {
        int angle = Mathf.RoundToInt(Mathf.Acos(dir.x) * Mathf.Sign(dir.y) * Mathf.Rad2Deg);
        return GetAngle0To1(angle);
    }

    static public float GetAngle0To1(int angle)
    {
        float angle0To1 = (angle + 90) / 180;
        return GetRoundedValue(angle0To1);
    }