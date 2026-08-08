using UnityEngine;

namespace Pickable
{
    public abstract class Pickable : MonoBehaviour
    {
        public abstract void Pickup(GameObject picker);
    }
}