using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Model
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public ICollection<UserTask> Tasks { get; set; }
    }
}
