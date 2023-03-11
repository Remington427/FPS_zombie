using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Transform target;

    public MeleeWeapon weaponRight;
    public MeleeWeapon weaponLeft;

    UnityEngine.AI.NavMeshAgent navMeshAgent;

    Animator animator;

    const string STAND_STATE = "Stand";
    public const string DEFEATED_STATE = "Defeated";
    public const string RUN_STATE = "Run";
    public const string ATTACK_STATE = "Attack";

    public string currentAction;

    private float nextSearch;
    private float searchRange = 2f;

    private float startDecay;
    private float decayTime = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        currentAction = STAND_STATE;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nextSearch = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            if(Time.time > (startDecay + decayTime))
            {
                Decay();
            }
        }
        else if(Time.time > nextSearch)
        {
            nextSearch = Time.time + searchRange;
            GameObject[] joueurs = GameObject.FindGameObjectsWithTag("Player");
            if(joueurs.Length != 0) target = GetClosestPlayer(joueurs);
        }
        else if(target != null)
        {
            if(MovingToTarget())
            {
                return;
            }
            else
            {
                if(currentAction != ATTACK_STATE)
                {
                    Attack();
                    return;
                }
                else
                {
                    Attacking();
                    return;
                }
            }
        }
    }

    private Transform GetClosestPlayer(GameObject[] joueurs)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject joueur in joueurs)
        {
            Vector3 diff = joueur.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = joueur;
                distance = curDistance;
            }
        }
        return closest.transform;
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
        startDecay = Time.time;
    }

    private void Run()
    {
        ResetAnimation();
        currentAction = RUN_STATE;
        animator.SetBool(RUN_STATE, true);
    }

    private void Attack()
    {
        ResetAnimation();
        currentAction = ATTACK_STATE;
        animator.SetBool(ATTACK_STATE, true);
    }

    private void Attacking()
    {
        if(this.animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_STATE))
        {
            //compte le temps de l'animation
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            //fin de l'animation
            if(normalizedTime > 1)
            {
                //Debug.Log("Stop");
                weaponRight.StopAttack();
                weaponLeft.StopAttack();
                Stand();
                return;
            }

            weaponRight.StartAttack();
            weaponLeft.StartAttack();

        }
    }

    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE, false);
        animator.SetBool(DEFEATED_STATE, false);
        animator.SetBool(RUN_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }

    private bool MovingToTarget()
    {
        navMeshAgent.SetDestination(target.transform.position);

        //si navMeshAgent n'est pas pret
        if(navMeshAgent.remainingDistance == 0) return true;

        if(navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if(currentAction != RUN_STATE) Run();
        }
        else
        {
            RotateToTarget();
            return false;
        }
        return true;
    }

    private void RotateToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }

    private void Decay()
    {
        Destroy(gameObject);
    }
}
