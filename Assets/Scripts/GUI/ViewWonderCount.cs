using InGame;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class ViewWonderCount : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            WonderManager.Instance.onCounterUpdate.AddListener(UpdateText);
        }

        private void OnDisable()
        {
            WonderManager.Instance.onCounterUpdate.RemoveListener(UpdateText);
        }

        private void UpdateText()
        {
            _text.text = WonderManager.Instance.counter.ToString();
        }
    }
}