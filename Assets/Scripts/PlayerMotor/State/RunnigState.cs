using UnityEngine;

public class RunnigState : BaseState
{
    
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
            // motor.ChangeState(GetComponent<JumpingState>());
        }  
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1.0f;
        m.z = motor.baseRunSpeed;

        return m;
    }

    
}

