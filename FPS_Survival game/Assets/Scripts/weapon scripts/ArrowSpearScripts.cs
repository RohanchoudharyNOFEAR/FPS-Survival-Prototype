using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpearScripts : MonoBehaviour
{
    private Rigidbody _mybody;
    public  float Speed = 30f;
    public float Deactivate_Timer = 3f;
    public float Damage = 15f;



    private void Awake()
    {
        _mybody = GetComponent<Rigidbody>();
    }




    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameobject", Deactivate_Timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch(Camera maincam)
    {
        //using velocity will direct or instantly increase speed 0 to 30
        //but addforce will linearly increase the speed
        _mybody.velocity =    maincam.transform.forward * Speed;
        transform.LookAt(transform.position + _mybody.velocity);
    }
    void DeactivateGameobject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<HealthScript>().ApplyDamage(Damage);

            gameObject.SetActive(false);
        }
    }
}
