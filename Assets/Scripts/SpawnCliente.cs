using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCliente : MonoBehaviour
{
    private float tempoDesdeUltimoSpawn;
    private List<float> possiveisAlturasDeSpawn;

    [SerializeField] private List<GameObject> clienteParaInstanciar;

    private GameManager gm;
    [SerializeField] private Predio predio;

    void Awake()
    {
        tempoDesdeUltimoSpawn = 0f;

        possiveisAlturasDeSpawn = new List<float>();
    }

    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        setarAlturas();
    }

    void Update()
    {
        tentativaInstanciacao();
    }

    void tentativaInstanciacao()
    {
        int aleatorio = Random.Range(20, 3020);

        if (tempoDesdeUltimoSpawn >= gm.tempoMaxAteOProximoSpawn || (tempoDesdeUltimoSpawn / gm.tempoMaxAteOProximoSpawn) * 100 > aleatorio)
        {
            float altura = possiveisAlturasDeSpawn[System.Math.Max(Random.Range(0, possiveisAlturasDeSpawn.Count + 2) - 2, 0)]; // Escolha das alturas (a de baixo é sempre 3x mais provável)

            GameObject clienteInstanciado = Instantiate(clienteParaInstanciar[0], new Vector3(-10f, altura, 0), Quaternion.identity);

            clienteInstanciado.GetComponent<Cliente>().idEstabelecimentoPredileto = predio.estabelecimentos[Random.Range(0, predio.estabelecimentos.Count)].GetComponent<Estabelecimento>().id;
            
            switch (clienteInstanciado.GetComponent<Cliente>().idEstabelecimentoPredileto)
            {
                case 'A': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.cyan; break;
                case 'F': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.red; break;
                case 'L': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.magenta; break;
                case 'R': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.gray; break;
                case 'S': clienteInstanciado.GetComponent<SpriteRenderer>().color = Color.black; break;
            }

            tempoDesdeUltimoSpawn = 0f;
        }

        else
        {
            tempoDesdeUltimoSpawn += Time.deltaTime;
        }
    }

    private void setarAlturas()
    {
        possiveisAlturasDeSpawn.Clear();

        for (int i = 0; i < gm.numeroAndares(); i++)
        {
            possiveisAlturasDeSpawn.Add(predio.GetComponent<Transform>().position.y - 0.22f + (float) (2.1875 * i));
        }
    }
}
