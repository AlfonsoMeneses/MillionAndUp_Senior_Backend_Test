using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Contracts.Models
{
    public class PropertyDto
    {
        public int IdProperty { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Price { get; set; }

        public string CodeInternal { get; set; } = null!;

        public short Year { get; set; }
    }
}
