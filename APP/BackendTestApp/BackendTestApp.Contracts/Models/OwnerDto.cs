using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Contracts.Models
{
    public class OwnerDto
    {
        public int IdOwner { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Photo { get; set; }

        public DateTime Birthday { get; set; }
    }
}
