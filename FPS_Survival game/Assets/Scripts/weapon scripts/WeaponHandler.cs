using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator _anim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public WeaponAim weaponAim;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private AudioSource _shootSound, _reloadSound;
    public GameObject Attack_point;

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

    public void ShootAimation()
    {
        _anim.SetTrigger("Shoot");
    }
    public void Aim(bool CanAim)
    {
        _anim.SetBool("Aim", CanAim);
    }

    void Turn_On_Muzzle_Flash()
    {
        _muzzleFlash.SetActive(true);
    }
    void Turn_Off_Muzzle_Flash()
    {
        _muzzleFlash.SetActive(false);
    }


    void Play_Shoot_Sound()
    {
        _shootSound.Play();
    }

    void Play_Reload_Sound()
    {
        _reloadSound.Play();
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


}
