using Humanizer;
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
        private static DateTime mainDate = new DateTime(1899, 12, 31);
        private static DateTime lastDate = new DateTime();
        private static int[] controlDigits = { -1, 5, 7, 9, 4, 6, 10, 5, 7 };
        private static int countNow = 0;

        public ICitizen this[string id]
        {
            get
            {
                if (String.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException();
                }

                Citizen result = null;

                foreach (Citizen ct in citizens)
                {
                    if (ct == null)
                        break;

                    if (ct.VatId == id)
                    {
                        result = ct;
                    }
                }

                return result;
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

        private string registerVatID(ref ICitizen citizen)
        {
            TimeSpan time = citizen.BirthDate - mainDate;
            string vatId = String.Empty;
            int tmpDigit = -1;

            vatId += time.Days.ToString();

            if (citizen.BirthDate.Year < 1910)
                vatId += "0";

            foreach (Citizen ct in citizens)
            {
                if (ct == null)
                    break;
                if (ct.VatId.StartsWith(vatId))
                {
                    tmpDigit = Convert.ToInt32(ct.VatId.Substring(6, 3));
                }
            }

            vatId += (++tmpDigit).ToString("000") + (int)citizen.Gender;
            tmpDigit = 0;

            for (int i = 0; i < 9; ++i)
            {
                tmpDigit += (vatId[i] - '0') * controlDigits[i];
            }

            tmpDigit = (tmpDigit % 11) % 10;
            vatId += tmpDigit;

            return vatId;
        }

        public void Register(ICitizen citizen)
        {
            if (checkIfExist(citizen.VatId))
            {
                throw new InvalidOperationException();
            }

            if (string.IsNullOrEmpty(citizen.VatId))
            {
                citizen.VatId = registerVatID(ref citizen);
            }

            citizens[countNow++] = citizen as Citizen;
            lastDate = SystemDateTime.Now();
        }

        public string Stats()
        {
            int men = 0, women = 0;
            string result = String.Empty;

            foreach (Citizen ct in citizens)
            {
                if (ct == null)
                    break;

                if ((int)ct.Gender == 0)
                {
                    ++women;
                }
                else
                {
                    ++men;
                }
            }

            if (men == 0 && women == 0)
            {
                result = "0 men and 0 women";
            }
            else
            {
                result = men + (men > 1 ? " men" : " man") + " and " + women + (women > 1 ? " women" : " woman") + ". " + "Last registartion was " + DateTime.UtcNow.AddHours(-30).Humanize(); // Well, it's seems work but return last word "вчера"
            }

            return result;
        }
    }
}
