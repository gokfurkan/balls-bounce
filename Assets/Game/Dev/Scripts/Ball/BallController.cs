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
        private Collider ballCollider;
        private TrailRenderer trailRenderer;
        private bool hasFirstCollision;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            trailRenderer = GetComponent<TrailRenderer>();
            ballCollider = GetComponent<Collider>();
        }

        public void InitBall()
        {
            hasFirstCollision = false;
            trailRenderer.enabled = false;
            ballCollider.enabled = true;
            rb.isKinematic = false;
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

            trailRenderer.enabled = true;
            hasFirstCollision = true;
            rb.velocity = new Vector3(rb.velocity.x , ballOptions.firstInteractionForceY);
        }

        public void OnReSpawnBall()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            trailRenderer.enabled = false;
        }

        public void OnInitMerge()
        {
            ballCollider.enabled = false;
            rb.isKinematic = true;
        }
    }
}