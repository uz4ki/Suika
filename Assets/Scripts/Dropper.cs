using UnityEngine;

namespace DefaultNamespace
{
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private float moveLimit;
        
        public void MoveDropper(float diff)
        {
            var pos = transform.localPosition;

            if (Mathf.Abs(pos.x + diff) > moveLimit) return;
            
            pos += diff * Vector3.right;
        }
    }
}