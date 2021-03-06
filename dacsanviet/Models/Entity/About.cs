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
    
    public partial class About
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public About()
        {
            this.FeedBacks = new HashSet<FeedBack>();
        }
    
        public long about_ID { get; set; }
        public string title { get; set; }
        public string metatitle { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string detail { get; set; }
        public string createdDate { get; set; }
        public string createdBy { get; set; }
        public string modifiedDate { get; set; }
        public string modifiedBy { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescriptions { get; set; }
        public Nullable<bool> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedBack> FeedBacks { get; set; }
    }
}
