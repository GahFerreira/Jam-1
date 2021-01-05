using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCliente : MonoBehaviour
{
    private float tempoDesdeUltimoSpawn;
    private List<float> possiveisAlturasDeSpawn;    // Lista de alturas possíveis para spawnar um cliente (uma por andar)
    [SerializeField] private char lado;
    [SerializeField] private List<string> clientesAInstanciarNoDia;

    [SerializeField] private List<GameObject> clienteParaInstanciar;    // Lista de prefabs de clientes

    private GameManager gm;
    [SerializeField] private Predio predio;

    void Awake()
    {
        tempoDesdeUltimoSpawn = 0f;

        possiveisAlturasDeSpawn = new List<float>();

        clientesAInstanciarNoDia = new List<string>();
    }

    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();  // Pegar o Script do GameManager

        setarAlturas(); // Pega a lista de alturas possíveis para spawnar um cliente (uma por andar)
    }

    void Update()
    {
        tentativaInstanciacao();
    }

    void tentativaInstanciacao()    // Aqui é uma 'tentativa' de spawnar um cliente, que roda a cada frame
    {
        // Gera-se 1 inteiro entre 3000. Começa em 20 pq depois de um spawn, tem-se 20% do tempoMaxAteOProximoSpawn de cooldown, pelo menos, até spawnar o proximo cliente (isso aqui deve ser balanceado)
        if (tempoDesdeUltimoSpawn >= gm.tempoMaxAteOProximoSpawn || (tempoDesdeUltimoSpawn / gm.tempoMaxAteOProximoSpawn) * 100 > Random.Range(20, 3020))    // Ou gera um cliente depois do tempo maximo ou por sorte, no rng (isso aqui deve ser balanceado)
        {
            float altura = possiveisAlturasDeSpawn[System.Math.Max(Random.Range(0, possiveisAlturasDeSpawn.Count + 2) - 2, 0)]; // Escolha das alturas (a do chão é sempre 3x mais provável que qualquer outra)
            float distanciaHorizontal = transform.position.x + 14f;

            if (lado == 'E') distanciaHorizontal *= -1;

            GameObject clienteInstanciado = Instantiate(clienteParaInstanciar[idParaIndice(clientesAInstanciarNoDia[Random.Range(0, clientesAInstanciarNoDia.Count)])], new Vector3(distanciaHorizontal, altura, 0), Quaternion.identity); // Instancia um cliente

            clienteInstanciado.GetComponent<Cliente>().idEstabelecimentoPredileto = predio.estabelecimentos[Random.Range(0, predio.estabelecimentos.Count)].GetComponent<Estabelecimento>().id; // Seta o estabelecimento predileto dele

            switch (clienteInstanciado.GetComponent<Cliente>().idEstabelecimentoPredileto)  // Isso aqui é pra mudar a cor do quadrado pq o yuz n mandou a fotinha do cliente dele ainda :c
            {
                case 'A': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.cyan; break;
                case 'F': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.red; break;
                case 'L': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.magenta; break;
                case 'R': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.gray; break;
                case 'S': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.black; break;
            }

            tempoDesdeUltimoSpawn = 0f; // Se spawnar, obviamente tem que setar isso pra 0f
        }

        else tempoDesdeUltimoSpawn += Time.deltaTime;    // Se não spawnar, continua adicionando tempo aqui
    }

    private void setarAlturas() // Função pra setar alturas possíveis de spawn
    {
        possiveisAlturasDeSpawn.Clear();    // Se você for setar novas alturas, as antigas tem que sumir primeiro

        for (int i = 0; i < gm.numeroAndares(); i++)
        {
            possiveisAlturasDeSpawn.Add(transform.position.y - 0.22f + (float)(2.1875 * i));    // Isso aqui vai mudar com a chegada das artes
        }
    }

    public void setarClientesQueSeraoInstanciadosNoDia(int dia)
    {
        clientesAInstanciarNoDia.Clear();

        // Personagens Padrão   (4 de cada são jogados na pool de spawn)
        for (int i = 0; i < 4; i++)
        {
            clientesAInstanciarNoDia.Add("Vel");
            clientesAInstanciarNoDia.Add("Gui");
            clientesAInstanciarNoDia.Add("CEO");
        }

        if (dia >= 2)
        {
            for (int i = 0; i < 3; i++) // 3 de cada são jogados na pool de spawn
            {
                clientesAInstanciarNoDia.Add("NGo");
                clientesAInstanciarNoDia.Add("Cri");
            }
        }

        if (dia >= 3)   // Apenas 1 de cada é adicionado na pool de spawn
        {
            clientesAInstanciarNoDia.Add("Cel");
            clientesAInstanciarNoDia.Add("Fis");
        }

        if (dia >= 4)
        {
            for (int i = 0; i < 2; i++)
            {
                clientesAInstanciarNoDia.Add("Mar");
            }
        }
    }

    private int idParaIndice(string id)
    {
        switch (id)
        {
            case "Cel": return 0;
            case "CEO": return 1;
            case "Cri": return 2;
            case "Fis": return 3;
            case "Gui": return 4;
            case "Mar": return 5;
            case "NGo": return 6;
            case "Vel": return 7;
        }

        return -1;  //Caso base: retorna -1 (a intenção é causar um erro, se realmente houver um bug aqui).
    }
}
