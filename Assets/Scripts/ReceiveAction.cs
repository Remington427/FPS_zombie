using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveAction : MonoBehaviour
{
    public int maxHitPoint = 5;
    public int hitPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        hitPoint = maxHitPoint;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(int damage)
    {
        hitPoint -= damage;

        if(hitPoint < 1)
        {
            GetFucked();
        }

    }

    public void GetFucked()
    {
        Destroy(gameObject);
    }
}
