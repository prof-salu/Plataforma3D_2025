using UnityEngine;

public class ControlaPersonagem : MonoBehaviour
{
    [Header("Movimento")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 500f;
    private float currentSpeed;

    [Header("Pulo")]
    public float jumpHeight = 2f;
    public float gravityMultiplier = 2f;
    public float groundedCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;
    private Vector3 velocity;

    private CharacterController controller;
    private Transform groundCheck;
    private Animator animator;

    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        // Verificar se está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundedCheckRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        // Aplica uma pequena velocidade vertical para manter o personagem no chão
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Obter input de movimento diretamente da classe Input
        Vector3 moveInput = input.Mover();
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.z).normalized;

        // Determinar velocidade atual com base na tecla Shift
        if (input.EstaCorrendo())
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        animator.SetFloat("Speed", moveDirection.magnitude * currentSpeed);

        // Movimentação
        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        // Pulo ao pressionar a barra de espaço
        if (isGrounded && input.EstaPulando())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * Physics.gravity.y * -gravityMultiplier);
            animator.SetTrigger("Jump");
        }

        // Aplicar gravidade
        velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundedCheckRadius);
        }
    }
}