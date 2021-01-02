using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int dia;
    public float tempoMaxAteOProximoSpawn;
    private float tempoDecorrido;
    public List<Predio> predio;

    [SerializeField] private List<GameObject> predioParaInstanciar;
    
    [SerializeField] private Avaliacoes av;

    void Awake()
    {
        dia = 0;
        tempoMaxAteOProximoSpawn = 6f;
        tempoDecorrido = 0f;
    }

    void Start()
    {
        proximaFase();
    }

    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido > 15f)
        {
            tempoDecorrido = 0f;

            av.situacaoJogador(true);
            proximaFase();
        }
    }

    public int getDia() { return dia; }

    public void setarDia()
    {
        dia += 1;   // Dia aumenta em 1

        if (dia < 82)   // O último dia, baseado na fómula abaixo, em que o tempo é positivo é o dia 81
        {
            tempoMaxAteOProximoSpawn -= (1 / dia);  // 5 - (1/1 + 1/2 + 1/3 + ... + 1/n) resulta em -infinito, por isso não pode ser usado pra endless
        }

        else if (dia == 1000 || dia == 10000 || dia == 100000 || dia == 1000000) // Aumentando a dificuldade nesses dias
        {
            tempoMaxAteOProximoSpawn *= (9 / 10);
        }

        else
        {
            tempoMaxAteOProximoSpawn -= (1 / (dia * dia)); // (1/(1*1) + 1/(2*2) + 1/(3*3) + ... + 1/(n*n)) converge pra (pi*pi)/6, mas como começa a partir do 82, sempre dá positivo
        }
    }

    public int setarDia(int numeroDoDia)
    {
        if (numeroDoDia <= 0) return -1;   /// Caso de erro (seria bom ter uma tratação posteriormente)

        dia = numeroDoDia;

        tempoMaxAteOProximoSpawn = 6f;

        for (int i = 1; i <= numeroDoDia; i++)
        {
            if (i < 82) tempoMaxAteOProximoSpawn -= 1 / i;

            else if (numeroDoDia == 1000 || numeroDoDia == 10000 || numeroDoDia == 100000 || numeroDoDia == 1000000)    // Aumentando a dificuldade nesses dias
            {
                tempoMaxAteOProximoSpawn *= (9 / 10);
            }

            else
            {
                tempoMaxAteOProximoSpawn -= (1 / (i * i));
            }
        }

        return 0;
    }

    public int numeroAndares()
    {
        if (dia == 4) return 4;

        else return System.Math.Min(dia + 2, 5);
    }

    public void proximaFase()
    {
        setarDia();

        av.zerarAvaliacoes();

        switch (dia)
        {
            case 1:
                {
                    Instantiate(predioParaInstanciar[0], new Vector3(0, -4), Quaternion.identity);

                    break;
                }

            case 4:
                {
                    predio[0].removerAndar();

                    Instantiate(predioParaInstanciar[1], new Vector3(20, -4), Quaternion.identity);

                    break;
                }

            default:
                {
                    foreach (Predio p in predio)
                    {
                        p.adicionarAndar();
                    }

                    break;
                }
        }
    }

    public int escolherFase(int numeroDaFase)
    {
        int possivelRetorno = setarDia(numeroDaFase); /// Caso de erro (seria bom ter uma tratação posteriormente)

        if (possivelRetorno != 0) return possivelRetorno;

        av.zerarAvaliacoes();

        if (dia <= 3) Instantiate(predioParaInstanciar[0], new Vector3(0, -4), Quaternion.identity);

        else
        {
            Instantiate(predioParaInstanciar[0], new Vector3(0, -4), Quaternion.identity);
            Instantiate(predioParaInstanciar[1], new Vector3(20, -4), Quaternion.identity);
        }

        return 0;
    }
}
