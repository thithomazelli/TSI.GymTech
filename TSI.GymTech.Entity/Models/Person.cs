using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TSI.GymTech.Entity.Configurations;
using System.Web.Script.Serialization;

namespace TSI.GymTech.Entity.Models
{
    [Table("person")]
    public class Person : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "PersonIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Name = "PersonId", ResourceType = typeof(App_LocalResources.Person))]
        public int PersonId { get; set; }

        //[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessageResourceName = "NameValidate", ErrorMessageResourceType = typeof(App_LocalResources.Person))]
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Name = "Name", ResourceType = typeof(App_LocalResources.Person))]
        [StringLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ProfileTypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Name = "ProfileType", ResourceType = typeof(App_LocalResources.Person))]
        public PersonType ProfileType { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Password", ResourceType = typeof(App_LocalResources.Person))]
        //[StringLength(16, MinimumLength = 4, ErrorMessageResourceName = "PasswordValidate", ErrorMessageResourceType = typeof(App_LocalResources.Person))]
        //public string Password { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(App_LocalResources.Person))]
        public GenderType? Gender { get; set; }

        [Display(Name = "NationalIDCard", ResourceType = typeof(App_LocalResources.Person))]
        [DisplayFormat(DataFormatString = "{0:##.###.###-#}")]
        [StringLength(16)]
        public string NationalIDCard { get; set; }

        [Required(ErrorMessageResourceName = "SocialSecurityCardRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Name = "SocialSecurityCard", ResourceType = typeof(App_LocalResources.Person))]
        [DisplayFormat(DataFormatString = "{0:###.###.###-##}")]
        [StringLength(16)]
        public string SocialSecurityCard { get; set; }

        [Display(Name = "BirthDate", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "RegistrationDate", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "DueDate", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessageResourceName = "StatusRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Name = "Status", ResourceType = typeof(App_LocalResources.Person))]
        public PersonStatus Status { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(App_LocalResources.Person))]
        [StringLength(64)]
        public string Photo { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.PhoneNumber)]
        [StringLength(32)]
        [DisplayFormat(DataFormatString = "{0:(##) ####-####}")]
        public string Phone { get; set; }

        [Display(Name = "MobilePhone", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(##) #####-####}", ApplyFormatInEditMode = true)]
        [StringLength(32)]
        public string MobilePhone { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessageResourceName = "EmailValidate", ErrorMessageResourceType = typeof(App_LocalResources.Person))]
        [Display(Name = "Email", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.EmailAddress)]
        [StringLength(64)]
        public string Email { get; set; }

        public virtual ICollection<AccessLog> AccessLogs { get; set; }
        
        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<AnamnesisSheet> AnamnesisSheets { get; set; }

        public virtual ICollection<EvaluationSheet> EvaluationSheets { get; set; }

        public virtual ICollection<TrainingSheet> TrainingSheets { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public GateConfiguration GetGateConfig()
        {
            return new GateConfiguration(this).GetGateConfiguration();
        }
    }
}    
