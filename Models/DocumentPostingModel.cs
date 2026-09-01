using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class DocumentPostingModel
    {
        [Range(0, 999999999.99, ErrorMessage = "Gross amount must be between 0 and 999,999,999.99")]
        public decimal GrossAmount { get; set; }

        // Other properties and methods...
    }
}