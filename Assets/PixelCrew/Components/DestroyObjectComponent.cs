﻿using UnityEngine;

namespace PixelCrew.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;

        public void DestroySelf()
        {
            Destroy(_objectToDestroy);
        }
    }
}