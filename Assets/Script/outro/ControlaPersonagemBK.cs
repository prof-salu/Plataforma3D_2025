using UnityEngine;

public class ControlaPersonagemBK : MonoBehaviour
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");
        if (groundCheck == null)
        {
            Debug.LogError("Objeto 'GroundCheck' não encontrado como filho deste GameObject!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        // Verificar se está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundedCheckRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Obter input de movimento diretamente da classe Input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Determinar velocidade atual com base na tecla Shift esquerda
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : walkSpeed;
        animator.SetFloat("Speed", moveDirection.magnitude * currentSpeed);

        // Movimentação
        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        // Pulo ao pressionar a barra de espaço
        if (isGrounded && Input.GetButtonDown("Jump"))
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