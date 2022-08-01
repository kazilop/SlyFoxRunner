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

    private BaseState state;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        state = GetComponent<RunnigState>();
        state.Construct();
    }

    private void Update()
    {
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        isGrounded = controller.isGrounded;

        moveVector = state.ProcessMotion();

        state.Transition();

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
}
