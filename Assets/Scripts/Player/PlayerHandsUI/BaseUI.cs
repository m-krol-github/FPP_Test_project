using UnityEngine;

namespace _Ambylon.PlayerController
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