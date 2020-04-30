using System;
using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Models
{
    public abstract class BaseModel
    {
        [Display(Name = "CreateDate", ResourceType = typeof(App_LocalResources.BaseModel))]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "CreateUser", ResourceType = typeof(App_LocalResources.BaseModel))]
        public int CreateUserId { get; set; }
        
        [Display(Name = "ModifyDate", ResourceType = typeof(App_LocalResources.BaseModel))]
        [DataType(DataType.DateTime)]
        public DateTime? ModifyDate { get; set; }
        
        [Display(Name = "ModifyUser", ResourceType = typeof(App_LocalResources.BaseModel))]
        public int ModifyUserId { get; set; }
    }
}
