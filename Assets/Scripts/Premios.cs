using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Premios : MonoBehaviour
{
    
    public List<GameObject> PremiosWeb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        ControlPuntaje.onLevelChanged += MostrarPremio;
    }

    void OnDisable()
    {
        ControlPuntaje.onLevelChanged += MostrarPremio;
    }

    private void MostrarPremio(int Nivel)
    {
        
        PremiosWeb[0].SetActive(false);
        PremiosWeb[1].SetActive(false);
        PremiosWeb[2].SetActive(false);
        
        if (Nivel >= 5 && Nivel < 10)
        {
            PremiosWeb[0].SetActive(true);
        }

        if (Nivel >= 10 && Nivel < 15)
        {
            PremiosWeb[1].SetActive(true);
        }

        if (Nivel >= 15)
        {
            PremiosWeb[2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
