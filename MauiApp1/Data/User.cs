using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Data;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsLoggedIn { get; set; } = false;

    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
    public bool HasInitialPassword { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid CreatedBy { get; set; }
}
