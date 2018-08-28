using System;
using System.Collections.Generic;

namespace SupportWidgetXF.Models
{
    public class GalleryDirectory
    {
        public string Name { set; get; }

        public List<string> Images { set; get; }

        public GalleryDirectory()
        {
            Images = new List<string>();
        }
    }
}