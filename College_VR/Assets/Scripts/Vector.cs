using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class Vector : MonoBehaviour
    {
        private float x;
        private float y;
        private float z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector()
        {
            this.x = 1f;
            this.y = 1f;
            this.z = 1f;
        }
        public void setX(float x)
        {
            this.x = x;
        }
        public void setY(float y)
        {
            this.y = y;
        }
        public void setZ(float z)
        {
            this.z = z;
        }
        public float getX()
        {
            return x;
        }
        public float getY()
        {
            return y;
        }
        public float getZ()
        {
            return z;
        }
        public float magnitude()
        {
            return (float) Math.Sqrt(x * x + y * y + z * z);
        }
        public static Vector operator +(Vector a, Vector b)
        {
            Vector res = new Vector();
            res.x = a.x + b.x;
            res.y = a.y + b.y;
            res.z = a.z + b.z;
            return res;
        }
        public static Vector operator-(Vector a, Vector b)
        {
            Vector res = new Vector();
            res.x = a.x - b.x;
            res.y = a.y - b.y;
            res.z = a.z - b.z;
            return res;
        }
        public static Vector operator*(Vector a, Vector b)
        {
            Vector res = new Vector();
            res.x = a.x * b.x;
            res.y = a.y * b.y;
            res.z = a.z * b.z;
            return res;
        }
        public static Vector operator* (float con, Vector v)
        {
            Vector res = new Vector();
            res.x = con * v.x;
            res.y = con * v.y;
            res.z = con * v.z;
            return res;
        }
        public static Vector operator *(Vector v, float con)
        {
            Vector res = new Vector();
            res.x = con * v.x;
            res.y = con * v.y;
            res.z = con * v.z;
            return res;
        }
        public static Vector operator~ (Vector v)
        {
            Vector res = new Vector();
            res.x = v.x / v.magnitude();
            res.y = v.y / v.magnitude();
            res.z = v.z / v.magnitude();
            return res;
        }
        public static Vector operator/ (Vector v, float con)
        {
            Vector res = new Vector();
            res.x = v.x / con;
            res.y = v.y / con;
            res.z = v.z / con;
            return res;
        }

        
    }
}
