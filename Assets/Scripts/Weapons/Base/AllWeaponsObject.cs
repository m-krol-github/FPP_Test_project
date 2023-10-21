using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "AllWeaponsHolder", menuName = "Weapons/AllWeaponsHolder", order = 0)]
    public class AllWeaponsObject : ScriptableObject
    {
        public List<ItemScriptableObject> avaibleWeapons;
    }
}