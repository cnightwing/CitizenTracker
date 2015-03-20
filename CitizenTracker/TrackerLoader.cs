using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICities;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using UnityEngine;

namespace CitizenTracker
{
    public class TrackerLoader : LoadingExtensionBase
    {
        public TrackerButton trackerButton;
        public TrackerPanel trackerPanel;

        public override void OnLevelLoaded(LoadMode mode)
        {
            var uiView = GameObject.FindObjectOfType<UIView>();

            trackerButton = uiView.AddUIComponent(typeof(TrackerButton)) as TrackerButton;
            UIComponent menuButton = uiView.FindUIComponent("Esc");
            Log.Message(trackerButton.width.ToString() + "/" + trackerButton.height.ToString()); // gives 0/0
            trackerButton.relativePosition = new Vector2
            (
                menuButton.relativePosition.x + (menuButton.width / 2.0f) - 18,
                menuButton.relativePosition.y - menuButton.height - 18
            );

            trackerPanel = uiView.AddUIComponent(typeof(TrackerPanel)) as TrackerPanel;
            trackerPanel.relativePosition = new Vector2
            (
                menuButton.relativePosition.x + menuButton.width - 300,
                menuButton.relativePosition.y + menuButton.height + 36
            );

            trackerButton.eventClick += TogglePanel;
        }

        public void TogglePanel(UIComponent component, UIMouseEventParameter eventParam)
        {
            trackerPanel.isVisible = !trackerPanel.isVisible;
        }
    }
}
