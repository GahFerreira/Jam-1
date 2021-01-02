using UnityEngine;
using UnityEngine.UI;

public class TextoTeste : MonoBehaviour
{
    public Text txt;

    public GameManager gm;
    public Avaliacoes av;

    void Update()
    {
        txt.text = "Dia " + gm.getDia() + "\n" + System.Math.Round(av.avaliacao, 2) + "/5";
    }
}
