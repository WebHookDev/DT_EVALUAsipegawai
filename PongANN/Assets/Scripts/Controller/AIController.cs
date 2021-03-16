using UnityEngine;

public class AIController : PaddleController
{
    [SerializeField]
    public bool isLearning = false;
    [SerializeField]
    private int updateFrenquency = 1;

    private int frameCount = 0;
    private float wantedPosY = 0f;

    [SerializeField]
    private int m_hiddenLayerNb = 1;

    [SerializeField]
    private int m_hiddenLayerNeuronsNb = 30;

    private NeuralNetwork m_neuralNetwork = null;
    private float m_ballDirectionX;
    private float m_ballDirectionY;
    private float m_ballPosY;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        GameMgr.Instance.Ball.OnBallThrown += OnBallThrown;
        m_neuralNetwork = new NeuralNetwork( 3, 1, m_hiddenLayerNb, m_hiddenLayerNeuronsNb );
    }

    private void FixedUpdate()
    {
        if ( ++frameCount % updateFrenquency != 0 )
            return;

  