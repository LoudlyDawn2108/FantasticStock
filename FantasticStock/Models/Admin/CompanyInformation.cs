using System;

namespace AdminDomain.Models
{
    public class CompanyInformation
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxID { get; set; }
        public byte[] LogoImage { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}