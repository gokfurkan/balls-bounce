using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class PanelManager : Singleton<PanelManager>
    {
        private List<PanelTypeHolder> allPanels = new List<PanelTypeHolder>();

        protected override void Initialize()
        {
            base.Initialize();
            
            InitializePanelSystem();
        }

        private void OnEnable()
        {
            BusSystem.OnLevelStart += ActivateGamePanel;
            BusSystem.OnLevelEnd += ActivateEndPanel;
        }

        private void OnDisable()
        {
            BusSystem.OnLevelStart -= ActivateGamePanel;
            BusSystem.OnLevelEnd -= ActivateEndPanel;
        }

        private void InitializePanelSystem()
        {
            GetAllPanels();
            ActivateMenuPanel();
        }

        private void ActivateMenuPanel()
        {
            DisableAll();
            
            Activate(PanelType.Money);
            Activate(PanelType.Level);
            Activate(PanelType.OpenSettings);
            Activate(PanelType.Restart);
        }

        private void ActivateGamePanel()
        {
            
        }

        private void ActivateEndPanel(bool win)
        {
            DisableAll();
            
            Activate(PanelType.Money);
            
            StartCoroutine(ActivateEndPanelDelay(win));
        }
        
        private IEnumerator ActivateEndPanelDelay(bool win)
        {
            Activate(PanelType.EndContinue , false);
            
            if (win)
            {
                yield return new WaitForSeconds(InitializeManager.instance.gameSettings.uiOptions.winPanelDelay);
                
                Activate(PanelType.Win);
                
                BusSystem.CallAddMoneys(InitializeManager.instance.gameSettings.economyOptions.winIncome);
                BusSystem.CallSpawnMoneys();
            }
            else
            {
                yield return new WaitForSeconds(InitializeManager.instance.gameSettings.uiOptions.losePanelDelay);
                
                Activate(PanelType.Lose);
                
                BusSystem.CallAddMoneys(InitializeManager.instance.gameSettings.economyOptions.loseIncome);
            }
            
            yield return new WaitForSeconds(InitializeManager.instance.gameSettings.uiOptions.endContinueDelay);
                
            Activate(PanelType.EndContinue);
        }

        public void ActivateSettingsPanel()
        {
            Activate(PanelType.OpenSettings , false);
            Activate(PanelType.Settings);
        }

        public void DeActivateSettingsPanel()
        {
            Activate(PanelType.Settings , false);
            Activate(PanelType.OpenSettings);
        }

        public void ActivateDevPanel()
        {
            
        }

        public void LoadLevel()
        {
            BusSystem.CallLevelLoad();
        }
        
        public void Activate(PanelType panelType, bool activate = true)
        {
            List<PanelTypeHolder> panels = FindPanels(panelType);

            if (panels != null)
            {
                for (int i = 0; i < panels.Count; i++)
                {
                    panels[i].gameObject.SetActive(activate);
                }
            }
            else
            {
                Debug.LogWarning("Panel not found: " + panelType.ToString());
            }
        }
        
        public void DisableAll()
        {
            foreach (var panel in allPanels)
            {
                panel.gameObject.SetActive(false);
            }
        }
        
        private List<PanelTypeHolder> FindPanels(PanelType panelType)
        {
            return allPanels.FindAll(panel => panel.panelType == panelType);
        }
        
        private void GetAllPanels()
        {
            allPanels = transform.root.GetComponentsInChildren<PanelTypeHolder>(true).ToList();
        }
    }
}