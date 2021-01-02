using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estabelecimento : MonoBehaviour
{
    public char id;
    public float variacaoEspecial;

    void Awake ()
    {
        variacaoEspecial = 0f;
    }
}
