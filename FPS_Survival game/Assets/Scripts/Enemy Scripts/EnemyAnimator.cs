using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(bool walk)
    {
        _anim.SetBool("Walk", walk);
    }
    public void Run(bool run)
    {
        _anim.SetBool("Run", run);
    }
    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }
    public void Dead()
    {
        _anim.SetTrigger("Death");
    }
}
