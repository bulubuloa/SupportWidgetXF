using System;
using System.Collections.Generic;

namespace SupportWidgetXF.Models
{
    public class ImageSet
    {
        public string Path { set; get; }
        public bool Checked { set; get; }

        public ImageSet()
        {
            Checked = false;
        }
    }

    public class GalleryDirectory
    {
        public string Name { set; get; }

        public List<ImageSet> Images { set; get; }

        public GalleryDirectory()
        {
            Images = new List<ImageSet>();
        }
    }
}