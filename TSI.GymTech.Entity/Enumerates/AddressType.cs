using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum AddressType
    {
        [Display(ResourceType = typeof(App_LocalResources.AddressType), Name = "Home")]
        Home = 0,
        [Display(ResourceType = typeof(App_LocalResources.AddressType), Name = "Office")]
        Office = 1,
        [Display(ResourceType = typeof(App_LocalResources.AddressType), Name = "Billing")]
        Billing = 2,
        [Display(ResourceType = typeof(App_LocalResources.AddressType), Name = "Collection")]
        Collection = 3
    }
}
