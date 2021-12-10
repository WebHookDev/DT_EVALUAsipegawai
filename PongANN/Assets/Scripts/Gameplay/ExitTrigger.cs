using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    [SerializeField]
    private bool isLeftSide = true;
    public delegate void OnBallExitHandler(bool isLeftSide);
    public event OnBallExitHandler OnBallExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            OnBallExit(isLef