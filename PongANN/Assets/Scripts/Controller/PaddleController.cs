
﻿using UnityEngine;
using System.Collections;

public abstract class PaddleController : MonoBehaviour
{
    [SerializeField]
    protected float speed = 8f;
    protected float height = 1f;
    protected float border = 0f;

    public delegate void InputHandler();

    public InputHandler OnLaunchBall;
    public InputHandler OnMoveUp;
    public InputHandler OnMoveDown;

    virtual protected void Start()