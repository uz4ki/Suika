using UnityEngine;

namespace Fruits
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Fruit : MonoBehaviour
    {
        public int index;
        public bool isConverted;
        public bool hasCollided;


        private void OnCollisionEnter(Collision collision)
        {
            hasCollided = true;
            
            var targetFruit = collision.gameObject.GetComponent<Fruit>();
            if (targetFruit == null || targetFruit.isConverted || this.index != targetFruit.index) return;
            
            isConverted = true;
            targetFruit.isConverted = true;
            
            FruitManager.Instance.ConvertFruit(this, targetFruit);
        }
    }
}
