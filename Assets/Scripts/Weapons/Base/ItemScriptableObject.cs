using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class ItemScriptableObject : ScriptableObject, IWeapon
    {
        public string weaponName;
        public float cooldownTime;
        public Sprite weaponIcon;

        [TextArea]
        public string description;

        public abstract IEnumerator Execute();

        public virtual void PrimaryAction()
        {

        }

        public virtual void SecondaryAction()
        {

        }
    }
}