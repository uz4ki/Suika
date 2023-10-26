using System;
using UnityEngine;

namespace Fruits
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Fruit : MonoBehaviour
    {
        public SpriteRenderer sprite;
        public int index;
        public bool isConverted;
        public bool hasCollided;
        
        private Rigidbody _rigidbody;
        [SerializeField] private BillBoard _billboard;
        private int _x;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _x = UnityEngine.Random.Range(0, 2);
        }

        private void FixedUpdate()
        {
            if (FruitManager.Instance.isDriving)
            {
                _rigidbody.AddTorque(Vector3.one * 300f * (0.5f - _x));
            }
        }

        private void Update()
        {
            _billboard.isEnabled = FruitManager.Instance.isTopView;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            hasCollided = true;

            var targetFruit = collision.gameObject.GetComponent<Fruit>();
            if (targetFruit == null || targetFruit.isConverted) return;

            if (targetFruit.index == -3)
            {
                transform.localScale *= 1.5f;
                Destroy(targetFruit.gameObject);
            }
            if (targetFruit.index == -4)
            {
                transform.localScale *= 0.75f;
                Destroy(targetFruit.gameObject);
            }
            
            if ( this.index != targetFruit.index) return;

            isConverted = true;
            targetFruit.isConverted = true;
            
            FruitManager.Instance.ConvertFruit(this, targetFruit);
        }
    }
}
