using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    // Variáveis uteis para a parte de Inputs
    [SerializeField] private int contControle, predioControle;  // contControle para W e S && predioControle para A e D
    private List<int> andaresSelecionados;  // Quais os andares selecionados com KeyCode.Space
    private GameObject cursorTeste; // um cursor somente pra deixar o processo da troca mais visível, não necessariamente usaremos um cursor.

    [SerializeField] private GameManager gm;

    void Awake()
    {
        contControle = 0;
        predioControle = 0;

        andaresSelecionados = new List<int>();

        cursorTeste = GameObject.Find("Cursor Teste");
    }

    void Update()
    {
        inputs();
    }

    private void inputs()
    {
        // move pra cima e se tiver no último bloco retorna pro primeiro.
        if (Input.GetKeyDown(KeyCode.W))
        {
            contControle = (contControle + 1) % gm.numeroAndares(); // Melhoria na fórmula de fila circular para cima

            moveCursorTeste();
        }

        // move pra baixo e se tiver no primeiro bloco do predio, vai para o último.
        if (Input.GetKeyDown(KeyCode.S))
        {
            contControle = (contControle - 1 + gm.numeroAndares()) % gm.numeroAndares();    // Melhoria na fórmula de fila circular para baixo

            moveCursorTeste();
        }

        // seleciona os blocos para operar a troca, então se a lista completar dois objs executa a troca.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            andaresSelecionados.Add(contControle);  // Sempre adiciona um objeto ao apertar espaço

            if (andaresSelecionados.Count >= 2) // Se for o segundo objeto selecionado, realiza algo a mais
            {
                if (andaresSelecionados[0] != andaresSelecionados[1]) gm.predio[predioControle].operarTroca(andaresSelecionados[0], andaresSelecionados[1]);    // Realiza a troca

                andaresSelecionados.Clear();    // Sempre limpa, ou depois da troca, ou se forem iguais
            }
        }

        if (Input.GetKeyDown(KeyCode.D))    // Movimento entre prédios
        {
            predioControle = (predioControle + 1) % gm.predio.Count;

            andaresSelecionados.Clear();    // Se há movimento para outro prédio, qualquer seleção feita no anterior é desfeita
        }

        if (Input.GetKeyDown(KeyCode.A)) // Movimento entre prédios
        {
            predioControle = (predioControle - 1 + gm.predio.Count) % gm.predio.Count;

            andaresSelecionados.Clear();    // Se há movimento para outro prédio, qualquer seleção feita no anterior é desfeita
        }
    }

    private void resetCursorTeste()
    {
        cursorTeste.transform.position = new Vector3(-4f, -4f);
    }

    // move o cursor conforme o ContControle.
    private void moveCursorTeste()
    {
        cursorTeste.transform.position = new Vector3(cursorTeste.transform.position.x, gm.predio[predioControle].estabelecimentos[contControle].transform.position.y, 0);
    }

    public void zerarComponentes()
    {
        contControle = 0;
        predioControle = 0;
        andaresSelecionados.Clear();

        resetCursorTeste();
    }
}
