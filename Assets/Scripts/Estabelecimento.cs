using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estabelecimento : MonoBehaviour
{
    public char id;
    public float variacaoEspecial;  // Para o caso especial de um buff ou nerf naquele estabelecimento específico

    void Awake ()
    {
        variacaoEspecial = 0f;
    }
}
