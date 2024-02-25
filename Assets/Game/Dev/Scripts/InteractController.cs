using System;
using Game.Dev.Scripts.Interfaces;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class InteractController : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            var interacteable = collision.gameObject.GetComponent<Interacteable>();
            if (interacteable != null)
            {
                interacteable.Interact(GetComponent<BallController>());   
            }
        }
    }
}