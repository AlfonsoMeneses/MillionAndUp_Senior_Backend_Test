using System;
using System.Collections.Generic;

namespace BackendTestApp.DataService.Entities;

public partial class PropertyImage
{
    public int IdPropertyImage { get; set; }

    public int IdProperty { get; set; }

    public string Image { get; set; } = null!;

    public short Enabled { get; set; }

    public virtual Property Property { get; set; } = null!;
}
