using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator anim;
    private ControlaPersonagem controle;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        controle = GetComponent<ControlaPersonagem>();
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetFloat("VelocidadeAtual", moveDirection.magnitude * currentSpeed);
    }
}
