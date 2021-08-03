using System;

namespace StatementConverter.Models
{
    public class Transaction
    {
        public string ValueDate { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public string PostDate { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3},{4},{5}", this.ValueDate, this.Description, this.Reference, this.PostDate, this.Amount, this.Balance);
        }
    }
}
