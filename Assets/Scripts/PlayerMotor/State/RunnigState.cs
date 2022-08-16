using UnityEngine;

public class RunnigState : BaseState
{
    [SerializeField] private float speedModifier = 30.0f;
    public override void Construct()
    {
        motor.verticalVelocity = 0;
    }
    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLine(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLine(1);
        }

        if (InputManager.Instance.SwipeUp && motor.isGrounded)
        {
            motor.ChangeState(GetComponent<JumpingState>());
        }

        if (!motor.isGrounded)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }

        if(InputManager.Instance.SwipeDown)
            motor.ChangeState(GetComponent<SlidingState>());
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1.0f;
        m.z = motor.baseRunSpeed + GameStats.Instance.score / speedModifier;

        return m;
    }

    
}

