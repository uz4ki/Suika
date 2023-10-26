using System;
using InGame;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    [Serializable]
    public struct WonderDescription
    {
        [TextArea] public string description;
    }
    public class ViewWonderText : MonoBehaviour
    {
        [SerializeField] private Text descriptionText;
        [SerializeField] private WonderDescription[] descriptions;

        private void Start()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            descriptionText.text = !WonderManager.Instance.isWonder ? "ワンダー:なし" : descriptions[(int)WonderManager.Instance.state].description;
        }
    }
}
