using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseXP.AbstractClasses
{
    public abstract class GameObject
    {
        #region"Declarations"
        protected Texture2D sTexture;
        protected int animCount;
        protected bool idle;
        private string contentPath;
        protected Vector2 sPosition;
        private Rectangle[] sRectangles;
        private int frameIndex;
        private int frameIndex;
        private double timeElapsed;
        private double timeToUpdate;
        public Vector2 Position { get { return sPosition; } }
        public enum movementDirections { east = 0, north = 1, northeast = 2, northwest = 3, south = 4, southeast = 5, southwest = 6, west = 7, idle = 8 };
        protected movementDirections currentDirection;
        #endregion

        public int framesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public GameObject(Vector2 position)
        {
            sPosition = position;
        }

        public void AddAnimation(int frames)
        {
            int width = sTexture.Width / frames;
            sRectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                sRectangles[i] = new Rectangle(i * width, (sTexture.Height / animCount) * (int)currentDirection, width, sTexture.Height / animCount);
            }
        }

        public abstract void DrawFrame();

        #region "Implemented Interface Methods"
        public void Update(GameTime gameTime) //IUpdate
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                if ((frameIndex < sRectangles.Length - 1) && !idle)
                {
                    frameIndex++;
                }
                else
                {
                    frameIndex = 0;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) //IDraw
        {
            DrawFrame();
            spriteBatch.Draw(sTexture, sPosition, sRectangles[frameIndex], Color.White);
        }

        public void LoadContent(ContentManager content) //ILoadContent
        {
            sTexture = content.Load<Texture2D>(contentPath);
        }
        #endregion
    }
}
