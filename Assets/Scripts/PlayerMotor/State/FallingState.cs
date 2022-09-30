using UnityEngine;

public class FallingState : BaseState
{
    public AudioClip jumpClip;
    public override void Construct()
    {
        motor.animator?.SetTrigger("Fall");
        //PlaySound();
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
        if (motor.isGrounded)
        {
            motor.ChangeState(GetComponent<RunnigState>());
        }
    }

    private void PlaySound()
    {
        playerAudio.clip = jumpClip;
        playerAudio.Play();
    }
}
