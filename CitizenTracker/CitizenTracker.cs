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
//Need to get all citizen info. and hook up goto buttons
//Need to implement save feature
//Need to add panels on load data too
//Need to tidy up panel