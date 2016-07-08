using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Model.Forms
{
    public class CheckBoxItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public CheckBoxItem() { }
        public CheckBoxItem(int id, string name)
        {
            ID = id;
            Name = name;
            IsChecked = false;
        }
        public CheckBoxItem(int id, string name, bool isChecked)
            :this(id, name)
        {
            IsChecked = isChecked;
        }
    }
}
