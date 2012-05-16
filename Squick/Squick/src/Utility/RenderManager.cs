using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Squick.src.Entity;

namespace Squick.Utility
{
    public static class RenderManager
    {
        private static GraphicsDevice _device;
        private static SpriteBatch _spriteBatch;
        private static Rectangle _screenBounds;
        private static Vector2 _screenCenter;
        private static RenderTarget2D _backgroundRenderTarget;
        private static RenderTarget2D _foregroundRenderTarget;

        public static void Initialize(GraphicsDevice device)
        {
            _device = device;

            _spriteBatch = new SpriteBatch(_device);
            _screenBounds = new Rectangle(0, 0, _device.Viewport.Width, _device.Viewport.Height);
            _screenCenter = new Vector2(_device.Viewport.Width / 2, _device.Viewport.Height / 2);

            _backgroundRenderTarget = new RenderTarget2D(_device, _screenBounds.Width, _screenBounds.Height);
            _foregroundRenderTarget = new RenderTarget2D(_device, _screenBounds.Width, _screenBounds.Height);
        }


        /**
         * TODO: DRAWING QUEUES
         * ie: Add2DTexture or something, to avoid multiple Begin and End
         **/


        public static void DrawString(SpriteFont font, String content, Vector2 location, Color color)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, content, location, color);
            _spriteBatch.End();
        }

        public static void DrawBox(Rectangle box)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(ResourceManager.tex_pixel, box, Color.Red);
            _spriteBatch.End();
        }

        public static void DrawLine(Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            _spriteBatch.Begin();
            _spriteBatch.Draw(ResourceManager.tex_pixel, point1, null, Color.Red,
                       angle, Vector2.Zero, new Vector2(length, 10f),
                       SpriteEffects.None, 0);
            _spriteBatch.End();
        }

        public static void Draw2DTexture(Texture2D tex, Rectangle box, Color color)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(tex, box, color);
            _spriteBatch.End();
        }

        public static void DrawEntity(Entity entity)
        {
            _spriteBatch.Begin();
            entity.Render(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
