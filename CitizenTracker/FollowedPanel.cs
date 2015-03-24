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
        public InstanceID ID;

        string Name;
        string AgeEducation;
        string Home;
        InstanceID homeID;
        string Work;
        InstanceID workID;
        string Status;
        string Status2;
        string Target;
        InstanceID targetID;
        int Happiness;

        CitizenManager cManager = Singleton<CitizenManager>.instance;
        BuildingManager bManager = Singleton<BuildingManager>.instance;

        //Major inclusions
        UIButton nameButton;
        UILabel ageeduLabel;
        UIButton homeButton;
        UIPanel workPanel;
        UIButton workButton;
        UILabel workLabel;
        UIPanel statusPanel;
        UILabel statusLabel;
        UIButton targetButton;
        UIPanel happinessPanel;
        UISprite happinessIcon;
        
        public override void Start()
        {
            this.height = 36;
            this.width = 1076;
            this.backgroundSprite = "ListItemHover";
            this.autoLayoutDirection = LayoutDirection.Horizontal;
            this.autoLayoutStart = LayoutStart.BottomLeft;
            this.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            this.autoLayout = true;

            happinessPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;

            happinessPanel.width = 36;
            happinessPanel.height = 36;
            happinessPanel.autoLayoutDirection = LayoutDirection.Vertical;
            happinessPanel.autoLayoutStart = LayoutStart.TopLeft;
            happinessPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            happinessPanel.autoLayout = true;

            happinessIcon = happinessPanel.AddUIComponent(typeof(UISprite)) as UISprite;

            happinessIcon.width = 24;
            happinessIcon.height = 24;

            nameButton = this.AddUIComponent(typeof(UIButton)) as UIButton;

            nameButton.width = 160;
            nameButton.height = 36;
            nameButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            nameButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            nameButton.textColor = new Color32(7, 132, 255, 255);
            nameButton.disabledTextColor = new Color32(7, 7, 7, 255);
            nameButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            nameButton.focusedTextColor = new Color32(7, 132, 255, 255);
            nameButton.pressedTextColor = new Color32(30, 30, 44, 255);
            nameButton.eventClick += GoToCitizen;

            ageeduLabel = this.AddUIComponent(typeof(UILabel)) as UILabel;

            ageeduLabel.autoSize = false;
            ageeduLabel.width = 200;
            ageeduLabel.height = 36;
            ageeduLabel.textAlignment = UIHorizontalAlignment.Left;
            ageeduLabel.verticalAlignment = UIVerticalAlignment.Middle;

            homeButton = this.AddUIComponent(typeof(UIButton)) as UIButton;

            homeButton.width = 200;
            homeButton.height = 36;
            homeButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            homeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            homeButton.textColor = new Color32(7, 132, 255, 255);
            homeButton.disabledTextColor = new Color32(7, 7, 7, 255);
            homeButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            homeButton.focusedTextColor = new Color32(7, 132, 255, 255);
            homeButton.pressedTextColor = new Color32(30, 30, 44, 255);
            homeButton.eventClick += GoToHome;

            workPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;

            workPanel.width = 240;
            workPanel.height = 36;
            workPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            workPanel.autoLayoutStart = LayoutStart.BottomLeft;
            workPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            workPanel.autoLayout = true;

            workLabel = workPanel.AddUIComponent(typeof(UILabel)) as UILabel;

            workLabel.textAlignment = UIHorizontalAlignment.Left;
            workLabel.verticalAlignment = UIVerticalAlignment.Middle;

            workButton = workPanel.AddUIComponent(typeof(UIButton)) as UIButton;

            workButton.autoSize = true;
            workButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            workButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            workButton.textColor = new Color32(7, 132, 255, 255);
            workButton.disabledTextColor = new Color32(7, 7, 7, 255);
            workButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            workButton.focusedTextColor = new Color32(7, 132, 255, 255);
            workButton.pressedTextColor = new Color32(30, 30, 44, 255);
            workButton.eventClick += GoToWork;

            statusPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;

            statusPanel.width = 240;
            statusPanel.height = 36;
            statusPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            statusPanel.autoLayoutStart = LayoutStart.BottomLeft;
            statusPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            statusPanel.autoLayout = true;

            statusLabel = statusPanel.AddUIComponent(typeof(UILabel)) as UILabel;

            statusLabel.textAlignment = UIHorizontalAlignment.Left;
            statusLabel.verticalAlignment = UIVerticalAlignment.Middle;

            targetButton = statusPanel.AddUIComponent(typeof(UIButton)) as UIButton;

            targetButton.autoSize = true;
            targetButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            targetButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            targetButton.textColor = new Color32(7, 132, 255, 255);
            targetButton.disabledTextColor = new Color32(7, 7, 7, 255);
            targetButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            targetButton.focusedTextColor = new Color32(7, 132, 255, 255);
            targetButton.pressedTextColor = new Color32(30, 30, 44, 255);
            targetButton.eventClick += GoToTarget;
        }

        public override void Update()
        {
            uint citizen = ID.Citizen;
            CitizenInfo citizenInfo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);

            //Name
            Name = cManager.GetCitizenName(citizen);
            nameButton.text = this.Name;
            
            //Age and Education
            AgeEducation = Locale.Get("CITIZEN_AGEEDUCATION",cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].EducationLevel.ToString() + Citizen.GetAgeGroup(cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Age));
            ageeduLabel.text = this.AgeEducation;

            //Home
            ushort homeNo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            if (homeNo != 0)
            {
                homeID.Building = homeNo;
                Home = bManager.GetBuildingName(homeNo, ID);
                homeButton.text = this.Home;
                homeButton.Enable();
            }
            else
            {
                homeButton.text = "Homeless";
                homeButton.Disable();
            }

            //Work
            ushort workNo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_workBuilding;
            ItemClass.Level schoolLevel = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCurrentSchoolLevel(citizen);
            if (schoolLevel != ItemClass.Level.None)
            {
                workLabel.text = "Student: ";
            }
            else if(workNo == 0)
            {
                workLabel.text = "Unemployed";
            }
            else
            {
                workLabel.text = "Worker: ";
            }
            if(workNo != 0)
            {
                workID.Building = workNo;
                Work = bManager.GetBuildingName(workNo, ID);
                workButton.text = this.Work;
                workButton.isVisible = true;
            }
            else
            {
                workButton.isVisible = false;
            }

            //Status
            Status = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)], out targetID);
            ushort citizenNo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
            Status2 = citizenInfo.m_citizenAI.GetLocalizedStatus(citizenNo, ref cManager.m_instances.m_buffer[(int)((UIntPtr)citizenNo)], out targetID);
            if(Status == "Confused")
            {
                statusLabel.text = this.Status2;
            }
            else
            {
                statusLabel.text = this.Status;
            }
            
            if (targetID.IsEmpty)
            {
                targetButton.isVisible = false;
            }
            else
            {
                Target = bManager.GetBuildingName(targetID.Building, ID);
                targetButton.text = " " + this.Target;
                targetButton.isVisible = true;
            }

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
        }

        private void GoToCitizen(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(ID,ToolsModifierControl.cameraController.transform.position,true);
        }

        private void GoToHome(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(homeID, ToolsModifierControl.cameraController.transform.position, true);
        }

        private void GoToWork(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(workID, ToolsModifierControl.cameraController.transform.position, true);
        }

        private void GoToTarget(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(targetID, ToolsModifierControl.cameraController.transform.position, true);
        }
    }
}
