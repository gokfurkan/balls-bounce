using System;
using Game.Dev.Scripts.Scriptables;
using Template.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dev.Scripts.Ball
{
    public class BallController : MonoBehaviour
    {
        public BallOptions ballOptions;
        
        private Rigidbody rb;
        private bool hasFirstCollision;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void InitBall()
        {
            hasFirstCollision = false;
            var randomX = Random.Range(ballOptions.startForceXRandom.x, ballOptions.startForceXRandom.y); 
            rb.AddForce(new Vector3(randomX, 0) , ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            ControlFirstInteraction(collision);
        }

        private void ControlFirstInteraction(Collision collision)
        {
            if (!ExtensionsMethods.IsInLayerMask(collision.gameObject.layer , ballOptions.interactLayers)) return;
            if (hasFirstCollision) return;

            hasFirstCollision = true;
            rb.velocity = new Vector3(rb.velocity.x , ballOptions.firstInteractionForceY);
        }

        public void ResetMovement()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}