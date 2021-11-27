using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    [SerializeField]
    private bool isLeftSide = true;
    public delegate void OnBallExitHandler