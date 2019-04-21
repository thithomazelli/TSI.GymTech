using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Entity.Models;

namespace TSI.GymTech.Entity.Configurations
{
    public class GateConfiguration
    {
        private CultureInfo _cultureInfo = new CultureInfo("pt-BR");
        private ResourceManager _resourceManager = new ResourceManager(typeof(App_LocalResources.GateStatusMessage));
        private Person _person;
        
        public GateConfiguration(Person person)
        {
            _person = person;
        }

        public GateStatusType GateStatus { get; set; }

        public string GateMessage { get; set; }

        public GateConfiguration GetGateConfiguration()
        {
            if (_person.Status != PersonStatus.Active)
            {
                GateMessage = _resourceManager.GetString("Denied", _cultureInfo);
                GateStatus = GateStatusType.Denied;
            }
            else
            {
                var lastGateStatus = _person.AccessLogs != null && _person.AccessLogs.Count() > 0 ?
                        _person.AccessLogs.LastOrDefault().AccessType : GateStatusType.AllowedBothSides;

                switch (lastGateStatus)
                {
                    case GateStatusType.AllowedEntry:
                        GateStatus = GateStatusType.AllowedExit;
                        break;

                    case GateStatusType.AllowedExit:
                        GateMessage = Enum.GetName(typeof(GateStatusMessage), GateStatusMessage.AllowedExit);
                        GateStatus = GateStatusType.AllowedEntry;
                        break;

                    default:
                        GateStatus = GateStatusType.AllowedEntry;
                        break;
                }
            }
            
            if (GateStatus == GateStatusType.AllowedEntry)
            {
                if (_person.DueDate != null)
                { 
                    TimeSpan timeSpanToExpired = Convert.ToDateTime(_person.DueDate) - Convert.ToDateTime(DateTime.Now.Date);

                    if (timeSpanToExpired.Days <= 0)
                    {
                        GateMessage = _resourceManager.GetString("Denied", _cultureInfo);
                        GateStatus = GateStatusType.Denied;
                        _person.Status = PersonStatus.Blocked;
                    }
                    else if (timeSpanToExpired.Days == 1)
                    {
                        GateMessage = _resourceManager.GetString("AllowedEntryExpiresOneDay", _cultureInfo);
                    }
                    else if (timeSpanToExpired.Days <= 30)
                    {
                        GateMessage = string.Format(_resourceManager.GetString("AllowedEntryWillBeExpired", _cultureInfo), timeSpanToExpired.Days);
                    }
                    else
                    {
                        GateMessage = _resourceManager.GetString("AllowedEntry", _cultureInfo);
                    }
                }
                else
                {
                    GateMessage = _resourceManager.GetString("AllowedEntryIncomplete", _cultureInfo);
                }
            }

            return this;
        }
    }
}
