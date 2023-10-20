using UnityEngine;

namespace _Ambylon.Scripts.Objects.Interactable
{
    public abstract class EnvironmentObject : MonoBehaviour, IInteractable
    {
        void IInteractable.OnHoverBehaviour()
        {
            throw new System.NotImplementedException();
        }

        //Behaviour, when player selects at the mesh
        protected abstract void OnClickBehaviour();
    }
}


