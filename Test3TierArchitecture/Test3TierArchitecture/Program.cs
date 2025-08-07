
using System;
using System.Data;
using ContactsBusinessLayer;
namespace Test3TierArchitecture
{
    internal class Program
    {
        static void testFindID(int ID)
        {
            clsBusinessContact Contact1 = clsBusinessContact.Find(ID);
            if (Contact1 != null)
            {
                Console.WriteLine(Contact1.FirstName + " " + Contact1.LastName);
                Console.WriteLine(Contact1.Email);
                Console.WriteLine(Contact1.Phone);
                Console.WriteLine(Contact1.Address);
                Console.WriteLine(Contact1.DateOfBirth);
                Console.WriteLine(Contact1.ImagePath);
                Console.WriteLine(Contact1.CountryID);
            }
            else
            {
                Console.WriteLine("Contact ["+ID+"] is not found");
            }
        }
        static void testAddNewContact()
        {
            clsBusinessContact Contact1 = new clsBusinessContact();
            Contact1.FirstName = "H";
            Contact1.LastName = "H";
            Contact1.Email= "H@g.com";
            Contact1.Phone = "333333";
            Contact1.Address = "N";
            Contact1.ImagePath = "";
            Contact1.DateOfBirth = new DateTime(1999, 11, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            if (Contact1.Save())
            {
                Console.WriteLine("Contact1 saved succesfully");
            }
            
        }
        static void testUpdateContact(int ID)
        {
            clsBusinessContact Contact1 = clsBusinessContact.Find(ID);
            Contact1.FirstName = "Ahmed";
            Contact1.LastName = "Mohammed";
            Contact1.Email = "Ahmed@gmail.com";
            Contact1.Phone = "400171717";
            Contact1.Address = "Cairo";
            Contact1.ImagePath = "";
            Contact1.DateOfBirth = new DateTime(1996, 1, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            if (Contact1.Save())
            {
                Console.WriteLine("Contact1 updated succesfully");
            }
        }
        static void testDeleteData(int ID)
        {
            if (clsBusinessContact.DeleteData(ID))
            {
                Console.WriteLine("Contact ID deleted successfully");
            }
        }
        static void GetListContacts() {
            DataTable dt=clsBusinessContact.GetListContacts();
            foreach (DataRow row in dt.Rows) {
                Console.WriteLine($"{row["FirstName"]} {row["LastName"]}");
            }
        }
        static void Main(string[] args)
        {
            //testFindID(4);
            //testAddNewContact();
            //testUpdateContact(1);
            //testDeleteData(46);
            
            GetListContacts();
            Console.ReadKey();
        }
    }
}
