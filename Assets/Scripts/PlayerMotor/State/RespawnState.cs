using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float VerticalDistance = 25.0f;
    [SerializeField] private float immunityTime = 1.5f;

    private float startTime;
    
    
    public override void Construct()
    {
        startTime = Time.time;

        motor.controller.enabled = false;

        motor.transform.position = new Vector3(0, VerticalDistance,
            motor.transform.position.z);

        motor.controller.enabled = true;
        motor.verticalVelocity = 0;
        motor.currentLane = 0;
        motor.animator?.SetTrigger("Respawn");

        GameManager.Instance.ChangeCamera(GameCamera.Respawn);
    }

    public override Vector3 ProcessMotion()
    {
        motor.ApplyGravity();

        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        /* if (motor.verticalVelocity < 0)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }  */

        if(motor.isGrounded && (Time.time - startTime) > immunityTime)
        {
            motor.ChangeState(GetComponent<RunnigState>());
        }

        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLine(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLine(1);
        }
    }

    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Game);
    }
}
