using System;
using System.Collections.Generic;

namespace Service.Library
{
    public class Messages
    {
        public string PersonEmail { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
        public List<string> Files { get; set; }
    }
}
