using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    public Machine()
    {
      this.JoinEntities = new HashSet<EngineerMachine>();
    }
    public int MachineId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public bool Beverage { get; set; }
    [Required]
    public bool Food { get; set; }
    [Required]
    public bool Retail {get; set; }
    [Required]
    public DateTime LastInspection { get; set; }
    public virtual ICollection<EngineerMachine> JoinEntities { get ; set ; }

  }
}