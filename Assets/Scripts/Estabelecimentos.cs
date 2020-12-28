using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estabelecimentos : MonoBehaviour
{

    private int andar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // gets and sets

    public void SetAndar(int andar)
    {
        this.andar = andar;
    }
    
    public int GetAndar()
    {
        return andar;
    }

}
