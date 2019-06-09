using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    movingLeft,
    movingRight,
    movingUp,
    movingDown
}
public class MovingPlatform : MonoBehaviour
{

    public GameState currentState = GameState.movingDown;

    //public Vector3 horizontalSpeed;
    private Vector3 verticalSpeed = new Vector3(0f, .1f, 0f);

    public float minY = -7.2f;
    public float maxy = 12.5f;

    private void Update()
    {
        ChangeState();
        VerticalMovePlatform();
        //HorizontalMovePlatform();
    }

    //private void HorizontalMovePlatform()
    //{
    //    if (currentState == GameState.movingRight)
    //    {
    //        transform.position += horizontalSpeed;
    //    }
    //    else if (currentState == GameState.movingLeft)
    //    {
    //        transform.position -= horizontalSpeed;
    //    }
    //}

    private void VerticalMovePlatform()
    {
        if (currentState == GameState.movingUp)
        {
            transform.position += verticalSpeed;
        }
        else if (currentState == GameState.movingDown)
        {
            transform.position -= verticalSpeed;
        }
    }

    private void ChangeState()
    {
        if (transform.position.y >= maxy)
        {
            currentState = GameState.movingDown;
        }
        else if (transform.position.y <= minY)
        {
            currentState = GameState.movingUp;
        }
    }
    private IEnumerator StopCoUp()
    {
        yield return new WaitForSeconds(5f);
        verticalSpeed = new Vector3(0f, .2f, 0f);
        currentState = GameState.movingDown;
    }
    private IEnumerator StopCoDown()
    {
        yield return new WaitForSeconds(5f);
        verticalSpeed = new Vector3(0f, .2f, 0f);
        currentState = GameState.movingUp;
    }
}
