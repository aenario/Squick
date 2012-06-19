using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

using Squick.Control;
using Squick.Component;
using Squick.Component.Player;
using Squick.Component.Collectible;
using Squick.Scene;
using Squick.Scene.Effects;
using Squick.Utility;
using Squick.UI;
using Squick.Scene.Menus;
using Squick.src.Utility;

namespace Squick.Scene.Levels
{
    public class Level1 : Scene{

        private const int BONUS_CHAIN_VALUE = 5; // Number of consecutive bonus to collect before getting a score bonus

        //private static int GravitySpeed = 50;
        private static int leftBound = 30;
        private static int rightBound = 770;

        // HUD
        private Score _HUD_score;
        private TextButton _backToMenu;
        private Message _gameEventMessage;
        private Message _gameScoreMessage;

        // Special effects
        private Fade _bombEffect;
        
        // Game components
        private Texture2D _levelBackground;
        private DizzySquick squick;
        private List<Entity> items = new List<Entity>();
    
        // Player score
        private int _score;
        private int _bonusChain;
            
        // Others
        private bool justStarted = true;// CHANGE TO COMPLEXE ENTITY
        private double _eventTimer;
        private bool _modeAdventure;
       
        //private Player _squick;

        public Level1(KinectInterface gameInput, bool modeAdventure = false)
        {
            _levelBackground = ResourceManager.tex_background_level1;
            _modeAdventure = modeAdventure;

            _bombEffect = new Fade();

            _HUD_score = new Score(new Vector2(10,0));
            _backToMenu = new TextButton("Menu", new Vector2(690, 10));

            squick = new DizzySquick(gameInput);
            squick.Pos = new Vector2(400, 400);

            _gameEventMessage = new Message("Ready?  Set...  Go!", new Vector2(20, 220),2.0f,Message.DISPLAY_LBL,250);
            _gameScoreMessage = new Message("",new Vector2(10,160),1.0f,Message.DISPLAY_LBL,1000);

            
            // Level score
            _score = 0;
            _bonusChain = 0;
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            _backToMenu.Update(gameTime, gameInput);
            if (_backToMenu.IsPressed())
            {
                _nextScene = new MainMenu(); 
                _sceneFinished = true;
            }

            /* TODO : Clean up all below (_step ?) */
            if (justStarted)
            {
                Level1CollectibleFactory.startNow(gameTime);
                justStarted = false;
                _eventTimer = gameTime.TotalGameTime.TotalSeconds;
            }
            if (Level1CollectibleFactory.doneSince(gameTime) > 10 & Level1CollectibleFactory.doneSince(gameTime) < 10.1)
            {
                _gameEventMessage.SetText("LEVEL CLEAR");
                squick.isDizzy = false;
                if(_modeAdventure) squick.SpeedY = -600;
                _bombEffect.Start(Fade.EFFECT.FADE_IN, Color.Black, 4000.0f);
            }
            if (Level1CollectibleFactory.doneSince(gameTime) > 15)
            {
                ScoreHolder.Level1 = _score;
                if (_modeAdventure) _nextScene = new Level2(gameInput);
                else _nextScene = new VictoryMenu();
                _sceneFinished = true;
            }

            // spawn items 
            foreach (EntityFactory se in Level1CollectibleFactory.getSpawnAt(gameTime))
            {
                Entity item = se.asEntity();
                //item.Speed = new Vector2(0, GravitySpeed);
                items.Add(item);
            }
            
            // destroy items below screen
            List<Collectible> toBeDestroy = new List<Collectible>();
            foreach (Collectible item in items) // quick & dirty
            {
                item.Update(gameTime);

                if (item.Pos.Y > 600)
                {
                    toBeDestroy.Add(item);
                    continue;
                }

                // Collisions
                if (item.GetBoundingBox().Intersects(squick.GetBoundingBox()))
                {
                    // Update internal state
                    item.CollideWithPlayer(true);

                    // BONUS
                    if (item.GetBonus() > 0)
                    {
                        _bonusChain++;
                        _score += this.ComputeBonus(_bonusChain) * item.GetBonus();
                        
                        // Display bonus text
                        if (this._bonusChain % BONUS_CHAIN_VALUE == 0)
                            _gameScoreMessage.SetText(_bonusChain.ToString() + " in a row!");

                    }
                    else
                    {
                        _bonusChain = 0;
                        _gameScoreMessage.SetText("");
                        _score += item.GetBonus();
                    }

                    // ADDITIONAL EFFECTS
                    // . Bomb effect
                    if (item is Bomb)
                    {
                        _bombEffect.Start(Fade.EFFECT.FADE_OUT, Color.White, 200.0f);
                    }

                    // Destroy item
                    toBeDestroy.Add(item);
                }

                
                // Remove if under the ground

                
            }

            foreach (Collectible i in toBeDestroy) { i.Destroy(); items.Remove(i); }
            
            /* Make squick bump */
            if ((squick.Pos.X < 36 && squick.Speed.X < leftBound)
                 || (squick.Pos.X + squick.Width > rightBound && squick.Speed.X > 0))
            {
                // Bounce
                squick.SpeedX *= -1;
                // Sound
                AudioManager.PlaySound(AudioManager.sound_bounce);
            }
            /* INGAME EVENTS */
            double time = Math.Round(gameTime.TotalGameTime.TotalSeconds - _eventTimer, 2);
            // Wave 1
            if (time == (double)Level1CollectibleFactory.WAVE_1)
            {
                _gameEventMessage.SetText("");
                _gameEventMessage.SetMode(Message.DISPLAY_BLINK, 250);
            }
            // Wave 2
            if (time == (double)Level1CollectibleFactory.WAVE_2 - 1)
                _gameEventMessage.SetText("     Warning!!");
            if (time == (double)Level1CollectibleFactory.WAVE_2 + 2)
                _gameEventMessage.SetText("");
            // Wave 3
            if (time == (double)Level1CollectibleFactory.WAVE_3 - 3)
                _gameEventMessage.SetText("     Speed-up!");
            if (time == (double)Level1CollectibleFactory.WAVE_3)
                _gameEventMessage.SetText("");
            // Wave 4
            if (time == (double)Level1CollectibleFactory.WAVE_4 - 1)
                _gameEventMessage.SetText("     Warning!");
            if (time == (double)Level1CollectibleFactory.WAVE_4 + 2)
                _gameEventMessage.SetText("");
            // Wave 5
            if (time == (double)Level1CollectibleFactory.WAVE_5 - 3)
                _gameEventMessage.SetText("     Stay focused!");
            if (time == (double)Level1CollectibleFactory.WAVE_5)
                _gameEventMessage.SetText("");
            // Wave 6
            if (time == (double)Level1CollectibleFactory.WAVE_6 - 1)
                _gameEventMessage.SetText("     Warning!");
            if (time == (double)Level1CollectibleFactory.WAVE_6 + 2)
                _gameEventMessage.SetText("");
            // Wave 7
            if (time == (double)Level1CollectibleFactory.WAVE_7 - 3)
                _gameEventMessage.SetText("     Speed-up!");
            if (time == (double)Level1CollectibleFactory.WAVE_7)
                _gameEventMessage.SetText("");
           


            // HUD update
            _HUD_score.Update(_score,this.ComputeBonus(_bonusChain));
            _gameEventMessage.Update(gameTime);
            _gameScoreMessage.Update(gameTime);

            // Special effects
            _bombEffect.Update(gameTime);

            squick.Update(gameTime);
        }

        public override void Render(GameTime gameTime)
        {
            // Background
            RenderManager.Draw2DTexture(_levelBackground, _levelBackground.Bounds, Color.White);

            _backToMenu.Render(gameTime);

            // Components
            squick.Render(gameTime);
            foreach (Entity item in items)
            {
                item.Render(gameTime);
            }

            // HUD
            _HUD_score.Render(gameTime);
            _gameEventMessage.Render(gameTime);
            _gameScoreMessage.Render(gameTime);

            // Special effects
            _bombEffect.Render(gameTime);
        }

        public int ComputeBonus(int bonusChain)
        {
            return ((int)bonusChain / BONUS_CHAIN_VALUE + 1);
        }

    }
}
