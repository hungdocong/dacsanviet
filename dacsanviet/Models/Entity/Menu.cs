//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dacsanviet.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public long menu_ID { get; set; }
        public string text { get; set; }
        public string link { get; set; }
        public Nullable<int> displayOrder { get; set; }
        public string target { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<long> menuType_ID { get; set; }
        public Nullable<long> menuParent_ID { get; set; }
    
        public virtual MenuType MenuType { get; set; }
    }
}