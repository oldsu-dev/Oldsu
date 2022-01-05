using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types {
    [Keyless]
    public class Badge {
        public int UserID { get; set; }
        public string Filename { get; set; }
    }
}