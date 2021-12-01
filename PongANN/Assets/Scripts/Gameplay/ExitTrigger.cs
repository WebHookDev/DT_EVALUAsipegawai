using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    [SerializeField]
    private bool isLeftSide = true;
    public delegate void OnBallExitHandler(bool isLeftSide);
    public event OnBallExitHandler OnBallExit;

    private v