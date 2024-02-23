using UnityEngine;

namespace Template.Scripts
{
    public class HapticManager : PersistentSingleton<HapticManager>
    {
        protected override void Initialize()
        {
            base.Initialize();
            
            bool currentHapticState = SaveManager.instance.saveData.GetHaptic();
            MoreMountains.NiceVibrations.MMVibrationManager.SetHapticsActive(currentHapticState);
        }

        public void PlayHaptic(MoreMountains.NiceVibrations.HapticTypes type)
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(type);
        }
    }
}