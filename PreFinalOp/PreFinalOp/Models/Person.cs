using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreFinalOp.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int MF { get; set; }
        public List<SelectListItem> SelectMF { get; set; }
        public decimal NAVPS = 5.6178M;
        public decimal SalesLoad
        {
            get
            {
                if (Amount <= 99999.99M && Amount >= 1000M)
                {
                    return Amount * .02M;
                }
                else if (Amount >= 100000M && Amount <= 499999.99M)
                {
                    return Amount * .015M;
                }
                else if (Amount >= 500000M && Amount <= 1999999.99M)
                {
                    return Amount * .01M;
                }
                else if (Amount >= 2000000M)
                {
                    return Amount * .005M;
                }
                else { return 0; }
            }
        }
        public decimal NetAmount { get { return Amount - SalesLoad; } }
        public decimal SharesBought { get { return NetAmount / NAVPS; } }
    }
}