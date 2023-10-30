using System.Collections.Generic;
using System;

namespace Factory.Models
{
  public class Machine
  
  {

    public int MachineId { get; set; }
  
    public string Name { get; set; }

    public string Descritpion { get; set; }

    public List<EngineerMachine> JoinEntities { get; set; }
    }
    
}