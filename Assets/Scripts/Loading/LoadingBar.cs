using UnityEngine;
using UnityEngine.UI;

namespace _Ambylon.Scripts.Loading
{
    public class LoadingBar : MonoBehaviour
    {
        private Slider _slider;

        void Start()
        {
            _slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
