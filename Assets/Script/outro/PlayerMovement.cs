using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    private InputManager inputManager; 

    private void Awake()
    {
        // Obtenha a referência do seu CharacterController 
        controller = GetComponent<CharacterController>();

        // Crie um objeto vazio chamado "GroundCheck" como filho do seu personagem
        groundCheck = transform.Find("GroundCheck"); 
        if (groundCheck == null)
        {
            Debug.LogError("Objeto 'GroundCheck' não encontrado como filho deste GameObject!");
            enabled = false;
            return;
        }
        // Obtenha a referência do seu InputManager 
        inputManager = FindObjectOfType<InputManager>(); 
        if (inputManager == null)
        {
            Debug.LogError("InputManager não encontrado na cena!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        // Verificar se está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundedCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            // Pequena força para garantir que fique "grudado" no chão (gravidade)
            velocity.y = -2f; 
        }

        // Obter input de movimento
        Vector2 moveInput = inputManager.GetMoveInput();
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // Determinar velocidade atual
        currentSpeed = inputManager.IsRunning() ? runSpeed : walkSpeed;

        // Movimentação
        if (moveDirection.magnitude >= 0.1f)
        {
            // Rotacionar o personagem na direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Mover o personagem
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        // Pulo
        if (isGrounded && inputManager.IsJumping())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * Physics.gravity.y * -gravityMultiplier);
        }

        // Aplicar gravidade
        velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Desenhar gizmo para visualização do GroundCheck na Scene View
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundedCheckRadius);
        }
    }
}