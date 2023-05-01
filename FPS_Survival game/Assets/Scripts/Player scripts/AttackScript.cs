using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float Damage = 2f;
    public float radius = 2f;
    public LayerMask layermask;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] Hits = Physics.OverlapSphere(transform.position, radius, layermask);

        if(Hits.Length> 0)
        {
            Hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(Damage);
            gameObject.SetActive(false);
        }
        
    }
}
