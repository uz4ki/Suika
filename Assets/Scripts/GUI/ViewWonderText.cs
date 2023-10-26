using System;
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
        [SerializeField] private Text countText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private WonderDescription[] descriptions;
    }
}
