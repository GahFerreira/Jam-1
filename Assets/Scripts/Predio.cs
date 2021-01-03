using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predio : MonoBehaviour
{
    [SerializeField] private List<GameObject> estabelecimentosParaInstanciar;   // Lista de Prefabs de Estabelecimentos
    public List<GameObject> estabelecimentos;   // Os estabelecimentos que, de fato, fazem parte do prédio
    private List<char> estabelecimentosParaAdicionar;   // Lista de id dos estabelecimentos que serão adicionados em algum momento
    [SerializeField] private GameObject fumacinha;

    private GameManager gm;

    void Awake()
    {
        estabelecimentosParaAdicionar = new List<char>();   // Aqui a lista de id é criada
        estabelecimentosParaAdicionar.Add('A'); // Os estabelecimentos que serão posteriormente adicionados ao prédio são adicionados aqui primeiro
        estabelecimentosParaAdicionar.Add('F'); // Essa lista existe para que não haja dois estabelecimentos iguais no mesmo prédio
        estabelecimentosParaAdicionar.Add('L'); // Pois quando ele for adicionado ao prédio de fato, ele sai dessa lista
        estabelecimentosParaAdicionar.Add('R'); // O Spa, no entanto, é caso especial, e é sempre adicionado no dia 2
    }

    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        InstanciarPredio(); 
    }

    public void InstanciarPredio () // Quando o Prefab Prédio é instanciado (este script está ligado nele), os estabelecimentos dele são instanciados por aqui
    {
        gm.predio.Add(this);

        for (int i = 0; i < gm.numeroAndares(); i++)    // Instancia-se estabelecimentos de acordo com o dia (ou fase) atual
        {
            int escolhaAleatoria = Random.Range(0, estabelecimentosParaAdicionar.Count);    // Escolhe-se aleatoriamente os estabelecimentos que vai adicionar

            GameObject gO = Instantiate(estabelecimentosParaInstanciar[idParaIndice(estabelecimentosParaAdicionar[escolhaAleatoria])], transform);  // Aqui instancia-se um estabelecimento, já colocando ele como filho do Prédio

            gO.transform.position += new Vector3(0, (float) (estabelecimentos.Count * 2.1875), 0);  // Aqui corrigi-se a altura do estabelecimento instanciado, baseado em quantos andares já tem

            estabelecimentos.Add(gO);   // Aqui adiciona-se o estabelecimento instanciado a uma lista que representa o prédio

            estabelecimentosParaAdicionar.RemoveAt(escolhaAleatoria);   // Aqui remove o id do estabelecimento instanciado da lista de estabelecimentos a se adicionar no futuro
        }
    }

    public void adicionarAndar()    // Função para, possivelmente, adicionar um novo andar (usado quando clicar em 'Continuar' no menu)
    {
        if (gm.getDia() == 2)   // Se for o segundo dia, adiciona-se o Spa
        {
            GameObject gO = Instantiate(estabelecimentosParaInstanciar[idParaIndice('S')], transform);

            gO.transform.position += new Vector3(0, (float)(estabelecimentos.Count * 2.1875), 0);

            estabelecimentos.Add(gO);
        }

        else if (estabelecimentosParaAdicionar.Count > 0)   // Em outros dias, adiciona-se o estabelecimento que falta (sabe-se qual é, pois tem guardado em estabelecimentosParaAdicionar)
        {
            int escolhaAleatoria = Random.Range(0, estabelecimentosParaAdicionar.Count);

            GameObject gO = Instantiate(estabelecimentosParaInstanciar[idParaIndice(estabelecimentosParaAdicionar[escolhaAleatoria])], transform);

            gO.transform.position += new Vector3(0, (float)(estabelecimentos.Count * 2.1875), 0);

            estabelecimentos.Add(gO);
        }
    }

    public void removerAndar()  // Função para remover um estabelecimento
    {
        estabelecimentosParaAdicionar.Add(estabelecimentos[estabelecimentos.Count - 1].GetComponent<Estabelecimento>().id); // Daí coloca de novo o id do estabelecimento removido na lista, para adicioná-lo depois de novo

        estabelecimentos.RemoveAt(estabelecimentos.Count - 1);
    }

    public void randomizarAndares() // Essa função, basicamente, randomiza a ordem dos andares (ainda não usei ela, mas a ideia é randomizar sempre que for iniciar um novo nível)
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

    public void operarTroca(int andar1, int andar2) // Função simples pra troca de andares
    {
        GameObject f1 = Instantiate(fumacinha, estabelecimentos[andar1].transform);
        GameObject f2 = Instantiate(fumacinha, estabelecimentos[andar2].transform);

        Vector3 aux = estabelecimentos[andar1].transform.position;
        estabelecimentos[andar1].transform.position = estabelecimentos[andar2].transform.position;
        estabelecimentos[andar2].transform.position = aux;

        GameObject aux2 = estabelecimentos[andar1];
        estabelecimentos[andar1] = estabelecimentos[andar2];
        estabelecimentos[andar2] = aux2;

        Destroy(f1, 1f);
        Destroy(f2, 1f);

    }

    private int idParaIndice(char id)   // Essa função associa um id a um índice
    {                                   // Esse id é o que está na lista de estabelecimentos para adicionar depois, e o índice é o que está na lista de estabelecimentos para instanciar (prefabs)
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
