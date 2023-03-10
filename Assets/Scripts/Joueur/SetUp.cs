using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SetUp : NetworkBehaviour
{
    [SerializeField] Behaviour[] composantsADesactiver;

    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
        {
            //on desactive le controle des autres joueurs
            for(int i = 0; i < composantsADesactiver.Length; i++)
            {
                composantsADesactiver[i].enabled = false;
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
