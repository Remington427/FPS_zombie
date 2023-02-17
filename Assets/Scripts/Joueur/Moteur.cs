using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moteur : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 velocite;
    private Vector3 rotation;
    private Vector3 rotationCamera;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Bouger();
        Regarder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mouvement(Vector3 m_velocite)
    {
        velocite = m_velocite;
    }

    public void Rotation(Vector3 m_rotation)
    {
        rotation = m_rotation;
    }

    public void RotationCamera(Vector3 m_rotationCamera)
    {
        rotationCamera = m_rotationCamera;
    }

    private void Bouger()
    {
        if(velocite!=Vector3.zero)
        {
            rb.MovePosition(rb.position + velocite * Time.fixedDeltaTime);
        }
    }

    private void Regarder()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        cam.transform.Rotate(-rotationCamera);

    }
}
