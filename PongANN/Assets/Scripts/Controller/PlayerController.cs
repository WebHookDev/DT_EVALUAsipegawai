
﻿using UnityEngine;
using System.Collections;

public class PlayerController : PaddleController
{
    override protected void Start()
    {
        base.Start();
        OnMoveUp += MoveUp;
        OnMoveDown += MoveDown;
    }

    protected override void OnBallCollide( Vector3 ballPosition )
    {
        GameMgr.Instance.AI.OnBallThrown();
    }

    private void Update ()