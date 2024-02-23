using Game.Dev.Scripts.Scriptables;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts.Ball
{
    public class BallController : MonoBehaviour
    {
        public BallOptions ballOptions;
        public Rigidbody rb;
        
        [Space(10)] 
        public LayerMask interactLayers;
        public Vector2Int startForceXRandom;
        public float firstInteractionForceY;

        private bool hasFirstCollision;
        
        public void InitBall()
        {
            hasFirstCollision = false;
            rb.AddForce(new Vector3(Random.Range(startForceXRandom.x , startForceXRandom.y) , 0) , ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            ControlFirstInteraction(collision);
        }

        private void ControlFirstInteraction(Collision collision)
        {
            if (!ExtensionsMethods.IsInLayerMask(collision.gameObject.layer , interactLayers)) return;
            if (hasFirstCollision) return;

            hasFirstCollision = true;
            rb.velocity = new Vector3(rb.velocity.x , firstInteractionForceY);
        }

        public void ResetMovement()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}