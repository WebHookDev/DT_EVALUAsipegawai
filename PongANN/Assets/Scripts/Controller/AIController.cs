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
    private int m_hiddenLayerNe