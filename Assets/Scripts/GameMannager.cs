using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    
    // lista para armazenar cada estabelecimento do primeiro e segundo predio.
    public List<GameObject> Listpredio1;
    public List<GameObject> Listpredio2;

    // variaveis uteis para a parte de inputs
    private int ContControle; // seleciona a posição na lista q vc vai pegar o obj.
    private List<GameObject> SelectedObj = new List<GameObject>(); // guarda os dois obj q serão feito a troca de posicao.
    private GameObject cursortest; // um cursor somente pra deixar o processo da troca mais visivel, não necessariamente usaremos um cursor.

  
    void Start()
    {
        ContControle = 0;
        cursortest = GameObject.Find("Cursor teste");

        Listpredio1 = Iniciarlista(GameObject.Find("Predio1").transform);
        
    }


    void Update()
    {
        
        inputs(); //to chamando pela update, pq pela fixed update não tava ficando legal.

    }


    private void inputs()
    {
        // move pra cima e se tiver no ultimo bloco retorna pro primeiro.
        if (Input.GetKeyDown(KeyCode.W) && ContControle < Listpredio1.Count - 1)
        {
            ContControle++;
            movecursorteste();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ContControle = 0;
            movecursorteste();
        }

        //move pra baixo e se tiver no primeiro bloco do predio, vai para o ultimo.
        if (Input.GetKeyDown(KeyCode.S) && ContControle > 0)
        {
            ContControle--;
            movecursorteste();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            ContControle = Listpredio1.Count - 1;
            movecursorteste();
        }

        //seleciona os blocos para operar a troca, então se a lista completar dois objs executa a troca.
        if (Input.GetKeyDown(KeyCode.Space) && SelectedObj.Count < 2) SelectedObj.Add(Listpredio1[ContControle]);
        else if (SelectedObj.Count >= 2) OperarTroca();

    }

    // procura os elementos filhos do predio1 ou 2 e armazena na lista.
    private List<GameObject> Iniciarlista(Transform predio)
    {
        List<GameObject> templist =  new List<GameObject>();
        //Transform[] childs = predio.GetComponentInChildren<Transform>();

        foreach(Transform child in predio)
        {
            templist.Add(child.gameObject);
        }
        
        return templist;

    }

    private void OperarTroca()
    {
        Transform temptransform = transform; // o temptransform é igual a transform pois ele precisa de uma iniciação, e como a classe Transform é protegida não consigo da new Transform().
        temptransform.position = SelectedObj[0].GetComponent<Transform>().position; 

        // chama o metodo de troca de posicao de cada bloco.
        SelectedObj[0].GetComponent<Estabelecimentos>().TrocarPos(SelectedObj[1].GetComponent<Transform>());
        SelectedObj[1].GetComponent<Estabelecimentos>().TrocarPos(temptransform);

        // criei essas variaveis so para as linhas a seguir n ficarem muito poluidas.
        int indexS1 = Listpredio1.IndexOf(SelectedObj[0]);
        int indexS2 = Listpredio1.IndexOf(SelectedObj[1]);

        // reorganiza a ordem da fila.
        Listpredio1[indexS1] = SelectedObj[1];
        Listpredio1[indexS2] = SelectedObj[0];

        //limpa a lista de objetos selecionados, pronto para a proxima seleção.
        SelectedObj.Clear();
    }

    // move o cursor conforme o ContControle.
    private void movecursorteste()
    {
        cursortest.transform.position = new Vector3(cursortest.transform.position.x, Listpredio1[ContControle].transform.position.y, 0);
    }
}
