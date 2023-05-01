using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField]
    private GameObject Boar_prefab, Cannibal_prefab;

    public Transform[] Boar_spawn_points, Cannibal_spawn_points;

    [SerializeField]
    private int Cannibal_enemy_count, Boar_enemy_count;

    private int Initial_Canninal_count, Initial_Boar_Count;

    public float Wait_before_enemy_spawn_time = 10f;

    
        // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
        
    }

     void Start()
    {
        Initial_Boar_Count = Boar_enemy_count;
        Initial_Canninal_count = Cannibal_enemy_count;

        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void SpawnEnemies()
    {
        SpawnCannibals();
        SpawnBoars();

    }

    void SpawnCannibals()
    {
        int Index = 0;
        for(int i = 0; i< Cannibal_enemy_count; i++)
        {
            if(Index > Cannibal_spawn_points.Length)
            {
                Index = 0;
            }

            Instantiate(Cannibal_prefab, Cannibal_spawn_points[Index].position, Quaternion.identity);
            Index++;

        }

        Cannibal_enemy_count = 0;

    }//spawncannibals

    void SpawnBoars()
    {
        int Index = 0;
        for (int i = 0; i < Boar_enemy_count; i++)
        {
            if (Index > Boar_spawn_points.Length)
            {
                Index = 0;
            }

            Instantiate(Boar_prefab, Boar_spawn_points[Index].position, Quaternion.identity);
            Index++;

        }

        Boar_enemy_count = 0;

    }//spawn boars


    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(Wait_before_enemy_spawn_time);
        SpawnCannibals();
        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");
    }


    public void EnemyDied(bool cannibal)
    {
        if (cannibal)
        {
            Cannibal_enemy_count++;

            if (Cannibal_enemy_count > Initial_Canninal_count)
            {
                Cannibal_enemy_count = Initial_Canninal_count;
            }
            else
            {
                Boar_enemy_count++;
                if(Boar_enemy_count > Initial_Boar_Count)
                {
                    Boar_enemy_count = Initial_Boar_Count;
                }
            }
        }

    }//enemydied

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }


}//class
