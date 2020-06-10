using System;
using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Subscription
    {

        public int id { get; set; }

        public string email { get; set; }//Email/username of who purchased subscription

        public string planId { get; set; }
        public string name { get; set; }//name of book
        public double price { get; set; }
        public string text { get; set; }
        
        [DataType(DataType.Date)]
       
        public DateTime Created_at { get; set; }
        [DataType(DataType.Date)]
       
        public DateTime End_at { get; set; }

    }
}
