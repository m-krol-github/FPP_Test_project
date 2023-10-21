using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        void PrimaryAction();
        void SecondaryAction();
    }
}