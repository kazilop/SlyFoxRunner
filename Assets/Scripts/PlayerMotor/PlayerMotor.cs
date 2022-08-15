using System;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    [Header("Settings")]
    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;


    public CharacterController controller;
    public Animator animator;

    private BaseState state;

    private bool isPaused;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        state = GetComponent<RunnigState>();

        isPaused = true;

        state.Construct();
    }

    private void Update()
    {
        if(!isPaused)
            UpdateMotor();
    }

    private void UpdateMotor()
    {
        isGrounded = controller.isGrounded;

        moveVector = state.ProcessMotion();

        state.Transition();

        animator?.SetBool("IsGround", isGrounded);
        animator?.SetFloat("Speed", Mathf.Abs(moveVector.z));

        controller.Move(moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        float r = 0.0f;

        if(transform.position.x != (currentLane * distanceInBetweenLanes))
        {
            float deltaToDesirePosition = (currentLane * distanceInBetweenLanes) - transform.position.x;
            r = (deltaToDesirePosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;

            float actualDistance = r * Time.deltaTime;

            if(Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesirePosition))
            {
                r = deltaToDesirePosition * (1 / Time.deltaTime);
            }
        }
        else
        {
            r = 0.0f;
        }

        return r;
    }

    public void ChangeLine(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }

    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;

        if(verticalVelocity < -terminalVelocity)
            verticalVelocity = -terminalVelocity;
    }

    public void PausePlayer()
    {
        isPaused = true;
    }

    public void ResumePlayer()
    {
        isPaused = false;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if(hitLayerName == "Death")
        {
            ChangeState(GetComponent<DeathState>());
        }
    }

    public void RespawnPlayer()
    {
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(GameCamera.Respawn);
    }

    public void ResetPlayer()
    {
        currentLane = 0;
        transform.position = Vector3.zero;
        animator?.SetTrigger("Idle");
        PausePlayer();
        ChangeState(GetComponent<RunnigState>());
       
    }
}
