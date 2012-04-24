using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Squick
{
    //TODO 
    public class Sprite
    {
        // Référence vers la classe du jeu
        private Game e_game;

        protected Vector2 _position;
        protected Texture2D _texture;
        protected Vector2 _speed;
        protected bool _active;

        // Position à l'écran du Sprite
        public Vector2 Position{
            get { return _position; }
            set { _position = value; }
        }

        // Texture du Sprite
        public Texture2D Texture{
            get { return _texture; }
            set { _texture = value; }
        }

        // Vitesse
        public Vector2 Speed{
            get { return _speed; }
            set { _speed = value; }
        }

        // Largeur de l'image
        public int Width{
            get { return _texture.Width; }
        }

        // Hauteur de l'image
        public int Height{
            get { return _texture.Height; }
        }

        // Définie si le sprite est actif
        public bool Active{
            get { return _active; }
            set { _active = value; }
        }

        // Objet Game
        public Game Game{
            get { return e_game; }
        }

        // ContentManager pour charger les ressources
        public ContentManager Content{
            get { return e_game.Content; }
        }

        #region Constructeurs
            public Sprite(Game game) { }
            public Sprite(Game game, Vector2 position) : this(game) { }
        #endregion

        // --- Schéma classique du pattern GameState
        public virtual void Initialize() { }

        public virtual void LoadContent(string textureName) { }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_active)
                spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
