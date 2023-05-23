using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Contracts.Models
{
    public class PropertyImageDto
    {
        public int IdPropertyImage { get; set; }

        public string Image { get; set; } = null!;

        public short Enabled { get; set; }

        //public PropertyDto Property { get; set; } = null!;
    }
}
