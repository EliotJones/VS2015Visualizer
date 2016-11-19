namespace Domain.Core
{
    using System;
    
    public class Person
    {
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string FavoriteColor { get; private set; }

        public DateTime BirthDate { get; set; }

        public Person(string firstName, string lastName, DateTime birthDate) : this(firstName, lastName, birthDate, "Unassigned")
        {
        }

        public Person(string firstName, string lastName, DateTime birthDate, string favoriteColor)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            FavoriteColor = favoriteColor;
        }

        private Person()
        {
        }
    }
}
