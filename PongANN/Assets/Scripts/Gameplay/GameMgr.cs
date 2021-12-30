
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    static GameMgr instance = null;
    static public GameMgr Instance
    {
        get
        {
            if ( instance == null )
                instance = FindObjectOfType<GameMgr>();
            return instance;
        }
    }

    private int courtHeight = 0;
    public int CourtHeight
    {