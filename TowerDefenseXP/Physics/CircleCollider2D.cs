using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TowerDefenseXP.Physics
{
    public struct CircleCollider2D
    {        
        public Vector2 Center { get;  set; }
        public float Radius { get;  set; }

        public CircleCollider2D(Vector2 center, float radius) : this()
        {
            Center = center;
            Radius = radius;
        }

        public bool Contains(Vector2 point)
        {
            return ((point - Center).Length() <= Radius);
        }

        public bool Intersects(CircleCollider2D other)
        {
            return ((other.Center)-Center).Length() < (other.Radius - Radius);
        }
    }
}
