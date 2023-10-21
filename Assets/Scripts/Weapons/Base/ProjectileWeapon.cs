using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/ProjectileWeapon", order = 0)]
    public class ProjectileWeapon : ItemScriptableObject, IProjectileWeapons
    {
        public int ammo;
        public int Ammo { get { return ammo; } }

        public int clips;
        public int Clips { get { return clips; } }

        public override void PrimaryAction()
        {
            if (ammo > 0)
                Debug.Log("Ammo left " + (ammo--));
            else
                Debug.Log("Out of ammo in " + weaponName + " wait for clip change in " + cooldownTime + "s");
        }

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(cooldownTime);
            if (ammo == 0 && clips > 0)
            {
                ammo = 20;
                clips--;
            }
            else if(clips == 0)
            {
                Debug.Log("No more clips for " + weaponName);
            }
        }
    }
}