
﻿using UnityEngine;
using System.Collections;

public abstract class PaddleController : MonoBehaviour
{
    [SerializeField]
    protected float speed = 8f;
    protected float height = 1f;
    protected float border = 0f;

    public delegate void InputHandler();

    public InputHandler OnLaunchBall;
    public InputHandler OnMoveUp;
    public InputHandler OnMoveDown;

    virtual protected void Start()
    {
        height = transform.localScale.y;
        border = GameMgr.Instance.CourtHeight / 2f - height / 2f;
    }

    protected void MoveUp()
    {
        if ( transform.position.y < border )
            transform.Translate( Vector3.up * speed * Time.deltaTime );
    }

    protected void MoveDown()
    {
        if ( transform.position.y > -border )
            transform.Translate( Vector3.down * speed * Time.deltaTime );
    }

    protected void MoveToPos( float wantedPosY )
    {
        wantedPosY = Mathf.Max( Mathf.Min( wantedPosY, border ), -border );

        if ( GameMgr.Instance.IsTrainingModeOn )
        {
            transform.position = new Vector2( transform.position.x, wantedPosY );
        }
        else
        {
            Vector3 toPos = new Vector3( 0f, wantedPosY - transform.position.y, 0f );
            if ( toPos.magnitude > 0.1f )
                toPos = toPos.normalized * speed;

            transform.Translate( toPos * Time.deltaTime );
        }
    }

    private void OnCollisionEnter2D( Collision2D col )
    {
        Ball ball = col.gameObject.GetComponent<Ball>();
        if ( ball != null )
            OnBallCollide( ball.transform.position );
    }

    protected void LaunchBall()
    {
        GameMgr.Instance.TryLaunchBall();
    }

    protected abstract void OnBallCollide( Vector3 ballPosition );
}