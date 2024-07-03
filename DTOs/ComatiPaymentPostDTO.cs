﻿using Comati3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comati3.DTOs
{
    public class ComatiPaymentPostDTO : BaseDTO
    {
        public int Amount { get; set; }
        [ForeignKey("ComatiId")]
        public int ComatiId { get; set; }
        public int MemberShipId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Remarks { get; set; }
    }
}