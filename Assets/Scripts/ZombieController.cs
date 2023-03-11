using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Animator animator;

    const string STAND_STATE = "Stand";
    public const string DEFEATED_STATE = "Defeated";

    public string currentAction;

    // Start is called before the first frame update
    void Awake()
    {
        currentAction = STAND_STATE;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Stand()
    {
        ResetAnimation();
        currentAction = STAND_STATE;
        animator.SetBool(STAND_STATE, true);
    }

    private void Defeated()
    {
        ResetAnimation();
        currentAction = DEFEATED_STATE;
        animator.SetBool(DEFEATED_STATE, true);
    }

    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE, false);
        animator.SetBool(DEFEATED_STATE, false);
    }
}
