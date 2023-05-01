using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager _weapon_manager;
    public float Firerate = 15f;
    private float _nexttimetofire;
    public float Damage = 20f;
    private Animator _zoomcamanim;
    private bool _zoomed;
    private Camera _mainCamera;
    private GameObject _crosshair;
    private bool _is_Aiming;
    [SerializeField]
    private GameObject _arrow_prefab, _spear_prefab;
    [SerializeField]
    private Transform _arrowspear_start_position;
    private void Awake()
    {
        _weapon_manager = GetComponent<WeaponManager>();
        _zoomcamanim = transform.Find("Look Root").transform.Find("FP Camera").GetComponent<Animator>();
        _crosshair = GameObject.FindWithTag("Crosshair");
        _mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        if (_weapon_manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > Firerate)
            {
                _nexttimetofire = Time.time + 1 / Firerate;
                BulletFired();
                _weapon_manager.GetCurrentSelectedWeapon().ShootAimation();

                // bulletfired
               
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_weapon_manager.GetCurrentSelectedWeapon().tag == "AXE")
                {
                    _weapon_manager.GetCurrentSelectedWeapon().ShootAimation();
                }

                if(_weapon_manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    BulletFired();
                    _weapon_manager.GetCurrentSelectedWeapon().ShootAimation();
                    //bulletfired
                   
                }
                else
                {
                    if(_is_Aiming == true)
                    {
                        _weapon_manager.GetCurrentSelectedWeapon().ShootAimation();
                        if(_weapon_manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            //throw arrow
                            ThrowArrowOrSpear(true);
                        }
                        else if(_weapon_manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            ThrowArrowOrSpear(false);
                            //throw spear
                        }
                    }

                }
            }
        }
        
           
    }// weapon shoot

    void ZoomInAndOut()
    {
        if(_weapon_manager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _zoomcamanim.Play("Zoomin");
                _crosshair.SetActive(false);
            }
        }
        if (_weapon_manager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonUp(1))
            {
                _zoomcamanim.Play("Zoomout");
                _crosshair.SetActive(true);
            }
        }

        if(_weapon_manager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _weapon_manager.GetCurrentSelectedWeapon().Aim(true);
                _is_Aiming = true;
            }
        }
        if (_weapon_manager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonUp(1))
            {
                _weapon_manager.GetCurrentSelectedWeapon().Aim(false);
                _is_Aiming = false;
            }
        }

    }// zoom in and out


    void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject Arrow = Instantiate(_arrow_prefab, _arrowspear_start_position.position, Quaternion.identity);
            Arrow.GetComponent<ArrowSpearScripts>().Launch(_mainCamera);

        }
        else
        {
            GameObject Spear= Instantiate(_spear_prefab, _arrowspear_start_position.position, Quaternion.identity);
            Spear.GetComponent<ArrowSpearScripts>().Launch(_mainCamera);
        }
    }// throw arrow spear

    void BulletFired()
    {
        RaycastHit hit;
        if(Physics.Raycast(_mainCamera.transform.position,_mainCamera.transform.forward, out hit))
        {
           if(hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(Damage);
               
            }
        }
    }
}
