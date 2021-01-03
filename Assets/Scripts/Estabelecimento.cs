using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estabelecimento : MonoBehaviour
{
    public char id;
    public float variacaoEspecial;  // Para o caso especial de um buff ou nerf naquele estabelecimento específico

    private Animator ani;
    private RaycastHit2D hit;
    [SerializeField]private float distance;

    void Awake ()
    {
        variacaoEspecial = 0f;
    }

    private void Start()
    {
        distance = 3f;
        ani = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
         AniControl();
    }

    private void AniControl()//controla as animações do estabelecimento
    {

        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), distance,LayerMask.GetMask("Clientes"));

        if (hit) ani.SetBool("Aberto", true);
        else ani.SetBool("Aberto", false);
    }
}
