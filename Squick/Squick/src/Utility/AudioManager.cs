using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Squick.Utility
{
    public static class AudioManager
    {
        private static ContentManager _content;
     
        // Global states
        private static bool _isMusicPlaying;
        private static SoundEffectInstance _musicInstance;

        // Sounds
        public static SoundEffect sound_boom;
        public static SoundEffect sound_bounce;
        public static SoundEffect sound_hurt;
        public static SoundEffect sound_jump;

      
        public static void Initialize(ContentManager content)
        {
            _content = content;
            _isMusicPlaying = false;

            // Sounds
            sound_boom = _content.Load<SoundEffect>("Sounds\\boom");
            sound_bounce = _content.Load<SoundEffect>("Sounds\\bounce");
            sound_hurt = _content.Load<SoundEffect>("Sounds\\hurt");
            sound_jump = _content.Load<SoundEffect>("Sounds\\jump");

            // Musics

        }

        public static void PlaySound(SoundEffect se)
        {
            se.Play();
        }

        public static void PlayMusic(SoundEffect m)
        {
            // Stop any previous music
            if (_isMusicPlaying)
                _musicInstance.Stop();

            _musicInstance = m.CreateInstance();
            _musicInstance.IsLooped = true;
            _isMusicPlaying = true;
        }

        public static void StopMusic()
        {
            // Stop any previous music
            if (_isMusicPlaying)
                _musicInstance.Stop();
        }
    }
}
