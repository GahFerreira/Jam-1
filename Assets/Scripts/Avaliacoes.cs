using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avaliacoes : MonoBehaviour
{
    // Variáveis para o funcionamento geral do jogo
    public float avaliacao;
    [SerializeField] private float somaAvaliacoes;
    [SerializeField] private int quantidadeAvaliacoes;

    [SerializeField] private TextoTeste text;

    void Awake()
    {
        avaliacao = somaAvaliacoes = 0f;
        quantidadeAvaliacoes = 0;
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

    public int atualizarSituacaoJogador(bool faseTerminou)   // Função pra checar se o jogador perdeu (ainda estou testando a melhor maneira de implementar, podemos discutir depois ela melhor)
    {
        if (quantidadeAvaliacoes >= 10 && avaliacao < 1f)
        {
            print("Sua nota despencou e seu prédio está em ruínas. Você perdeu. {nota < 1 && qtAv >= 10}");
            GameObject[] cliente = GameObject.FindGameObjectsWithTag("Cliente");

            foreach (GameObject go in cliente) Destroy(go);

            text.setSituacao(-1);

            return -1;
        }

        else if (faseTerminou == true && avaliacao < 3f)
        {
            print("Sua nota não alcançou o esperado. Você perdeu. {nota < 3 && faseFim == true}");
            GameObject[] cliente = GameObject.FindGameObjectsWithTag("Cliente");

            foreach (GameObject go in cliente) Destroy(go);

            text.setSituacao(-2);

            return -2;
        }

        else if (quantidadeAvaliacoes >= 1 && avaliacao < 3f)
        {
            print("Cuidado! {nota < 3f}");

            text.setSituacao(1);

            return 1;
        }

        else text.setSituacao(0);

        return 0;
    }
}
