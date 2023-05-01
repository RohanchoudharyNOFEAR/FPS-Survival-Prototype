using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}


public class EnemyController : MonoBehaviour
{
    private EnemyAnimator _enemy_Animator;
    private NavMeshAgent _navAgent;
    private EnemyState _enemyState;
    public float Walk_Speed = 0.7f;
    public float Run_Speed = 3f;
    public float Chase_Distance = 7f;
    private float Current_Chase_Distance;
    public float Attack_Distance = 1.8f;
    public float Chase_After_Attack_Distance = 2f;
    public float Patrol_Radius_Min = 20f, Patrol_Radius_Max = 50f;
    public float Patrol_For_This_Time = 15f;
    private float Patrol_Timer;
    public float Wait_Before_Attack = 2f;
    private float Attack_Timer;
    private Transform _target;
    public GameObject Attack_point;
    private EnemyAudio _enemyaudio;




    private void Awake()
    {
        _enemy_Animator = GetComponent<EnemyAnimator>();
        _navAgent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindWithTag("Player").transform;
        _enemyaudio = GetComponentInChildren<EnemyAudio>();
    }







    // Start is called before the first frame update
    void Start()
    {
        _enemyState = EnemyState.PATROL;
        Patrol_Timer = Patrol_For_This_Time;
        Attack_Timer = Wait_Before_Attack;
        Current_Chase_Distance = Chase_Distance;

    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyState == EnemyState.PATROL)
        {
            Patrol();

        }

        if (_enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if (_enemyState == EnemyState.ATTACK)
        {
            Attack();
;
        }

    }//update


    void Patrol()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = Walk_Speed;
        Patrol_Timer += Time.deltaTime;
        if (Patrol_Timer > Patrol_For_This_Time)
        {
            SetNewRandomDestination();
            Patrol_Timer = 0f;

        }
        if (_navAgent.velocity.sqrMagnitude > 0)
        {
            _enemy_Animator.Walk(true);
        }
        else
        {
            _enemy_Animator.Walk(false);
        }

        if (Vector3.Distance(transform.position, _target.position) <= Chase_Distance)
        {
            _enemy_Animator.Walk(false);
            _enemyState = EnemyState.CHASE;

            //play spooted sound
            _enemyaudio.Play_Scremesounds();
        }


    }//patrol

    void Chase()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = Run_Speed;

        _navAgent.SetDestination(_target.position);

        if (_navAgent.velocity.sqrMagnitude > 0)
        {
            _enemy_Animator.Run(true);
        }
        else
        {
            _enemy_Animator.Run(false);
        }
        if (Vector3.Distance(transform.position, _target.position) <= Attack_Distance)
        {
            _enemy_Animator.Run(false);
            _enemy_Animator.Walk(false);
            _enemyState = EnemyState.ATTACK;

            if (Chase_Distance != Current_Chase_Distance)
            {
                Chase_Distance = Current_Chase_Distance;
            }
            else if (Vector3.Distance(transform.position, _target.position) > Chase_Distance)
            {
                _enemy_Animator.Run(true);
                _enemyState = EnemyState.PATROL;
                Patrol_Timer = Patrol_For_This_Time;
                if (Chase_Distance != Current_Chase_Distance)
                {
                    Chase_Distance = Current_Chase_Distance;
                }
            }

        }//chase


      

    }
    void Attack()
    {
       
        _navAgent.velocity = Vector3.zero;
        _navAgent.isStopped = true;
        Attack_Timer += Time.deltaTime;
        if(Attack_Timer> Wait_Before_Attack)
        {
            _enemy_Animator.Attack();
            Attack_Timer = 0;
            //play attack sound

            _enemyaudio.Play_AttackSound();

            
        }

        if (Vector3.Distance(transform.position, _target.position) > Chase_Distance + Chase_After_Attack_Distance)
        {
            _enemyState = EnemyState.CHASE;
        }


    }//attack

    void SetNewRandomDestination()
    {
        float rand_radius = Random.Range(Patrol_Radius_Min, Patrol_Radius_Max);
        Vector3 rand_dir = Random.insideUnitSphere * rand_radius;
        rand_dir += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(rand_dir, out navHit, rand_radius, -1);
        _navAgent.SetDestination(navHit.position);
    }

    void Turn_On_AttackPoint()
    {
        Attack_point.SetActive(true);
    }
    void Turn_Off_AttackPoint()
    {
        if (Attack_point.activeInHierarchy)
        {
            Attack_point.SetActive(false);
        }

    }
    public EnemyState Enemy_State
    {
        get;
        set;
    }

}