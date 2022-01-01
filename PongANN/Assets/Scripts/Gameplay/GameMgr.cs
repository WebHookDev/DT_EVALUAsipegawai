
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
        get { return courtHeight; }
    }

    private int scoreP1 = 0;
    private int scoreP2 = 0;

    private Text score1Text;
    private Text score2Text;

    // debug output text
    private Text ballDataPosYText;
    private Text ballDataVelocityText;