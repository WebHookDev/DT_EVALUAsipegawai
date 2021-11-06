
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

    static public float ComputeBallPosCourt(float pos0To1)
    {
        return (pos0To1 - 0.5f) * GameMgr.Instance.CourtHeight;
    }
    #endregion


    // Use this for initialization
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsBallStuck())
            ForceBallBounceBack();
        else if (IsBallOut())
            transform.position = Vector2.zero;
    }

    public void Launch(bool useRandomDir = false, bool repeatLastLaunch = false)
    {
        float angle = 0.5f;
        if (repeatLastLaunch)
        {
            // TODO
        }
        else if (useRandomDir)
        {
            angle = UnityEngine.Random.Range(0.2f, 0.8f);
        }

        int degAngle = GetAngleDegree(angle);
        Vector2 dir = new Vector2(Mathf.Cos(degAngle * Mathf.Deg2Rad), Mathf.Sin(degAngle * Mathf.Deg2Rad));
        rigidBody.velocity = dir * InitialSpeed;
        currentSpeed = InitialSpeed;

        if (OnBallThrown != null)
            OnBallThrown();
    }

    private float ComputeHitFactor(Vector2 racketPos, float racketHeight)
    {
        return (transform.position.y - racketPos.y) / racketHeight;
    }

    private bool IsBallStuck()
    {
        return GameMgr.Instance.IsBallLaunched && rigidBody.velocity.magnitude <= 0.001f;
    }

    private bool IsBallOut()
    {
        return Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.y) > 10f;