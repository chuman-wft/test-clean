using CleanArchitecture.Domain.Entities;


    public class Company : BaseEntity
    {

        public string? Name { get; set; }


        public string? Symbol { get; set; }


        public string? Email { get; set; }

        public string Website { get; set; } = "";


        public string? InstrumentType { get; set; }

    }

