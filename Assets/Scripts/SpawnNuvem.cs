using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNuvem : MonoBehaviour
{

    [SerializeField] private List<GameObject> nuvens;
    [SerializeField] private float tempoAteProximaNuvem;


    // Start is called before the first frame update
    void Start()
    {
        spawnNuvem();
    }

    // Update is called once per frame
    void Update()
    {
        tempoAteProximaNuvem -= Time.deltaTime;
        if (tempoAteProximaNuvem <= 0) spawnNuvem();
    }

    void spawnNuvem()
    {
        tempoAteProximaNuvem = Random.Range(5, 7);

        int altura = Random.Range(-1, 4);
        int obj = Random.Range(0, nuvens.Count-1);
        float randomVelocidade = Random.Range(2, 4);
        
        GameObject nuvem = Instantiate(nuvens[obj], transform);
        nuvem.transform.position = new Vector3(-22, altura, 0);
        nuvem.GetComponent<Rigidbody2D>().velocity = new Vector2(randomVelocidade, 0); //preferi colocar rigidbodys nas nuvens para poder fazelas movelas, sem a necessidade de criar um script so pra essa função
        Destroy(nuvem, 15f); //valor teste

    }
}
