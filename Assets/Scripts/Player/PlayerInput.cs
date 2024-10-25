using System;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayerInput
    {
        public float GetDirection()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                return -1;
            if (Input.GetKey(KeyCode.RightArrow))
                return 1;
            
            return 0;
        }

        public bool GetFireRequired()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }

            return false;
        }
    }
}