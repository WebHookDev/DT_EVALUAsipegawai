
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

    static public int GetAngleDegree(float angle)
    {
        int angleDegree = Mathf.RoundToInt((angle * 180f) - 90f);
        return angleDegree;
    }

    static public float GetRoundedValue(float val, int nbDecimal = 1)
    {
        return (float)Math.Round(Convert.ToDecimal(val), nbDecimal);
    }

    static public float GetBallPos0To1(float posY)
    {
        int courtHeight = GameMgr.Instance.CourtHeight;
        float output = posY / courtHeight + 0.5f;
        return output = Mathf.Max(Mathf.Min(output, 1f), 0f);
    }

    static public float GetBallPos0To1Rounded(float posY)
    {
        return GetRoundedValue(GetBallPos0To1(posY), 2);
    }