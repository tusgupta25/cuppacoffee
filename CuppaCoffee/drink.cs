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
    
    public partial class drink
    {
        public int product_ID { get; set; }
        public int drink_size_code { get; set; }
        public string ingredients { get; set; }
        public Nullable<int> price { get; set; }
        public string flavor { get; set; }
    
        public virtual drink_sizes drink_sizes { get; set; }
        public virtual product product { get; set; }
    }
}