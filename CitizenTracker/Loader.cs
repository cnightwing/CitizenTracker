using System;
using System.Collections.Generic;
using System.Reflection;
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
    public class Loader : LoadingExtensionBase
    {
        public TrackerButton trackerButton;
        public RenderButton renderButton;
        public TrackerRenderManager renderManager;
        FastList<IRenderableManager> RenderManagers
        {
            get
            {
                return (FastList<IRenderableManager>)typeof(RenderManager).GetField("m_renderables", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
        }
        public TrackerPanel trackerPanel;
        public DetailPanel detailPanel;
        public FollowButton followButton;

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.LoadGame | mode == LoadMode.NewGame)
            {
                var uiView = GameObject.FindObjectOfType<UIView>();

                //Button to make the panel appear
                var infoPanel = uiView.FindUIComponent("InfoPanel");
                trackerButton = infoPanel.AddUIComponent(typeof(TrackerButton)) as TrackerButton;
                var happySprite = infoPanel.Find("Happiness");
                trackerButton.relativePosition = new Vector2
                (
                    happySprite.relativePosition.x + 32,
                    happySprite.relativePosition.y
                );

                //Button to activate icons above the heads of followed citizens
                renderButton = infoPanel.AddUIComponent(typeof(RenderButton)) as RenderButton;
                renderButton.relativePosition = new Vector2
                (
                    trackerButton.relativePosition.x + 28,
                    trackerButton.relativePosition.y
                );

                //Render manager to make that happen
                renderManager = new TrackerRenderManager();
                RenderManagers.Add(renderManager);

                //Tracker Panel, positioned flush to the right of the screen just above the main toolbar
                trackerPanel = uiView.AddUIComponent(typeof(TrackerPanel)) as TrackerPanel;
                trackerPanel.height = 760;
                trackerPanel.width = 370;
                trackerPanel.isVisible = false;
                float tfp = UIView.GetAView().GetScreenResolution().x / UIView.GetAView().GetScreenResolution().y;
                var thumbnailBar = uiView.FindUIComponent("ThumbnailBar");
                trackerPanel.transformPosition = new Vector3(tfp, 0, 0);
                trackerPanel.relativePosition = new Vector3
                (
                    trackerPanel.relativePosition.x - trackerPanel.width,
                    thumbnailBar.relativePosition.y - trackerPanel.height,
                    0
                );

                //Detail Panel, positioned just to the left of Tracker Panel, but movable
                detailPanel = uiView.AddUIComponent(typeof(DetailPanel)) as DetailPanel;
                detailPanel.height = 360;
                detailPanel.width = 360;
                detailPanel.isVisible = false;
                detailPanel.relativePosition = new Vector3
                (
                    trackerPanel.relativePosition.x - detailPanel.width - 4,
                    trackerPanel.relativePosition.y,
                    0
                );

                //Button to follow citizens put into citizen info panel
                UIComponent citizenWorldInfoPanel = uiView.FindUIComponent("(Library) CitizenWorldInfoPanel");
                followButton = citizenWorldInfoPanel.AddUIComponent(typeof(FollowButton)) as FollowButton;
                followButton.width = 32;
                followButton.height = 32;
                UIComponent smileyFace = citizenWorldInfoPanel.Find("Happiness");
                followButton.relativePosition = new Vector2
                (
                    smileyFace.relativePosition.x + (smileyFace.width / 2.0f) - 16,
                    smileyFace.relativePosition.y + 40
                );
            }
        }

        public override void OnLevelUnloading()
        {
            if (GameObject.Find("TrackerPanel") != null)
            {
                GameObject.Destroy(trackerPanel.gameObject);
                GameObject.Destroy(trackerButton.gameObject);
                GameObject.Destroy(renderButton.gameObject);
                RenderManagers.Remove(renderManager);
                GameObject.Destroy(followButton.gameObject);
                GameObject.Destroy(detailPanel.gameObject);
                CitizenList.followList.Clear();
            }
        }
    }
}
