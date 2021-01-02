using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predio : MonoBehaviour
{
    [SerializeField] private List<GameObject> estabelecimentosParaInstanciar;
    public List<GameObject> estabelecimentos;
    private List<char> estabelecimentosParaAdicionar;

    private GameManager gm;

    void Awake()
    {
        estabelecimentosParaAdicionar = new List<char>();
        estabelecimentosParaAdicionar.Add('A');
        estabelecimentosParaAdicionar.Add('F');
        estabelecimentosParaAdicionar.Add('L');
        estabelecimentosParaAdicionar.Add('R');
    }

    void Start()
    {
        GameObject gameManager = GameObject.FindWithTag("GameController");

        gm = gameManager.GetComponent<GameManager>();

        InstanciarPredio();
    }

    public void InstanciarPredio ()
    {
        gm.predio.Add(this);

        for (int i = 0; i < gm.numeroAndares(); i++)
        {
            int escolhaAleatoria = Random.Range(0, estabelecimentosParaAdicionar.Count);

            GameObject gO = Instantiate(estabelecimentosParaInstanciar[idParaIndice(estabelecimentosParaAdicionar[escolhaAleatoria])], transform);

            gO.transform.position += new Vector3(0, (float) (estabelecimentos.Count * 2.1875), 0);

            estabelecimentos.Add(gO);

            estabelecimentosParaAdicionar.RemoveAt(escolhaAleatoria);
        }
    }

    public void adicionarAndar()
    {
        if (gm.getDia() == 2)
        {
            GameObject gO = Instantiate(estabelecimentosParaInstanciar[idParaIndice('S')], transform);

            gO.transform.position += new Vector3(0, (float)(estabelecimentos.Count * 2.1875), 0);

            estabelecimentos.Add(gO);
        }

        else if (estabelecimentosParaAdicionar.Count > 0)
        {
            int escolhaAleatoria = Random.Range(0, estabelecimentosParaAdicionar.Count);

            GameObject gO = Instantiate(estabelecimentosParaInstanciar[idParaIndice(estabelecimentosParaAdicionar[escolhaAleatoria])], transform);

            gO.transform.position += new Vector3(0, (float)(estabelecimentos.Count * 2.1875), 0);

            estabelecimentos.Add(gO);
        }
    }

    public void removerAndar()
    {
        estabelecimentosParaAdicionar.Add(estabelecimentos[estabelecimentos.Count - 1].GetComponent<Estabelecimento>().id);

        estabelecimentos.RemoveAt(estabelecimentos.Count - 1);
    }

    public void randomizarAndares()
    {
        int aleatorio;

        for (int i = estabelecimentos.Count; i > 0; i--)
        {
            aleatorio = Random.Range(0, i);

            GameObject aux = estabelecimentos[0];
            estabelecimentos[0] = estabelecimentos[aleatorio];
            estabelecimentos[aleatorio] = aux;
        }
    }

    public void operarTroca(int andar1, int andar2)
    {
        Vector3 aux = estabelecimentos[andar1].transform.position;
        estabelecimentos[andar1].transform.position = estabelecimentos[andar2].transform.position;
        estabelecimentos[andar2].transform.position = aux;

        GameObject aux2 = estabelecimentos[andar1];
        estabelecimentos[andar1] = estabelecimentos[andar2];
        estabelecimentos[andar2] = aux2;
    }

    private int idParaIndice(char id)
    {
        switch (id)
        {
            case 'A': return 0;
            case 'F': return 1;
            case 'L': return 2;
            case 'R': return 3;
            case 'S': return 4;
        }

        return 0; // Caso base: retorna o Arcade (precisa ter isso, senão dá erro).
    }
}
