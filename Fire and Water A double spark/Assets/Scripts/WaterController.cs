using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WaterController : MonoBehaviour
{
    [Header("Гравитация")]
    [SerializeField] private float gravity = -20f;

    [Header("Полосы")]
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float laneChangeSpeed = 12f;

    [Header("Управление")]
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    private CharacterController controller;
    private Vector3 velocity;

    private int currentLane = 1;
    private const int minLane = 0;
    private const int maxLane = 2;
    private float startX;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        startX = transform.position.x;
    }

    private void Update()
    {
        HandleInput();
        ApplyGravity();

        float targetX = startX + (currentLane - 1) * laneWidth;
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * laneChangeSpeed);
        float moveX = newX - transform.position.x;

        // По Z персонаж на месте (двигается платформа), по X — смена полосы,
        // по Y — гравитация, чтобы персонаж не проваливался/висел в воздухе.
        controller.Move(new Vector3(moveX, velocity.y * Time.deltaTime, 0f));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(leftKey))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(rightKey))
        {
            MoveRight();
        }
    }


    public void MoveLeft()
    {
        currentLane = Mathf.Max(minLane, currentLane - 1);
    }


    public void MoveRight()
    {
        currentLane = Mathf.Min(maxLane, currentLane + 1);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    public int CurrentLane => currentLane;
}