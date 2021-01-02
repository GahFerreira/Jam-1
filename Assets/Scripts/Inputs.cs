using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    // Variáveis uteis para a parte de Inputs
    [SerializeField] private int contControle, predioControle;
    private List<int> andaresSelecionados;
    private GameObject cursorTeste; // um cursor somente pra deixar o processo da troca mais visível, não necessariamente usaremos um cursor.

    [SerializeField] private GameManager gm;

    void Awake()
    {
        contControle = 0;
        predioControle = 0;

        andaresSelecionados = new List<int>();
    }

    void Start()
    {
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
            contControle = (contControle + 1) % gm.numeroAndares();

            moveCursorTeste();
        }

        // move pra baixo e se tiver no primeiro bloco do predio, vai para o último.
        if (Input.GetKeyDown(KeyCode.S))
        {
            contControle = (contControle - 1 + gm.numeroAndares()) % gm.numeroAndares();

            moveCursorTeste();
        }

        // seleciona os blocos para operar a troca, então se a lista completar dois objs executa a troca.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            andaresSelecionados.Add(contControle);

            if (andaresSelecionados.Count >= 2)
            {
                if (andaresSelecionados[0] != andaresSelecionados[1]) gm.predio[predioControle].operarTroca(andaresSelecionados[0], andaresSelecionados[1]);

                andaresSelecionados.Clear();
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            predioControle = (predioControle + 1) % gm.predio.Count;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            predioControle = (predioControle - 1 + gm.predio.Count) % gm.predio.Count;
        }
    }

    // move o cursor conforme o ContControle.
    private void moveCursorTeste()
    {
        cursorTeste.transform.position = new Vector3(cursorTeste.transform.position.x, gm.predio[predioControle].estabelecimentos[contControle].transform.position.y, 0);
    }
}
