using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Contracts.Models
{
    /// <summary>
    /// Property Owner Data Transfer Object
    /// </summary>
    public class OwnerDto
    {
        /// <summary>
        /// Owner ID
        /// </summary>
        public int IdOwner { get; set; }

        /// <summary>
        /// Owner Name
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Owner Address
        /// </summary>
        public string Address { get; set; } = null!;

        /// <summary>
        /// Owner Photo
        /// </summary>
        public string? Photo { get; set; }

        /// <summary>
        /// Owner Birthdat
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}
