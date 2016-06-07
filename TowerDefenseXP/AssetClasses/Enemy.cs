using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseXP
{
    namespace AssetClasses
    {
        public class Enemy : AnimatedSprite
        {
            public Enemy(Vector2 position) : base(position)
            {
                framesPerSecond = 10;
                idle = true;                
            }

            public void LoadContent(ContentManager content)
            {
                sTexture = content.Load<Texture2D>("Characters/spritesheet");
                animCount = 8;
                AddAnimation(8);
            }

            public void ChangeDirection(movementDirections newDirection)
            {
                if (newDirection == movementDirections.idle)
                {
                    idle = true;
                    return;
                }
                else
                {
                    idle = false;
                    //animSelect = Array.IndexOf(DirectionRef, direction);
                    currentDirection = newDirection;
                    AddAnimation(8);
                }
            }

            public override void DrawFrame()
            {
                if (!idle)
                {
                    switch (currentDirection)
                    {
                        case movementDirections.east:
                            sPosition.X += 1;
                            break;
                        case movementDirections.north:
                            sPosition.Y -= 1;
                            break;
                        case movementDirections.west:
                            sPosition.X -= 1;
                            break;
                        case movementDirections.south:
                            sPosition.Y += 1;
                            break;
                        case movementDirections.southwest:
                            sPosition.X -= 0.5f;
                            sPosition.Y += 0.5f;
                            break;
                        case movementDirections.southeast:
                            sPosition.X += 0.5f;
                            sPosition.Y += 0.5f;
                            break;
                        case movementDirections.northwest:
                            sPosition.X -= 0.5f;
                            sPosition.Y -= 0.5f;
                            break;
                        case movementDirections.northeast:
                            sPosition.X += 0.5f;
                            sPosition.Y -= 0.5f;
                            break;
                    }
                }
            }
        }
    }
}
