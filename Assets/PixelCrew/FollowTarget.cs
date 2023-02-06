using System;
using UnityEngine;

namespace PixelCrew
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float dumping;

        private void LateUpdate()
        {
            var currentObjectTransform = transform.position;
            var dest = new Vector3(target.position.x, target.position.y, currentObjectTransform.z);
            currentObjectTransform = Vector3.Lerp(currentObjectTransform, dest, Time.deltaTime * dumping);
            transform.position = currentObjectTransform;
        }
    }
}