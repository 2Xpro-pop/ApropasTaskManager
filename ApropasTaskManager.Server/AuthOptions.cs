using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApropasTaskManager.Server;

public class AuthOptions
{
    public IEnumerable<string> Issuers { get; set; }
    public IEnumerable<string> Audiences { get; set; }
    public string Key { get; set; }
    public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}
