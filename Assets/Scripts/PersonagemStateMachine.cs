using UnityEngine;

public class PersonagemStateMachine : MonoBehaviour
{
    // Enumera��o para os diferentes estados do personagem
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

    // Vari�vel para armazenar o estado atual do personagem
    public EstadoDoPersonagem EstadoAtual { get; private set; }

    private CharacterController controller;
    private Animator animator;

    void Awake()
    {
        // Obt�m a refer�ncia ao componente Animator no mesmo GameObject
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        // Define o estado inicial do personagem
        EstadoAtual = EstadoDoPersonagem.Idle;
    }

    void Update()
    {
        // L�gica para transi��o entre os estados
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

    // M�todos para a l�gica de cada estado
    void EstadoIdle()
    {
        // L�gica quando o personagem est� parado
        // Exemplo: Verificar por input de movimento para mudar para o estado Andando
    }

    void EstadoAndando()
    {
        // L�gica quando o personagem est� andando
        // Exemplo: Verificar por input de parada para mudar para o estado Idle,
        // ou input de corrida para mudar para o estado Correndo
    }

    void EstadoCorrendo()
    {
        // L�gica quando o personagem est� correndo
        // Exemplo: Verificar por input de parada ou andar para mudar para os estados correspondentes
    }

    void EstadoPulando()
    {
        // L�gica quando o personagem est� pulando
        // Exemplo: Verificar se o personagem tocou o ch�o para mudar para Idle ou Andando
    }

    void EstadoCaindo()
    {
        // L�gica quando o personagem est� caindo
        // Exemplo: Verificar se o personagem tocou o ch�o para mudar para Idle ou Andando
    }

    void EstadoAtacando()
    {
        // L�gica quando o personagem est� atacando
        // Exemplo: Verificar o fim da anima��o de ataque para voltar ao estado anterior
    }

    void EstadoBloqueando()
    {
        // L�gica quando o personagem est� bloqueando
        // Exemplo: Verificar o fim da anima��o de bloqueio para voltar ao estado anterior
    }

    // M�todo para mudar o estado do personagem
    public void MudarEstado(EstadoDoPersonagem novoEstado)
    {
        EstadoAtual = novoEstado;
        // Opcional: Voc� pode adicionar l�gica aqui para acionar anima��es espec�ficas
        // com base no novo estado, usando o 'anim.Play("NomeDaAnimacao");'
    }
}

