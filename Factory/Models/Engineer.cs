using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    public Engineer()
    {
      this.JoinEntities = new HashSet<Engineer>();
    }

    public int EngineerId { get; set; }
    [Required]
    public string EngineerName { get; set; }
    public virtual ICollection<Engineer> JoinEntities { get; set; }
  }
}