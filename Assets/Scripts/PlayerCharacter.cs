using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private const float DeadZone = 0.1f;

    private bool facingRight_ = true;
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
        //We flip the characters when not facing in the right direction
        if (move > DeadZone && !facingRight_)
        {
            Flip();
        }

        if (move < -DeadZone && facingRight_)
        {
            Flip();
        }
        //We manage the state machine of the character
        switch (currentState_)
        {
            case State.Idle:
                if (Mathf.Abs(move) > DeadZone)
                {
                    ChangeState(State.Walk);
                }
                break;
            case State.Walk:
                if (Mathf.Abs(move) < DeadZone)
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
                anim.Play("Idle");
                break;
            case State.Walk:
                anim.Play("Walk");
                break;
            case State.Jump:
                anim.Play("Jump");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        currentState_ = state;
    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight_ = !facingRight_;
    }
}
