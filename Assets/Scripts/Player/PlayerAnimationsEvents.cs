using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Ambylon.PlayerController
{

    public class PlayerAnimationsEvents : MonoBehaviour
    {

        [SerializeField] private AudioSource audioSource;
        public AudioSource AudioSource => audioSource;

        [SerializeField] private AudioClip[] footsteps;
        [SerializeField] private AudioClip[] jumpSounds;
         
        /// <summary>
        /// public callbacks below comes from animations key of playerArmature Animator, default purpose is playing audio clip but can be use d for any other feature.
        /// </summary>

        #region AnimationsCallbacks

        public void OnLand()
        {
            
        }

        public void Footstep()
        {
            
        }
        
        #endregion
    }
}