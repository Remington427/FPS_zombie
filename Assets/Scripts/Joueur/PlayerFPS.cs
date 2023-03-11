using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPS : MonoBehaviour
{
    public Camera playerCamera;
    private CharacterController characterController;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 15f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    Vector3 moveDirection;

    private bool isRunning;

    float rotationX = 0;
    public float rotationSpeed = 2.0f;
    public float rotationLimit = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //calcul mouvement
        CalculMouvement();
        //applique le mouvement
        characterController.Move(moveDirection * Time.deltaTime);
        //rotation
        CalculRotation();
    }

    void CalculMouvement()
    {
        //avant/arriere
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        //gauche/droite
        Vector3 right = transform.TransformDirection(Vector3.right);

        //avant/arriere
        float speedZ = Input.GetAxis("Vertical");
        //gauche/droite
        float speedX = Input.GetAxis("Horizontal");

        float speedY = moveDirection.y;
        
        //attribution variable course ou marche
        isRunning = Input.GetKey(KeyCode.LeftShift);
        
        //attribution vitesse
        if(isRunning)
        {
            speedZ *= runningSpeed;
            speedX *= runningSpeed;
        }
        else
        {
            speedZ *= walkingSpeed;
            speedX *= walkingSpeed;
        }

        moveDirection = forward * speedZ + right * speedX;

        if(Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = speedY;
        }

        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    void CalculRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -rotationLimit, rotationLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
    }
}
