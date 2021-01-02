using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    [SerializeField] private string id;

    [SerializeField] private float velocidade;
    [SerializeField] private List<float> estrelasAcerto, estrelasErro, estrelasEspecial;
    public char idEstabelecimentoPredileto;

    private Avaliacoes av;

    void Start()
    {
        Destroy(gameObject, 15f);

        av = GameObject.FindWithTag("GameController").GetComponent<Avaliacoes>();
    }

    void Update()
    {
        andar();
    }

    void andar()
    {
        transform.position = transform.position + new Vector3(velocidade * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter2D (Collider2D other) // Algoritmo para quando o cliente entrar no prédio
    {
        GetComponent<BoxCollider2D>().isTrigger = false;

        if (other.tag == "Estabelecimento")
        {
            float avaliacaoFinal; // Facilidade de manipulação da avaliação

            Estabelecimento est = other.GetComponent<Estabelecimento>(); // Características do estabelecimento

            if (idEstabelecimentoPredileto == est.id)   // Se acertou o estabelecimento
            {
                avaliacaoFinal = estrelasAcerto[Random.Range(0, estrelasAcerto.Count)];    // Ganha o bônus de acerto

                if (id == "Cri" && est.id == 'R') avaliacaoFinal += 10f;    // O Crítico adora restaurantes

                switch (id) // Casos especiais que buffam ou nerfam o estabelecimento
                {
                    case "Cel": est.variacaoEspecial += 0.5f; break;    // Celebridade buffa o estabelecimento
                    case "Fis": est.variacaoEspecial -= 0.5f; break;    // Fiscal nerfa o estabelecimento
                }
            }

            else // Se errou o estabelecimento
            {
                if (id == "NGo" && est.id == 'S') // Caso especial do Nerd Gordo no Spa
                {
                    avaliacaoFinal = estrelasEspecial[Random.Range(0, estrelasEspecial.Count)];
                }

                else avaliacaoFinal = estrelasErro[Random.Range(0, estrelasErro.Count)];   // Perde nota pelo erro
            }

            if (id == "Mar")    // Caso especial do Maratonista
            {
                if (av.avaliacao > 4) avaliacaoFinal -= 0.5f;
                else if (av.avaliacao < 2) avaliacaoFinal += 0.5f;
            }

            avaliacaoFinal += est.variacaoEspecial; // Adiciona um buff / nerf do estabelecimento

            if (avaliacaoFinal < 0) avaliacaoFinal = 0f;    // Caso a nota, por algum motivo, der menor que 0, ela fica como 0.
            else if (avaliacaoFinal > 5) avaliacaoFinal = 5f;   // Ou 5, se der maior que 5.

            av.adicionarAvaliacao(avaliacaoFinal);

            //Debug.Log("Avaliação: " + avaliacaoFinal);
        }
    }
}
