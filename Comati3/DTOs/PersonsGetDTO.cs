﻿namespace Comati3.DTOs
{
    public class PersonsGetDTO: BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public string? Remarks { get; set; }
        public string? Password { get; set; }
    }
}
