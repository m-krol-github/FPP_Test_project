using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class CurrentWeapon : MonoBehaviour
    {
        public ItemScriptableObject current;
        
        public void Run()
        {
            StartCoroutine(current.Execute());
        }
    }
}