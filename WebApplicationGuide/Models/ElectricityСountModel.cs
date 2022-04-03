using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WebApplicationGuide.Models
{
    public class ElectricityCount
    {
        [Key] public long ElectricityCountId { get; set; }
        public string SerialNumber { get; set; }
        [Required] public string Name { get; set; }
    }

    public class ElectricityValue
    {
        [Key] public long ElectricityValuesId { get; set; }
        [Required] public long ElectricityCountForeignKey { get; set; }

        [DataType(DataType.Date)] [Required] public DateTime CreateAt { get; set; } // Дата записи данных

        //Возможно тут можно было сделать вложенный объект?
        public float ActiveReceive { get; set; }
        public float ActiveOutput { get; set; }
        public float ReactiveReceive { get; set; }
        public float ReactiveOutput { get; set; }

        public ElectricityCount ElectricityCount { get; set; }

        // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        //     validationContext.
        //     if (null == 1)
        //     {
        //         yield return new ValidationResult("ElectricityCountForeignKey is invaliud",
        //             new[] {"ElectricityCountForeignKey"});
        //     }
        // }
    }
}