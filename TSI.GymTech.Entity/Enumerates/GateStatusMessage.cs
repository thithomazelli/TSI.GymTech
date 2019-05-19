using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum GateStatusMessage
    {
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "Denied")]
        Denied,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedEntry")]
        AllowedEntry,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedExit")]
        AllowedExit,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedEntryExpiresOneDay")]
        AllowedEntryExpiresOneDay,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedEntryWillBeExpired")]
        AllowedEntryWillBeExpired,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedEntryIncomplete")]
        AllowedEntryIncomplete
    }
    
}
