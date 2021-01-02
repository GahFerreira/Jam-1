using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avaliacoes : MonoBehaviour
{
    // Variáveis para o funcionamento geral do jogo
    public float avaliacao, DELETADEPOIS;   // DELETADEPOIS é só pra testar o sistema de derrota
    [SerializeField] private float somaAvaliacoes;
    [SerializeField] private int quantidadeAvaliacoes;

    void Awake()
    {
        DELETADEPOIS = avaliacao = somaAvaliacoes = 0f;
        quantidadeAvaliacoes = 0;
    }

    void Update()
    {
        situacaoJogador(false); // Checa pra ver se o jogador perdeu a cada frame
    }

    public void adicionarAvaliacao(float av)    // Função para que, quando o cliente colidir com um estabelecimento, ele adicione sua avaliação
    {
        somaAvaliacoes += av;
        quantidadeAvaliacoes += 1;

        avaliacao = somaAvaliacoes / quantidadeAvaliacoes;
    }

    public void zerarAvaliacoes()   // Função para zerar avaliações (usadas no início do jogo e das fases)
    {
        avaliacao = somaAvaliacoes = 0f;
        quantidadeAvaliacoes = 0;
    }

    public int situacaoJogador(bool faseTerminou)   // Função pra checar se o jogador perdeu (ainda estou testando a melhor maneira de implementar, podemos discutir depois ela melhor)
    {
        if ((avaliacao < 1 && quantidadeAvaliacoes >= 10) || avaliacao < 3 && faseTerminou == true)
        {
            print("Sua nota despencou e seu prédio está em ruínas. Você perdeu.");
            GameObject[] cliente = GameObject.FindGameObjectsWithTag("Cliente");

            foreach (GameObject go in cliente) Destroy(go);

            return -1;
        }

        else if (avaliacao < 3f && (DELETADEPOIS >= 3f || (quantidadeAvaliacoes == 1 && avaliacao != DELETADEPOIS)))
        {
            print("Cuidado! Sua nota está ficando baixa.");
        }

        DELETADEPOIS = avaliacao;

        return 0;
    }
}
