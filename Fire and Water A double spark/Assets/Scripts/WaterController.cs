using UnityEngine;

/// <summary>
/// Вода — стоит примерно на одном месте по оси Z (мир/платформы движутся
/// навстречу под ней отдельным скриптом). Персонаж отвечает только за
/// горизонтальное перемещение: смену трёх полос влево/вправо.
/// </summary>
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

    // 0 = левая полоса, 1 = центр, 2 = правая полоса
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
            currentLane = Mathf.Max(minLane, currentLane - 1);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            currentLane = Mathf.Min(maxLane, currentLane + 1);
        }
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