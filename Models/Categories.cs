using System;
using System.Collections.Generic;

namespace Models;
public class Categories
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Tasks>? Tasks { get; set; }
}