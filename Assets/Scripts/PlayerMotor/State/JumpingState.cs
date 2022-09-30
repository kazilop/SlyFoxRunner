using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce = 7.0f;
    public AudioClip jumpClip;

    
    public override void Construct()
    {
        motor.animator?.SetTrigger("Jump");
        motor.verticalVelocity = jumpForce;
        PlayJumpSound();
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

    private void PlayJumpSound()
    {
        playerAudio.clip = jumpClip;
        playerAudio.Play();
    }

    public override void Transition()
    {
        if(motor.verticalVelocity < 0)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
    }
}
