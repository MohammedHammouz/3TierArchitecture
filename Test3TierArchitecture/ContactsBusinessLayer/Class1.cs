using System;
using System.Data;
using CotactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsBusinessContact
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int CountryID { get; set; }
        public clsBusinessContact()
        {
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.ImagePath = "";
            this.CountryID = -1;
            Mode = enMode.AddNew;
            
        }
        
        private  clsBusinessContact(int ID,string FirstName,string LastName,
            string Email,string Phone,string Address,
            DateTime  DateOfBirth,int CountryID, string ImagePath)
        {
            
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        } 
        public static clsBusinessContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "",ImagePath="";
            DateTime DateOfBirth = DateTime.Now;

            int CountryID = -1;
            if (clsDataAccess.FindByContactID(ID, ref FirstName, ref LastName, ref Email
                , ref Phone, ref Address, ref ImagePath, ref DateOfBirth, ref CountryID))
            {
                return new clsBusinessContact(ID, FirstName, LastName, Email,
                    Phone, Address, DateOfBirth, CountryID, ImagePath);
            }
            else {
                return null;
            }
                
        }
        private bool _AddContact()
        {
            this.ID = clsDataAccess.AddNewContact(this.FirstName, this.LastName, this.Email
                ,this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
            return this.ID != -1;
        }
        private  bool _UpdateContact()
        {
            return clsDataAccess.UpdateContact(this.ID, this.FirstName, this.LastName,
            this.Email, this.Phone, this.Address,
            this.ImagePath, this.DateOfBirth, this.CountryID);
        }
        public static bool DeleteData(int ID)
        {
            return clsDataAccess.DeleteContact(ID);
        }
        public static DataTable GetListContacts()
        {
            return clsDataAccess.GetListContacts();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else{
                        return false;
                    }
                case enMode.Update:
                    return _UpdateContact();
                default:
                    return true;
            }
            
        }
    }
}
