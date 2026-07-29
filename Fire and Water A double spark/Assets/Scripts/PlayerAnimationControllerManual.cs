using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    
    private readonly int jumpHash = Animator.StringToHash("Jump");
    private readonly int runHash = Animator.StringToHash("Run");
    private readonly int downHash = Animator.StringToHash("Down");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Run всегда true (кроме случаев когда нажат S)
        animator.SetBool(runHash, true);

        // Если нажал W - Jump на 0.5 секунды
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool(jumpHash, true);
            StartCoroutine(ResetJump());
        }

        // Если нажал S - Down на 0.5 секунды
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool(downHash, true);
            animator.SetBool(runHash, false); // Run выключаем
            StartCoroutine(ResetDown());
        }
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(jumpHash, false);
    }

    private IEnumerator ResetDown()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(downHash, false);
        animator.SetBool(runHash, true); // Run включаем обратно
    }
}