using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace ApropasTaskManager.Shared
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; }
        [JsonIgnore] public virtual User User { get; set; }


        public string Name { get; set; } 
        public string Surname { get; set; }
        public string MiddleName { get; set; }
    }
}
