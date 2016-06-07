using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using TowerDefenseXP.Physics;
using TowerDefenseXP.Interfaces;


namespace TowerDefenseXP
{
    namespace AssetClasses
    {
        class Tower : IUpdate, IDraw, ILoadContent
        {
            private Texture2D towerTexture;
            private Vector2 towerPos;
            public bool firstClick;
            public bool active;
            private int screenX, screenY;
            public CircleCollider2D CircleCollider;
            private double timeElapsed;
            private double fireRate = 1f;
            private Vector2 currentTarget;
            private List<RedProjectile> projectileList;
            public Vector2 SetTarget { get; set; }
            public Texture2D SetProjectile { get; set; }
            public bool targetAcquired;

            public Tower(Vector2 pos, Texture2D _towerTexture, int _screenX, int _screenY)
            {
                towerPos = pos;
                firstClick = true;
                towerTexture = _towerTexture;
                screenX = _screenX;
                screenY = _screenY;
            }

            public void Update(GameTime gameTime)
            {
                if (firstClick)
                {
                    towerPos.X = Cursor.Position.X - screenX;
                    towerPos.Y = Cursor.Position.Y - screenY;
                    active = false;
                }
                else
                {
                    if (!active)
                    {
                        CircleCollider = new CircleCollider2D(towerPos, 400.0f);
                        CircleCollider.Center = towerPos;
                    }                    
                    active = true;
                }
                if (targetAcquired)
                {
                    timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeElapsed > fireRate)
                    {
                        timeElapsed -= fireRate;
                        FireProjectile();
                    }
                }
                if (projectileList != null)
                {
                    foreach (var proj in projectileList)
                    {
                        proj.Update(gameTime);
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(towerTexture, towerPos, Color.White);
                if (projectileList != null)
                {
                    foreach (var proj in projectileList){
                        proj.Draw(spriteBatch);
                    }
                }
            }

            public void FireProjectile()
            {
                if (projectileList == null)
                {
                    projectileList = new List<RedProjectile>();
                }
                var projectile = new RedProjectile(this.towerPos);
                projectile.SetTexture(SetProjectile);
                projectile.Fire(SetTarget);
                projectileList.Add(projectile);
                Debug.Print("Projectile Fired!" + Environment.NewLine);
            }            
        }
    }
}
