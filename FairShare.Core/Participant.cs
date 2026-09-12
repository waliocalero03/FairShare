using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Core
{
    public class Participant
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public required string Name { get; set; }
    }
}
