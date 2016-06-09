using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizens
{
    public class CitizenRegistry : ICitizenRegistry
    {
        private static Citizen[] citizens = new Citizen[20];
        private static int countNow;
        private static DateTime mainDate = new DateTime(1899, 12, 31);

        public ICitizen this[string id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private bool checkIfExist(string vatID)
        {
            bool result = false;

            foreach (Citizen ct in citizens)
            {
                if (ct == null)
                    break;
                if (ct.VatId == vatID)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private bool checkVatId(string vatID)
        {
            int[] controlDigits = { -1, 5, 7, 9, 4, 6, 10, 5, 7 };
            int[] ourDigits = new int[10];
            int X = 0;

            for (int i = 0; i < vatID.Length; ++i)
            {
                ourDigits[i] = vatID[i] - '0';
            }

            for (int i = 0; i < 9; ++i)
            {
                X += ourDigits[i] * controlDigits[i];
            }

            X = (X % 11) % 10;

            return (X == ourDigits[9]);
        }

        private string registerVatID(DateTime dt)
        {           
            TimeSpan time = dt - mainDate;
            
            // TODO : main logic

            return time.Days.ToString();
        }

        public void Register(ICitizen citizen)
        {

            if (checkIfExist(citizen.VatId))
            {
                throw new InvalidOperationException();
            }

            if (string.IsNullOrEmpty(citizen.VatId))
            {
                citizen.VatId = registerVatID(citizen.BirthDate);
            }


            citizens[countNow++] = citizen as Citizen;
        }

        public string Stats()
        {
            throw new NotImplementedException();
        }
    }
}
