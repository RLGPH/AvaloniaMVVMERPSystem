using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class Moderator
    {
        public int ModeratorId { get; set; } = 0;
        public bool IsMod { get; set; } = false;
        public Moderator(int modId, bool isMod)
        {
            ModeratorId = modId;
            IsMod = isMod;
        }
    }
}
