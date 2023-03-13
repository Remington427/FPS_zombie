using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    public int gunDamage = 1;
    public float weaponRange = 200f;
    public float hitForce = 100f;
    private Camera fpsCam;
    public float fireRate = 0.25f;
    private float nextFire;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            //
            nextFire = Time.time + fireRate;

            Debug.DrawRay(fpsCam.transform.position, Vector3.forward * 10, Color.red);
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {
                print(hit.transform.name + " a ete touche !");

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);

                    if(hit.collider.gameObject.GetComponent<ReceiveAction>() != null)
                    {
                        hit.collider.gameObject.GetComponent<ReceiveAction>().GetDamage(gunDamage);
                    }
                }
            }
        }
    }
}
