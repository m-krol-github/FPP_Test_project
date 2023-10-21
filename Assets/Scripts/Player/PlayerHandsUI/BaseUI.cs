using UnityEngine;

namespace Gameplay.PlayerController
{
    public abstract class BaseUI : MonoBehaviour
    {
        public virtual void ShowView()
        {
            gameObject.SetActive(true);
        }

        public virtual void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}