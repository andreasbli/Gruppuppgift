//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCGruppuppgift.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public program()
        {
            this.news = new HashSet<news>();
        }
    
        public int programid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.TimeSpan> time_from { get; set; }
        public Nullable<System.TimeSpan> time_to { get; set; }
        public string genre { get; set; }
        public Nullable<int> FK_channelid { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual channel channel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<news> news { get; set; }
    }
}
