// ReSharper disable VirtualMemberCallInConstructor
namespace IDFit.Data.Models
{
    using System;
    using System.Collections.Generic;

    using IDFit.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Trainings = new List<Training>();
            this.TrainedPeople = new List<ApplicationUser>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int? DietId { get; set; }
        public virtual Diet Diet { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }

        // if user is user
        public string CoachId { get; set; }
        public virtual ApplicationUser Coach { get; set; }

        // if user is coach
        public IEnumerable<ApplicationUser> TrainedPeople { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
