using Fruits;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class ViewNext : MonoBehaviour
    {
        private Image _image;
        [SerializeField] private Sprite cherries;

        private void Awake()
        {
            _image = GetComponent<Image>();
            FruitManager.Instance.OnFruitUpdated.AddListener(UpdateNextFruitView);
        }

        private void UpdateNextFruitView()
        {
            var index =  GameManager.Instance.nextFruit;
            if (index == -1)
            {
                _image.sprite = FruitManager.Instance.wonderPrefab.sprite.sprite;
            }
            else if (index == -2)
            {
                _image.sprite = cherries;
            }
            else if (index == -3)
            {
                _image.sprite = FruitManager.Instance.extraFruits[0].texture;
            }
            else if (index == -4)
            {
                _image.sprite = FruitManager.Instance.extraFruits[1].texture;
            }
            else
            {
                _image.sprite = FruitManager.Instance.fruitBlueprints[index].texture;
            }
        }
    }
}
