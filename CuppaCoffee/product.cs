//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CuppaCoffee
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.drinks = new HashSet<drink>();
            this.order_line = new HashSet<order_line>();
        }
    
        public int product_ID { get; set; }
        public string product_name { get; set; }
        public Nullable<int> product_type_code { get; set; }
        public int product_unit_cost { get; set; }
        public string product_desc { get; set; }
        public Nullable<int> product_quantity { get; set; }
    
        public virtual product_types product_types { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<drink> drinks { get; set; }
        public virtual food food { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_line> order_line { get; set; }
    }
}