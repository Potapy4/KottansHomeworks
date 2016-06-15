using System;
using Humanizer;

namespace Citizens
{
    public class Citizen : ICitizen
    {
        private DateTime dateOfBirth;
        private string firstName;
        private Gender gender;
        private string lastName;

        public Citizen(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            if (!isValidGender(gender))
            {
                throw new ArgumentOutOfRangeException("Invalid gender");
            }

            if (!isValidDateOfBirth(dateOfBirth))
            {
                throw new ArgumentException("Invalid Date Of Birth");
            }

            this.firstName = firstName.Transform(To.LowerCase, To.TitleCase);
            this.lastName = lastName.Transform(To.LowerCase, To.TitleCase);
            this.dateOfBirth = dateOfBirth.Date;
            this.gender = gender;
        }

        public Citizen()
        {
            dateOfBirth = default(DateTime);
            firstName = default(string);
            lastName = default(string);
            gender = default(Gender);
        }

        private bool isValidGender(Gender gender)
        {
            return Enum.IsDefined(typeof(Gender), gender);
        }
        private bool isValidDateOfBirth(DateTime dateOfBirth)
        {
            DateTime dtNow = SystemDateTime.Now(); // Not compare without this
            return (dateOfBirth.CompareTo(dtNow) > 0);
        }        

        #region Properties
        public DateTime BirthDate
        {
            get
            {
                return dateOfBirth;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
        }

        public Gender Gender
        {
            get
            {
                return gender;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
        }

        public string VatId
        {
            get; set;
        }
        #endregion
    }
}
