using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Moteur))]
public class Controles : MonoBehaviour
{
    [SerializeField] private float vitesse = 3f;
    [SerializeField] private float sensibilite = 5f;

    private Moteur moteur;

    // Start is called before the first frame update
    void Start()
    {
        moteur = GetComponent<Moteur>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouvements du joueur
        float deplacementX = Input.GetAxisRaw("Horizontal");
        float deplacementZ = Input.GetAxisRaw("Vertical");

        Vector3 mouvementHorizontal = transform.right * deplacementX;
        Vector3 mouvementVertical = transform.forward * deplacementZ;

        Vector3 velocite = (mouvementHorizontal + mouvementVertical).normalized * vitesse;

        moteur.Mouvement(velocite);

        //Rotation du joueur
        float rotationY = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, rotationY, 0) * sensibilite;

        moteur.Rotation(rotation);

        //Rotation camera
        float rotationX = Input.GetAxisRaw("Mouse Y");

        Vector3 rotationCamera = new Vector3(rotationX, 0, 0) * sensibilite;

        moteur.RotationCamera(rotationCamera);
        
    }
}
