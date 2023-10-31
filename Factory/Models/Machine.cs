using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine

  {

    public int MachineId { get; set; }

    public string Name { get; set; }

    public string MachineDescription { get; set; }

    public List<EngineerMachine> JoinEntities { get; set; }
  }

}