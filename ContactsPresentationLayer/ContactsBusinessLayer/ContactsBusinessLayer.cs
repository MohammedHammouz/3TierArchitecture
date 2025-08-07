using ContactsDataAccessLayer;
using System;
using System.Data;
using static ContactsBusinessLayer.clsContact;

namespace ContactsBusinessLayer
{

    public class clsContact
    {
            public enum enMode { AddNew=0,Update=1};
            public enMode Mode = enMode.AddNew;
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string ImagePath {  get; set; }
            public int CountryID { get; set; }
            public clsContact()
            {
                this.Id = -1;
                this.FirstName = "";
                this.LastName = "";
                this.Email = "";
                this.Phone = "";
                this.Address = "";
                this.DateOfBirth = DateTime.Now;
                this.ImagePath = "";
                this.CountryID = -1;
                Mode= enMode.AddNew;
            }
        //this constructor we make it private because we can't add id becase database add it automatically
            private  clsContact(int ID,string FirstName, string LastName, 
                string Email,string Phone,string Address,
                DateTime DateOfBirth,string ImagePath,int CountryID)
            {
                this.Id = ID;
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.Email = Email;
                this.Phone = Phone;
                this.Address = Address;
                this.DateOfBirth = DateOfBirth;
                this.ImagePath = ImagePath;
                this.CountryID = CountryID;
                Mode = enMode.Update;

            } 
            public static clsContact Find(int ID)
            {
                string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
                DateTime DateOfBirth = DateTime.Now;
                int CountryID = -1;
                if(clsContactDataAccess.FindByContactID(ID,ref FirstName,ref LastName,ref Email,ref Phone,ref Address,ref ImagePath,ref DateOfBirth,ref CountryID))
                {
                    return new clsContact(ID,  FirstName, LastName,
                     Email,  Phone, Address,
                     DateOfBirth,  ImagePath,  CountryID);
                }
                else
                {
                    return null;
                }
            }
            private bool _AddNewContact()
            {
                this.Id=clsContactDataAccess.AddNewContact(this.FirstName,this.LastName,this.Email,
                    this.Phone,this.Address,this.ImagePath,this.DateOfBirth,this.CountryID);
                return this.Id != -1;
            }
        private bool _UpdateContact()
        {
            return clsContactDataAccess.UpdateContact(this.Id, this.FirstName, this.LastName, this.Email,
                    this.Phone, this.Address, this.ImagePath, this.DateOfBirth, this.CountryID);
        }
        public static bool DeleteContact(int ID)
        {
            return clsContactDataAccess.DeleteContact(ID);
        }
        public static DataTable ListContacts()
        {
            //List<clsContact> Contacts=new List<clsContact>();
            //List<stContact> rawContacts = clsContactDataAccess.ListContacts();
            //foreach (stContact rawContact in rawContacts)
            //{
            //    clsContact contact = new clsContact();
            //    contact.Id = rawContact.ContactID;
            //    contact.FirstName = rawContact.FirstName;
            //    contact.LastName = rawContact.LastName;
            //    contact.Email = rawContact.Email;
            //    contact.Phone = rawContact.Phone;
            //    contact.Address = rawContact.Address;
            //    contact.DateOfBirth = rawContact.DateOfBirth;
            //    contact.CountryID = rawContact.CountryID;
            //    contact.ImagePath = rawContact.ImagePath;

            //    Contacts.Add(contact);
            //}
            //return Contacts;
            return clsContactDataAccess.ListContacts();
        }
        public static bool IsContactExist(int ID)
        {
            return clsContactDataAccess.IsContactExist(ID);
        }
        public static void testTiers()
        {
            clsContactDataAccess.testTiers();
        }
        public bool Save()
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewContact())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case enMode.Update:
                        return _UpdateContact();
                default:
                    return true;
                }
            
        }
    }
    public class clsCountriesBusiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ID { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }
        public clsCountriesBusiness(int ID, string countryName, string code, string phoneCode)
        {
            this.ID = ID;
            this.CountryName = countryName;
            this.Code = code;
            this.PhoneCode = phoneCode;
        }

        private clsCountriesBusiness()
        {
            this.ID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;
        }
        public static clsCountriesBusiness Find(int ID)
        {

            string CountryName = "";
            string Code = "";
            string PhoneCode = "";


            int CountryID = -1;

            if (clsCountriesData.GetCountryInfoByID(ID, ref CountryName, ref Code, ref PhoneCode))

                return new clsCountriesBusiness(ID, CountryName, Code, PhoneCode);
            else
                return null;

        }

        public static clsCountriesBusiness Find(string CountryName)
        {

            int ID = -1;
            string Code = "";
            string PhoneCode = "";


            if (clsCountriesData.GetCountryInfoByName(CountryName, ref ID, ref Code, ref PhoneCode))

                return new clsCountriesBusiness(ID, CountryName, Code, PhoneCode);
            else
                return null;

        }

        private bool _AddNewCountry()
        {
            //call DataAccess Layer 

            this.ID = clsCountriesData.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);

            return (this.ID != -1);
        }
        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateContact();

            }




            return false;
        }
        private bool _UpdateContact()
        {
            //call DataAccess Layer 

            return clsCountriesData.UpdateCountry(this.ID, this.CountryName, this.Code, this.PhoneCode);

        }
        public static bool IsCountryExist(string CountryName)
        {
            return clsCountriesData.IsCountryExistByName(CountryName);
        }
        public static bool InsertIntoCountries()
        {
            return clsCountriesData.InsertIntoCountries();
        }
        public static DataTable ListCountries()
        {
            return clsCountriesData.ListCountries();
        }
    }
}
    

