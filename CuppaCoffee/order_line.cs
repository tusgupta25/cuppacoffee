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
    
    public partial class order_line
    {
        public int order_ID { get; set; }
        public int product_ID { get; set; }
        public int order_quantity { get; set; }
    
        public virtual order1 order { get; set; }
        public virtual product product { get; set; }
    }
}
