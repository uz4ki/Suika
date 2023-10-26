using Fruits;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class ViewScore : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            FruitManager.Instance.OnFruitUpdated.AddListener(UpdateScoreText);
        }

        private void UpdateScoreText()
        {
            _text.text = GameManager.Instance.score.ToString();
        }
    }
}
