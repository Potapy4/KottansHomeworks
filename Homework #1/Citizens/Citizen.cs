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
            if (!isValidGender(ref gender))
            {
                throw new ArgumentOutOfRangeException();
            }

            if (!isValidDateOfBirth(ref dateOfBirth))
            {
                throw new ArgumentException();
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

        private bool isValidGender(ref Gender gender)
        {
            if (!Enum.IsDefined(typeof(Gender), gender))
            {
                return false;
            }
            return true;
        }
        private bool isValidDateOfBirth(ref DateTime dateOfBirth)
        {
            DateTime dtNow = SystemDateTime.Now(); // Not compare without this
            if (dateOfBirth.CompareTo(dtNow) > 0)
            {
                return false;
            }
            return true;
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
