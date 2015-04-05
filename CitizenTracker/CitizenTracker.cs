using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ICities;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using UnityEngine;

namespace CitizenTracker
{
    public class CitizenTracker : IUserMod
    {
        public string Name
        {
            get { return "Citizen Tracker"; }
        }

        public string Description
        {
            get { return "Follow your favourite citizens around town!"; }
        }
    }
}

//Notes to self:
//Put detail minipanels into their own classes
//See if other things can be sensibly followed
//Add a kill button?
//Look to see what other information would be interesting
//Find a way to better place the render icons