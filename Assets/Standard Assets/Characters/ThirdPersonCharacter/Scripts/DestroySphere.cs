using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class DestroySphere : MonoBehaviour
    {
        public event Action<GameObject> OnFinishCnange;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AICharacterControl>())
            {
                OnFinishCnange?.Invoke(gameObject);
            }
        }
    }
}
