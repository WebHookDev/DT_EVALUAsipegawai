using UnityEngine;

public class AIController : PaddleController
{
    [SerializeField]
    public bool isLearning = false;
    [SerializeField]
    private int updateFrenquency = 1;

    private int frameCount = 0;
 