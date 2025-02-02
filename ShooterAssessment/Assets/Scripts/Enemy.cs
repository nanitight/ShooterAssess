using UnityEngine;

namespace GN.ShooterAssessment
{
    /// <summary>
    /// The Enemy that is to be destroyed.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public int LocationIndex =-1;

        private void Update()
        {
            transform.Rotate(transform.up,Time.deltaTime);
        }
    }
}
