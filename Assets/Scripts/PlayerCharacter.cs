using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public enum State
    {
        None,
        Idle,
        Walk,
        Jump
    }

    private State currentState_ = State.None;

    [SerializeField] private Animator anim_;

    private const float deadZone_ = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move -= 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move += 1.0f;
        }
        switch (currentState_)
        {
            case State.Idle:
                if (Mathf.Abs(move) > deadZone_)
                {
                    ChangeState(State.Walk);
                }
                break;
            case State.Walk:
                if (Mathf.Abs(move) < deadZone_)
                {
                    ChangeState(State.Idle);
                }
                break;
            case State.Jump:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                anim_.Play("Idle");
                break;
            case State.Walk:
                anim_.Play("Walk");
                break;
            case State.Jump:
                anim_.Play("Jump");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        currentState_ = state;
    }
}
