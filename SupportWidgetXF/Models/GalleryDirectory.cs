using System;
using System.Collections.Generic;
using SupportWidgetXF.Models.Widgets;

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

    public class GalleryDirectory : IAutoDropItem
    {
        public string Name { set; get; }

        public List<ImageSet> Images { set; get; }

        public GalleryDirectory()
        {
            Images = new List<ImageSet>();
        }

        public string IF_GetTitle()
        {
            return Name;
        }

        public string IF_GetDescription()
        {
            return Name;
        }

        public string IF_GetIcon()
        {
            return Name;
        }

        public Action IF_GetAction()
        {
            return null;
        }

        public bool IF_GetChecked()
        {
            return false;
        }

        public void IF_SetChecked(bool _Checked)
        {

        }
    }
}