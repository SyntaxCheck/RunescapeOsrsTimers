using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StorageFile
{
    public DateTime TreeStart { get; set; }
    public DateTime FruitTreeStart { get; set; }
    public DateTime HardwoodStart { get; set; }
    public DateTime HerbStart { get; set; }
    public DateTime BirdhouseStart { get; set; }
    public DateTime ThroneMaxTime { get; set; }

    public string Tree { get; set; }
    public string FruitTree { get; set; }
    public string Hardwood { get; set; }
    public string Herb { get; set; }

    public bool PlaySound { get; set; }

    public StorageFile()
    {
        PlaySound = true;
    }
}