using Gameplay.Pool;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Gameplay.PlayerControl
{
    public class PlayerWeaponShooting : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [Header("Player Shooting")]
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootForce;
        [SerializeField] private PoolManager _pooling;
        [SerializeField] private GameObject muzzleEffect;

        private int _shootTimes;

        public void ShootWeapon()
        {
            _shootTimes = 3;
            animator.SetTrigger("Shoot");
            StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            _shootTimes--;
            GameObject go = _pooling.UseObject(bullet, shootPoint.position, Quaternion.identity);
            Rigidbody bulletBody = go.GetComponent<Rigidbody>();
            bulletBody.AddForce(shootPoint.transform.forward * shootForce, ForceMode.Impulse);
            PoolManager.Instance.UseObject(muzzleEffect, shootPoint.transform.position, shootPoint.transform.rotation);
            
            if (_shootTimes > 0) 
            {
                yield return new WaitForSeconds(.1f);
                StartCoroutine(ShootRoutine());
            }
            else
                yield return null;
        }

        public void ReloadWeapon()
        {
            animator.SetTrigger("Reload");
        }
    }
}