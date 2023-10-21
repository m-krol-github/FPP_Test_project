using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.PlayerControl
{
    public class PlayerWeaponShooting : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void ShootWeapon()
        {
            animator.SetTrigger("StandShoot");
        }

        public void ReloadWeapon()
        {
            animator.SetTrigger("Reload");
        }
    }
}