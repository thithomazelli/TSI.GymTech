using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public class BaseModel
    {
        [Display(Description = "CreateDate", ResourceType = typeof(App_LocalResources.BaseModel))]
        public DateTime? CreateDate { get; set; }

        [ForeignKey("CreateUser")]
        public int? CreateUserId { get; set; }

        [Display(Description = "CreateUser", ResourceType = typeof(App_LocalResources.BaseModel))]
        public Person CreateUser { get; set; }

        [Display(Description = "ModifyDate", ResourceType = typeof(App_LocalResources.BaseModel))]
        public DateTime? ModifyDate { get; set; }

        [ForeignKey("ModifyUser")]
        public int? ModifyUserId { get; set; }

        [Display(Description = "ModifyUser", ResourceType = typeof(App_LocalResources.BaseModel))]
        public Person ModifyUser { get; set; }
    }
}
