using System;
using System.Collections.Generic;
namespace Models;
public class Users
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<Tasks>? Tasks { get; set; }
}