using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using UnityEngine;

namespace CitizenTracker
{
    public class FollowedPanel : UIPanel
    {
        public InstanceID instanceID;

        int Happiness;
        string Name;
        string District;
        string Status;
        string Target;
        InstanceID targetID;
        
        CitizenManager cManager = Singleton<CitizenManager>.instance;
        BuildingManager bManager = Singleton<BuildingManager>.instance;
        DistrictManager dManager = Singleton<DistrictManager>.instance;
        SimulationManager sManager = Singleton<SimulationManager>.instance;

        //Panel to contain and center the detail button
        UIPanel detailPanel;
        UIButton detailButton;

        //Panel to contain and center the happiness sprite
        UIPanel happinessPanel;
        UISprite happinessIcon;

        //Panel to hold text items so that there's proper padding at the top and bottom
        UIPanel paddingPanel;

        //Panel to contain the name, type and activity
        UIPanel summaryPanel;
        UIButton nameButton;
        UILabel summaryLabel;
        UIPanel statusPanel;
        UILabel statusLabel;
        UIButton targetButton;

        //Panel to contain the delete button
        UIPanel deletePanel;
        UIButton deleteButton;        
        
        public override void Start()
        {
            this.width = 360;
            this.height = 72;
            this.backgroundSprite = "InfoPanelBack";
            this.autoLayoutDirection = LayoutDirection.Horizontal;
            this.autoLayoutStart = LayoutStart.TopLeft;
            this.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            this.autoLayout = true;
            
            //Detail button panel
            detailPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;
            detailPanel.width = 32;
            detailPanel.height = 72;
            detailPanel.autoLayoutDirection = LayoutDirection.Vertical;
            detailPanel.autoLayoutStart = LayoutStart.TopLeft;
            detailPanel.autoLayoutPadding = new RectOffset(0, 0, 20, 20);
            detailPanel.autoLayout = true;

            detailButton = detailPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            detailButton.width = 32;
            detailButton.height = 32;
            detailButton.normalFgSprite = "ArrowLeftFocused";
            detailButton.hoveredFgSprite = "ArrowLeftHovered";
            detailButton.pressedFgSprite = "ArrowLeftPressed";
            detailButton.eventClick += ShowDetail;

            //Happiness panel
            happinessPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;
            happinessPanel.width = 40;
            happinessPanel.height = 72;
            happinessPanel.autoLayoutDirection = LayoutDirection.Vertical;
            happinessPanel.autoLayoutStart = LayoutStart.TopLeft;
            happinessPanel.autoLayoutPadding = new RectOffset(0, 8, 20, 20);
            happinessPanel.autoLayout = true;

            happinessIcon = happinessPanel.AddUIComponent(typeof(UISprite)) as UISprite;
            happinessIcon.width = 32;
            happinessIcon.height = 32;

            //Padding and summary panel
            paddingPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;
            paddingPanel.width = 248;
            paddingPanel.height = 72;
            paddingPanel.autoLayoutDirection = LayoutDirection.Vertical;
            paddingPanel.autoLayoutStart = LayoutStart.TopLeft;
            paddingPanel.autoLayoutPadding = new RectOffset(0, 0, 6, 6);
            paddingPanel.autoLayout = true;

            summaryPanel = paddingPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            summaryPanel.width = 248;
            summaryPanel.height = 60;
            summaryPanel.autoLayoutDirection = LayoutDirection.Vertical;
            summaryPanel.autoLayoutStart = LayoutStart.TopLeft;
            summaryPanel.autoLayoutPadding = new RectOffset(0, 0, 1, 1);
            summaryPanel.autoLayout = true;

            nameButton = summaryPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            nameButton.width = 248;
            nameButton.height = 18;
            nameButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            nameButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            nameButton.textColor = new Color32(55, 148, 200, 255);
            nameButton.disabledTextColor = new Color32(7, 7, 7, 255);
            nameButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            nameButton.focusedTextColor = new Color32(55, 148, 200, 255);
            nameButton.pressedTextColor = new Color32(2, 48, 61, 255);
            nameButton.eventClick += GoToCitizen;

            summaryLabel = summaryPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            summaryLabel.autoSize = false;
            summaryLabel.width = 248;
            summaryLabel.height = 18;
            summaryLabel.textAlignment = UIHorizontalAlignment.Left;
            summaryLabel.verticalAlignment = UIVerticalAlignment.Middle;

            statusPanel = summaryPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            statusPanel.width = 248;
            statusPanel.height = 18;
            statusPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            statusPanel.autoLayoutStart = LayoutStart.TopLeft;
            statusPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            statusPanel.autoLayout = true;

            statusLabel = statusPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            statusLabel.textAlignment = UIHorizontalAlignment.Left;
            statusLabel.verticalAlignment = UIVerticalAlignment.Middle;

            targetButton = statusPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            targetButton.autoSize = true;
            targetButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            targetButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            targetButton.textColor = new Color32(55, 148, 200, 255);
            targetButton.disabledTextColor = new Color32(7, 7, 7, 255);
            targetButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            targetButton.focusedTextColor = new Color32(55, 148, 200, 255);
            targetButton.pressedTextColor = new Color32(2, 48, 61, 255);
            targetButton.eventClick += GoToTarget;

            //Delete button panel
            deletePanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;
            deletePanel.width = 40;
            deletePanel.height = 72;
            deletePanel.autoLayoutDirection = LayoutDirection.Vertical;
            deletePanel.autoLayoutStart = LayoutStart.TopLeft;
            deletePanel.autoLayoutPadding = new RectOffset(4, 4, 20, 20);
            deletePanel.autoLayout = true;

            deleteButton = deletePanel.AddUIComponent(typeof(UIButton)) as UIButton;
            deleteButton.width = 32;
            deleteButton.height = 32;
            deleteButton.normalFgSprite = "buttonclose";
            deleteButton.hoveredFgSprite = "buttonclosehover";
            deleteButton.pressedFgSprite = "buttonclosepressed";
            deleteButton.eventClick += Unfollow;
        }

        public override void Update()
        {
            uint citizen = instanceID.Citizen;

            if (cManager.GetCitizenName(citizen) != null)
            {
                CitizenInfo citizenInfo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);

                //Happiness
                string[] happinessLevels = new string[]
                {
                    "VeryUnhappy",
                    "Unhappy",
                    "Happy",
                    "VeryHappy",
                    "ExtremelyHappy"
                };
                Happiness = Citizen.GetHappiness((int)cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_health, (int)cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_wellbeing);
                happinessIcon.spriteName = "NotificationIcon" + happinessLevels[(int)Citizen.GetHappinessLevel(Happiness)];

                //Name
                Name = cManager.GetCitizenName(citizen);
                nameButton.text = this.Name;

                //Summary
                ushort homeNo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                Vector3 homePos = bManager.m_buildings.m_buffer[(int)((UIntPtr)homeNo)].m_position;
                int districtNo = (int)dManager.GetDistrict(homePos);
                if (districtNo == 0)
                {
                    District = sManager.m_metaData.m_CityName;
                }
                else
                {
                    District = dManager.GetDistrictName(districtNo);
                }
                summaryLabel.text = District + " resident";

                //Status
                Status = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)], out targetID);
                statusLabel.text = this.Status;
                if (targetID.IsEmpty)
                {
                    targetButton.isVisible = false;
                }
                else
                {
                    Target = bManager.GetBuildingName(targetID.Building, instanceID);
                    targetButton.text = this.Target;
                    targetButton.isVisible = true;
                }
            }
            else
            {
                statusLabel.text = "Dead or moved away";
                targetButton.isVisible = false;
                happinessIcon.spriteName = "NotificationIconDead";
                detailButton.isVisible = false;
            }
        }

        private void ShowDetail(UIComponent component, UIMouseEventParameter p)
        {
            var uiView = GameObject.FindObjectOfType<UIView>();
            var trackerPanel = uiView.FindUIComponent("TrackerPanel");
            DetailPanel detailPanel = uiView.GetComponentInChildren<DetailPanel>();
            if (!detailPanel.isVisible)
            {
                detailPanel.relativePosition = new Vector2
                (
                    trackerPanel.relativePosition.x - 364,
                    trackerPanel.relativePosition.y
                );
                detailPanel.isVisible = true;
            }
            detailPanel.instanceID = this.instanceID;
        }

        private void GoToCitizen(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(instanceID,ToolsModifierControl.cameraController.transform.position,true);
        }

        private void GoToTarget(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(targetID, ToolsModifierControl.cameraController.transform.position, true);
        }

        private void Unfollow(UIComponent component, UIMouseEventParameter p)
        {
            var uiView = GameObject.FindObjectOfType<UIView>();
            DetailPanel detailPanel = uiView.GetComponentInChildren<DetailPanel>();
            if (detailPanel.instanceID == this.instanceID)
            {
                detailPanel.isVisible = false;
            }
            CitizenList.followList.Remove(instanceID);
            Destroy(this);
        }
    }
}
