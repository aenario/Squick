using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

using Squick.Component;

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
        private static Boolean _isRendering;

        public static void Initialize(GraphicsDevice device)
        {
            _device = device;

            _spriteBatch = new SpriteBatch(_device);
            _screenBounds = new Rectangle(0, 0, _device.Viewport.Width, _device.Viewport.Height);
            _screenCenter = new Vector2(_device.Viewport.Width / 2, _device.Viewport.Height / 2);

            _backgroundRenderTarget = new RenderTarget2D(_device, _screenBounds.Width, _screenBounds.Height);
            _foregroundRenderTarget = new RenderTarget2D(_device, _screenBounds.Width, _screenBounds.Height);

            _isRendering = false;
        }


        /**
         * TODO: DRAWING QUEUES
         * ie: Add2DTexture or something, to avoid multiple Begin and End
         **/

        public static void StartRendering()
        {
            if (_isRendering)
                return;
            _isRendering = true;
            _spriteBatch.Begin();
        }

        public static void EndRendering()
        {
            if (!_isRendering)
                return;
            _isRendering = false;
            _spriteBatch.End();
        }


        public static void DrawString(SpriteFont font, String content, Vector2 location, Color color,float scale = 1.0f)
        {
            _spriteBatch.DrawString(font, content, location, color, 0.0f, new Vector2(0,0),scale,SpriteEffects.None,0.0f);
        }

        public static void DrawBox(Rectangle box)
        {
            DrawBox(box, Color.Red);
        }

        public static void DrawBox(Rectangle box, Color color)
        {
            _spriteBatch.Draw(ResourceManager.tex_pixel, box, color);
        }

        public static void DrawLine(Vector2 point1, Vector2 point2, Color color)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            _spriteBatch.Draw(ResourceManager.tex_pixel, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, 10f),
                       SpriteEffects.None, 0);
        }

        public static void Draw2DTexture(Texture2D tex, Rectangle box, Color color)
        {
            _spriteBatch.Draw(tex, box, color);
        }

        public static void Draw2DTexture(Texture2D tex, Vector2 pos, Color color)
        {
            _spriteBatch.Draw(tex, pos, null, color);
        }

        public static void Draw2DTexture(Texture2D tex, Vector2 pos, Color color, float armAngle, Vector2 origin)
        {
             _spriteBatch.Draw(tex, pos, null, color, armAngle, origin, 1f, SpriteEffects.None, 0);
        }

        public static void Draw2DTexture(Texture2D tex, Vector2 pos, Color color, float armAngle, Vector2 origin, Vector2 scale)
        {
            _spriteBatch.Draw(tex, pos, null, color, armAngle, origin, scale, SpriteEffects.None, 0);
        }
        public static void Draw2DTexture(Texture2D tex, Rectangle source, Vector2 pos, Color color, float armAngle, Vector2 origin, Vector2 scale)
        {
            _spriteBatch.Draw(tex, pos, source, color, armAngle, origin, scale, SpriteEffects.None, 0);
        }

     /*   public static void DrawEntity(Entity entity)
        {
            //_spriteBatch.Begin();
            //entity.Render(_spriteBatch);
            //_spriteBatch.End();
        }
      */
    }
}
