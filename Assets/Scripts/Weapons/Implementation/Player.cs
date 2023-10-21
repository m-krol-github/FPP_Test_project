using Gameplay.Inputs;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Weapons
{
    public class Player : MonoBehaviour, IWeapon
    {
        [SerializeField] private AllWeaponsObject weapons;
        [SerializeField] private CurrentWeapon currentWeapon;
        [SerializeField] private TextMeshProUGUI weaponText;

        private ItemScriptableObject activeWeapon;
        private int weaponIndex = 0;

        private UserInput _inputs;

        private void Awake()
        {
            _inputs = new UserInput();

            _inputs.Actions.PrimaryAction.canceled += _ => PrimaryAction();
            _inputs.Actions.SecondaryAction.canceled += _ => SecondaryAction();

            if (activeWeapon == null)
            {
                activeWeapon = weapons.avaibleWeapons[weaponIndex];
                currentWeapon.current = activeWeapon;
                weaponText.text = activeWeapon.weaponName;
            }
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        public void PrimaryAction()
        {
            activeWeapon.PrimaryAction();
            currentWeapon.Run();
        }

        public void SecondaryAction()
        {
            weaponIndex++;

            if (weapons.avaibleWeapons.Count == weaponIndex)
                weaponIndex = 0;

            activeWeapon = weapons.avaibleWeapons[weaponIndex];
            weaponText.text = activeWeapon.weaponName;
            currentWeapon.current = activeWeapon;
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
    }
}