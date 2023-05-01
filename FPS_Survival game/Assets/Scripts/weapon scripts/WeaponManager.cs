using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] _weapons;
    private int _current_weapon_index;


    // Start is called before the first frame update
    void Start()
    {
        _current_weapon_index = 0;
        _weapons[_current_weapon_index].gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Turn_on_selected_weapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Turn_on_selected_weapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Turn_on_selected_weapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Turn_on_selected_weapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Turn_on_selected_weapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Turn_on_selected_weapon(5);
        }
    }

    void Turn_on_selected_weapon(int weaponIndex)
    {
        if (_current_weapon_index == weaponIndex)
        {
            return;
        }
        _weapons[_current_weapon_index].gameObject.SetActive(false);
        _weapons[weaponIndex].gameObject.SetActive(true);
        _current_weapon_index = weaponIndex;
    }


    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return _weapons[_current_weapon_index];
    }
}
