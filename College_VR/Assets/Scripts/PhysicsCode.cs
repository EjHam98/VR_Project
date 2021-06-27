using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PhysicsCode
    {
        public float cd = 0.47f;
        public float ro = 1.225f;
        public float g = 9.98f;

        private Vector3 pivot_p, bucket_p;

        public PhysicsCode()
        {

        }

        public PhysicsCode(Vector3 pivot_pos, Vector3 bucket_pos)
        {
            pivot_p = pivot_pos;
            bucket_p = bucket_pos;
        }

        public void setPivotPosition(Vector3 pivot_pos)
        {
            pivot_p = pivot_pos;
        }

        public void setBucketPosition(Vector3 bucket_pos)
        {
            bucket_p = bucket_pos;
        }

        public Vector3 getGravity(float mass)
        {
            return new Vector3(0f, -1f*g*mass, 0f);
        }

        public float getAirResistance(float surface, float velocity)
        {
            return 0.5f * cd * ro * surface * velocity * velocity;
        }
    }

}