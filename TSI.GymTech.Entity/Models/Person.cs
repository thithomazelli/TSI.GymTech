using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public class Person : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "PersonIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Description = "PersonId", ResourceType = typeof(App_LocalResources.Person))]
        public int PersonId { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessageResourceName = "NameValidate", ErrorMessageResourceType = typeof(App_LocalResources.Person))]
        [Display(Description = "Name", ResourceType = typeof(App_LocalResources.Person))]
        [StringLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ProfileTypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Description = "ProfileType", ResourceType = typeof(App_LocalResources.Person))]
        public PersonType ProfileType { get; set; }

        [DataType(DataType.Password)]
        [Display(Description = "Password", ResourceType = typeof(App_LocalResources.Person))]
        [StringLength(16, MinimumLength = 4, ErrorMessageResourceName = "PasswordValidate", ErrorMessageResourceType = typeof(App_LocalResources.Person))]
        public string Password { get; set; }

        [Display(Description = "Gender", ResourceType = typeof(App_LocalResources.Person))]
        public GenderType? Gender { get; set; }

        [Display(Description = "NationalIDCard", ResourceType = typeof(App_LocalResources.Person))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:##.###.###-#}")]
        [StringLength(16)]
        public string NationalIDCard { get; set; }

        [Required(ErrorMessageResourceName = "SocialSecurityCardRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Description = "SocialSecurityCard", ResourceType = typeof(App_LocalResources.Person))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###.###.###-##}")]
        [StringLength(16)]
        public string SocialSecurityCard { get; set; }

        [Display(Description = "BirthDate", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [Display(Description = "RegistrationDate", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }

        [Display(Description = "DueDate", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessageResourceName = "StatusRequired", ErrorMessageResourceType = typeof(App_LocalResources.Person), AllowEmptyStrings = false)]
        [Display(Description = "Status", ResourceType = typeof(App_LocalResources.Person))]
        public PersonStatus Status { get; set; }

        [Display(Description = "Photo", ResourceType = typeof(App_LocalResources.Person))]
        [StringLength(64)]
        public string Photo { get; set; }

        [Display(Description = "Comments", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        [Display(Description = "Phone", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.PhoneNumber)]
        [StringLength(32)]
        public string Phone { get; set; }

        [Display(Description = "MobilePhone", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.PhoneNumber)]
        [StringLength(32)]
        public string MobilePhone { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessageResourceName = "EmailValidate", ErrorMessageResourceType = typeof(App_LocalResources.Person))]
        [Display(Description = "Email", ResourceType = typeof(App_LocalResources.Person))]
        [DataType(DataType.EmailAddress)]
        [StringLength(64)]
        public string Email { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        [NotMapped]
        public virtual int StudentId
        {
            get { return PersonId; }
            set { StudentId = PersonId; }
        }

        [NotMapped]
        public virtual int TrainerId
        {
            get { return PersonId; }
            set { TrainerId = PersonId; }
        }
    }
}    
