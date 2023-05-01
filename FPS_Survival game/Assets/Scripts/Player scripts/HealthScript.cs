using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HealthScript : MonoBehaviour
{

    private EnemyAnimator _enemyanimator;
    private NavMeshAgent _navAgent;
    private EnemyController _enemycontroller;

    public float Health = 100f;
    public bool Is_player, Is_boar, Is_Cannibal;
    private bool _is_Dead;
    private EnemyAudio enemyAudio;
    private PlayerStats _playerstats;
    

    void Awake ()
    {
        if(Is_boar|| Is_Cannibal)
        {
            _enemyanimator = GetComponent<EnemyAnimator>();
            _enemycontroller = GetComponent<EnemyController>();
            _navAgent = GetComponent<NavMeshAgent>();
            //get audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
          
        }
        if (Is_player)
        {
            _playerstats = GetComponent<PlayerStats>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float Damage)
    {
        if (_is_Dead == true)
        
            return;
        

        Health -= Damage;
        if (Is_player)
        {
            //show damage
            _playerstats.Display_HealthStats(Health);
        }
        if(Is_Cannibal || Is_boar)
        {
            if(_enemycontroller.Enemy_State == EnemyState.PATROL)
            {
                _enemycontroller.Chase_Distance = 50f;
            }
        }

        if (Health <= 0f)
        {
            PlayerDied();
            _is_Dead = true;
        }
    }//apply damage

    void PlayerDied()
    {
        if (Is_Cannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 0.5f);
            _enemycontroller.enabled = false;
            _navAgent.enabled = false;
            _enemyanimator.enabled = false;

            //start coroutine
            StartCoroutine(DeadSound());
            // enemy manager spawn more enemies
            EnemyManager.Instance.EnemyDied(true);
        }

        if (Is_boar)
        {
            _navAgent.velocity = Vector3.zero;
            _navAgent.isStopped = true;
            _enemycontroller.enabled = false;
            _enemyanimator.Dead();
            //start coroutine
            StartCoroutine(DeadSound());
            // enemy manager spawn more enemies
            EnemyManager.Instance.EnemyDied(false);
        }

        if (Is_player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i< enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // call enemy manager to stop spawning enemy
            EnemyManager.Instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            GetComponent<PlayerAttack>().enabled = false;
        }

        if(tag == "Player")
        {
            Invoke("RestartGame", 3f);

        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }

    }// player died

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_deadsound();
    }
}
