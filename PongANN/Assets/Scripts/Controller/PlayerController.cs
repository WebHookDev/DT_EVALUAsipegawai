
﻿using UnityEngine;
using System.Collections;

public class PlayerController : PaddleController
{
    override protected void Start()
    {
        base.Start();
        OnMoveUp += MoveUp;