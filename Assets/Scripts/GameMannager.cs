using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    //predios
    private GameObject predio1;
    private GameObject predio2;

    // lista para armazenar cada estabelecimento do primeiro e segundo predio
    public List<GameObject> Listpredio1;
    public List<GameObject> Listpredio2;



    // Start is called before the first frame update
    void Start()
    {

        predio1 = GameObject.Find("Predio1");
        Listpredio1 = Iniciarlista(predio1.transform);

        predio2 = GameObject.Find("Predio2");
        Listpredio2 = Iniciarlista(predio2.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // procura os elementos filhos do predio1 ou 2 e armazena na lista
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


}
