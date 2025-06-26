using UnityEngine;

public class PersonagemStateMachine : MonoBehaviour
{
    // Enumeração para os diferentes estados do personagem
    public enum EstadoDoPersonagem
    {
        Idle,
        Andando,
        Correndo,
        Pulando,
        Caindo,
        Atacando,
        Bloqueando,
    }

    // Variável para armazenar o estado atual do personagem
    public EstadoDoPersonagem EstadoAtual { get; private set; }

    private CharacterController controller;
    private Animator animator;

    void Awake()
    {
        // Obtém a referência ao componente Animator no mesmo GameObject
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        // Define o estado inicial do personagem
        EstadoAtual = EstadoDoPersonagem.Idle;
    }

    void Update()
    {
        // Lógica para transição entre os estados
        switch (EstadoAtual)
        {
            case EstadoDoPersonagem.Idle:
                EstadoIdle();
                break;
            case EstadoDoPersonagem.Andando:
                EstadoAndando();
                break;
            case EstadoDoPersonagem.Correndo:
                EstadoCorrendo();
                break;
            case EstadoDoPersonagem.Pulando:
                EstadoPulando();
                break;
            case EstadoDoPersonagem.Caindo:
                EstadoCaindo();
                break;
            case EstadoDoPersonagem.Atacando:
                EstadoAtacando();
                break;
            case EstadoDoPersonagem.Bloqueando:
                EstadoBloqueando();
                break;
            // Adicione outros casos para os estados adicionais
            default:
                break;
        }
    }

    // Métodos para a lógica de cada estado
    void EstadoIdle()
    {
        // Lógica quando o personagem está parado
        // Exemplo: Verificar por input de movimento para mudar para o estado Andando
    }

    void EstadoAndando()
    {
        // Lógica quando o personagem está andando
        // Exemplo: Verificar por input de parada para mudar para o estado Idle,
        // ou input de corrida para mudar para o estado Correndo
    }

    void EstadoCorrendo()
    {
        // Lógica quando o personagem está correndo
        // Exemplo: Verificar por input de parada ou andar para mudar para os estados correspondentes
    }

    void EstadoPulando()
    {
        // Lógica quando o personagem está pulando
        // Exemplo: Verificar se o personagem tocou o chão para mudar para Idle ou Andando
    }

    void EstadoCaindo()
    {
        // Lógica quando o personagem está caindo
        // Exemplo: Verificar se o personagem tocou o chão para mudar para Idle ou Andando
    }

    void EstadoAtacando()
    {
        // Lógica quando o personagem está atacando
        // Exemplo: Verificar o fim da animação de ataque para voltar ao estado anterior
    }

    void EstadoBloqueando()
    {
        // Lógica quando o personagem está bloqueando
        // Exemplo: Verificar o fim da animação de bloqueio para voltar ao estado anterior
    }

    // Método para mudar o estado do personagem
    public void MudarEstado(EstadoDoPersonagem novoEstado)
    {
        EstadoAtual = novoEstado;
        // Opcional: Você pode adicionar lógica aqui para acionar animações específicas
        // com base no novo estado, usando o 'anim.Play("NomeDaAnimacao");'
    }
}

