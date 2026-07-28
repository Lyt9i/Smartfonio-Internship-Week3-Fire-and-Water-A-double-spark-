using UnityEngine;

/// <summary>
/// Огонь — стоит примерно на одном месте по оси Z (мир/платформы движутся
/// навстречу под ним отдельным скриптом). Персонаж отвечает только за
/// вертикальное перемещение: прыжок вверх и приседание/наклон вниз.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FireController : MonoBehaviour
{
    [Header("Гравитация и прыжок")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpForce = 9f;

    [Header("Приседание")]
    [SerializeField] private float duckHeight = 0.9f;
    [SerializeField] private float duckDuration = 0.6f;

    [Header("Управление")]
    [SerializeField] private KeyCode jumpKey = KeyCode.W;
    [SerializeField] private KeyCode duckKey = KeyCode.S;

    private CharacterController controller;
    private Vector3 velocity;

    private float standHeight;
    private Vector3 standCenter;
    private float duckTimer;
    private bool isDucking;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        standHeight = controller.height;
        standCenter = controller.center;
    }

    private void Update()
    {
        HandleInput();
        ApplyGravity();

        // Двигаем только по Y — по X/Z персонаж остаётся на месте,
        // так как вперёд едет платформа, а не он сам.
        controller.Move(new Vector3(0f, velocity.y, 0f) * Time.deltaTime);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(jumpKey) && controller.isGrounded && !isDucking)
        {
            velocity.y = jumpForce;
        }

        if (Input.GetKeyDown(duckKey) && controller.isGrounded && !isDucking)
        {
            StartDuck();
        }

        if (isDucking)
        {
            duckTimer -= Time.deltaTime;
            if (duckTimer <= 0f)
            {
                EndDuck();
            }
        }
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // прижимает к земле, чтобы isGrounded было стабильным
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private void StartDuck()
    {
        isDucking = true;
        duckTimer = duckDuration;
        controller.height = duckHeight;
        controller.center = new Vector3(standCenter.x, duckHeight / 2f, standCenter.z);
    }

    private void EndDuck()
    {
        isDucking = false;
        controller.height = standHeight;
        controller.center = standCenter;
    }
}