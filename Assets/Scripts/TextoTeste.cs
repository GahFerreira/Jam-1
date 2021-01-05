using UnityEngine;
using UnityEngine.UI;

public class TextoTeste : MonoBehaviour
{
    public Text txt;

    public GameManager gm;
    public Avaliacoes av;

    private string situacao;

    void Awake()
    {
        situacao = "";
    }

    void Update()
    {
        txt.text = "Dia " + gm.getDia() + "\n" + System.Math.Round(av.avaliacao, 2) + "/5" + "\n" + situacao;
    }

    public void setSituacao(int sit)
    {
        switch (sit)
        {
            case -2: situacao = "Nota não alcançou o alvo. Derrota."; break;

            case -1: situacao = "Nota baixa demais. Derrota."; break;

            case 0: situacao = ""; break;

            case 1: situacao = "Cuidado! Nota abaixo do esperado!"; break;
        }

    }
}
