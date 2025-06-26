using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Cinemachine Cameras")]
    public CinemachineFreeLook thirdPersonCamera; // Arraste sua CM FreeLook aqui
    public CinemachineVirtualCamera firstPersonCamera; // Arraste sua CM vcam aqui
    private bool isFirstPerson = false; // Estado atual da câmera

    [Header("Movimento")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 500f;
    private float currentSpeed;

    [Header("Pulo")]
    public float jumpHeight = 2f;
    public float gravityMultiplier = 2f;
    public float groundCheckRadius = 0.2f; //Raio do ground check
    public LayerMask groundLayer; //Layer do plano
    private Vector3 velocity; //velocidade de queda
    private bool isGrounded; //verifica se o personagem esta no chao


    private CharacterController controller;
    private InputManager inputManager;
    public Transform groundCheck;//Armazena a posição do groundcheck
    private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        //Busca na cena algum GameObject que possua o Component InputManager
        inputManager = FindObjectOfType<InputManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        //Retorna TRUE caso o groundcheck esteja em contato com a camada selecionada
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        //Obter o input
        Vector3 moveInput = inputManager.GetMoveInput();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.z).normalized;
        
        //Operador ternario
        currentSpeed = inputManager.IsRunning() ? runSpeed : walkSpeed;

        //Movimentacao
        if(moveDirection.magnitude >= 0.1f)
        {
            //Rotacionar o personagem
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);


            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                            targetRotation,
                                                            rotationSpeed * Time.deltaTime);

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        if (inputManager.IsJumping() && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * Physics.gravity.y * -gravityMultiplier);
            animator.SetTrigger("Jump");
        }

        if (inputManager.IsAttacking() && isGrounded) {
            animator.SetTrigger("Attack");
        }

        //Aplicando a gravidade
        velocity.y += Physics.gravity.y * Time.deltaTime * gravityMultiplier;

        //Faz o pulo acontecer
        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("Speed", moveDirection.magnitude * currentSpeed);
        animator.SetBool("IsGrounded", isGrounded);

        if (inputManager.IsChangeCamera())
        {
            ChangeCamera();
        }
    }

    void ChangeCamera()
    {
        isFirstPerson = !isFirstPerson; // Inverte o estado

        if (isFirstPerson)
        {
            // Ativa a câmera em primeira pessoa (maior prioridade)
            firstPersonCamera.Priority = 10;
            thirdPersonCamera.Priority = 5; // Garante que a terceira pessoa tenha menor prioridade
            Debug.Log("Mudando para Primeira Pessoa");
        }
        else
        {
            // Ativa a câmera em terceira pessoa (maior prioridade)
            thirdPersonCamera.Priority = 10;
            firstPersonCamera.Priority = 5; // Garante que a primeira pessoa tenha menor prioridade
            Debug.Log("Mudando para Terceira Pessoa");
        }
    }


    private void OnDrawGizmosSelected()
    {        
        Gizmos.color = isGrounded ? Color.yellow : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
