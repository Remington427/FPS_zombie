using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 1;

    public LayerMask layerMask;

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isAttacking) return;
        //si le masque n'est pas celui d'un joueur
        if((layerMask.value & (1 << other.gameObject.layer)) == 0) return;

        other.GetComponent<ReceiveAction>().GetDamage(damage);


    }
}
