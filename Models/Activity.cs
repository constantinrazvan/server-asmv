using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAsmv.Models
{
    public class Activity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; } 
    public string Name { get; set; } = string.Empty; 
    public DateTime Date { get; set; } 
    public long CoordinatorId { get; set; } 
    public required User Coordinator { get; set; }

    public List<ActivityUser> AssignedUsers { get; set; } = new List<ActivityUser>();
}

}
