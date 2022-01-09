
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
    private Text timeScaleText;
    private Slider timeScaleSlider;
    private GameObject ToggleRandomDirGao;

    private Ball ball;
    public Ball Ball
    {
        get { return ball; }
    }
    bool isBallLaunched = false;
    public bool IsBallLaunched
    {
        get { return isBallLaunched; }
    }

    private GameObject playerGao;
    private GameObject aiGao;

    private AIController ai;
    public AIController AI
    {
        get { return ai; }
    }

    [SerializeField]
    private bool isTrainingModeOn = false;
    public bool IsTrainingModeOn
    {
        get { return isTrainingModeOn; }
    }
    [SerializeField]
    private bool randomDirOn = false;
    private int trainingStep = 0;
    private int nbTrainingSteps = 0;
    private bool isTrainingPosDownward = true;

    // Use this for initialization
    void Awake()
    {
        ball = FindObjectOfType<Ball>();
        playerGao = GameObject.Find( "Paddle1" );
        aiGao = GameObject.Find( "Paddle2" );
        ai = aiGao.GetComponent<AIController>();

        score1Text = GameObject.Find( "Score1" ).GetComponent<Text>();
        score2Text = GameObject.Find( "Score2" ).GetComponent<Text>();

        timeScaleSlider = GameObject.Find( "TimeScaleSlider" ).GetComponent<Slider>();
        timeScaleText = GameObject.Find( "TimeScaleText" ).GetComponent<Text>();
        timeScaleText.text = string.Format( "TimeScale {0:0.0}", timeScaleSlider.value );

        ToggleRandomDirGao = GameObject.Find( "ToggleRandomDir" );
        ToggleRandomDirGao.SetActive( false );

        courtHeight = Mathf.RoundToInt( Camera.main.orthographicSize * 2f );
        nbTrainingSteps = Mathf.RoundToInt( courtHeight / ball.transform.localScale.y );
    }

    void Start()
    {
        foreach ( ExitTrigger trigger in FindObjectsOfType<ExitTrigger>() )
        {
            trigger.OnBallExit += OnBallExit;
        }
        playerGao.GetComponent<PlayerController>().OnLaunchBall += TryLaunchBall;
    }

    void Update()
    {
        if ( isTrainingModeOn && isBallLaunched == false )
        {
            Vector3 trainingPos = playerGao.transform.position + Vector3.right * 0.6f;


            if ( isTrainingPosDownward )
                trainingPos.y = CourtHeight / 2f
                                - ball.transform.localScale.y / 2f
                                - ball.transform.localScale.y * trainingStep;

            else
                trainingPos.y = CourtHeight / 2f
                                - ball.transform.localScale.y / 2f
                                - ball.transform.localScale.y * ( nbTrainingSteps - trainingStep );


            ball.transform.position = trainingPos;
            if ( trainingStep == nbTrainingSteps - 1 ) isTrainingPosDownward = !isTrainingPosDownward;
            trainingStep = ( trainingStep + 1 ) % nbTrainingSteps;
            TryLaunchBall();
        }
    }

    void LateUpdate()
    {
        if ( isTrainingModeOn == false && isBallLaunched == false )
        {
            ball.transform.position = playerGao.transform.position + Vector3.right * 0.6f;
        }
    }

    public void TryLaunchBall()
    {
        if ( isBallLaunched == false )
        {
            if ( isTrainingModeOn )
                ball.Launch( randomDirOn );
            else
                ball.Launch();

            isBallLaunched = true;
        }
    }

    private void OnBallExit( bool isLeftSide )
    {
        ball.Rigidbody.velocity = Vector2.zero;
        isBallLaunched = false;
