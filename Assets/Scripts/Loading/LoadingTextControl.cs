using TMPro;
using UnityEngine;

namespace _Ambylon.Scripts.Loading
{
    public class LoadingTextControl : MonoBehaviour
    {
        private TMP_Text _tmpText;
        private string _startText;
        
        // Start is called before the first frame update
        void Start()
        {
            _tmpText = GetComponent<TMP_Text>();
            _startText = _tmpText.text;
            _tmpText.text = _startText + "0%";
        }

        private void Update()
        {
            
        }
    }
}
