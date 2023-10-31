
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    public string Name { get; set; }
    public virtual List<EngineerMachine> JoinEntities { get; set; }
    public string Speciality { get; set; }
  }
}