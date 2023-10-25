using UnityEngine;

namespace Fruits
{
    [CreateAssetMenu(fileName = "NewFruitBlueprint", menuName = "ScriptableObjects/FruitBlueprint")]
    public class FruitBlueprint : ScriptableObject
    {
        public float radius;
        public int score;
        public Sprite texture;
    }
}

