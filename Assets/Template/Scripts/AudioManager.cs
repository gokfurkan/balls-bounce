using UnityEngine;
using AudioType = Game.Dev.Scripts.AudioType;

namespace Template.Scripts
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] private AudioClip[] effects;
        [SerializeField] private AudioSource source;

        protected override void Initialize()
        {
            base.Initialize();
            
            bool currentSoundState = SaveManager.instance.saveData.GetSound();
            AudioListener.volume = currentSoundState ? 1 : 0;
            
            //test
            Play(AudioType.GameStart);
        }

        public void Play(AudioType type)
        {
            source.PlayOneShot(effects[(int)type]);
        }
    }
}