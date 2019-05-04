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
        private CultureInfo _cultureInfo = new CultureInfo("pt");
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
            if (_person.ProfileType != PersonType.Student)
            {
                if (_person.Status != PersonStatus.Active)
                {
                    GateMessage = string.Format(_resourceManager.GetString("Denied", _cultureInfo), _person.PersonId, _person.Name);
                    GateStatus = GateStatusType.Denied;
                }
                else
                {
                    GateMessage = string.Format(_resourceManager.GetString("AllowedEntry", _cultureInfo), _person.PersonId, _person.Name);
                    GateStatus = GateStatusType.AllowedEntry;
                }
                    return this;
            }
            
            if (_person.Status != PersonStatus.Active)
            {
                GateMessage = string.Format(_resourceManager.GetString("Denied", _cultureInfo), _person.PersonId, _person.Name);
                GateStatus = GateStatusType.Denied;
            }
            else
            {
                GateStatus = GateStatusType.AllowedEntry;

                if (_person.DueDate != null)
                {
                    TimeSpan timeSpanToExpired = Convert.ToDateTime(_person.DueDate) - Convert.ToDateTime(DateTime.Now.Date);

                    if (timeSpanToExpired.Days <= 0)
                    {
                        GateMessage = string.Format(_resourceManager.GetString("Denied", _cultureInfo), _person.PersonId, _person.Name);
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
                        GateMessage = string.Format(_resourceManager.GetString("AllowedEntry", _cultureInfo), _person.PersonId, _person.Name);
                    }
                }
                else
                {
                    GateMessage = string.Format(_resourceManager.GetString("AllowedEntryIncomplete", _cultureInfo), _person.PersonId, _person.Name);
                }
            }
            
            return this;
        }
    }
}
