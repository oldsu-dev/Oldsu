using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;

namespace Oldsu.Types
{
    [Keyless]
    public class Channel
    {
        public string Tag { get; set; }
        public string Topic { get; set; }
        
        public bool AutoJoin { get; set; }
        public bool CanWrite { get; set; }

        public Privileges RequiredPrivileges { get; set; }
    }
}