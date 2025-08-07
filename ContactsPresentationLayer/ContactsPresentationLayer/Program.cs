using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;

namespace ContactsPresentationLayer
{
    internal class Program
    {
        static void testFindContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);
            if (Contact1 != null) {
                Console.WriteLine(Contact1.FirstName+" "+Contact1.LastName);
                Console.WriteLine(Contact1.Email);
                Console.WriteLine(Contact1.Phone);
                Console.WriteLine(Contact1.Address);
                Console.WriteLine(Contact1.DateOfBirth);
                Console.WriteLine(Contact1.CountryID);
                Console.WriteLine(Contact1.ImagePath);
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] is not found");
            }
        }
        static void testAddNewContact()
        {
            clsContact Contact1=new clsContact();
            Contact1.FirstName = "M";
            Contact1.LastName = "H";
            Contact1.Email = "a@a.com";
            Contact1.Phone = "01010101";
            Contact1.Address = "na";
            
            Contact1.ImagePath = "";
            Contact1.DateOfBirth = new DateTime(1999, 11, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            if (Contact1.Save())
            {
                Console.WriteLine("Contact Added successfully with id = "+Contact1.Id);
            }
        }
        static void testUpdateData(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);
            Contact1.FirstName = "M";
            Contact1.LastName = "H";
            Contact1.Email = "a@a.com";
            Contact1.Phone = "01010101";
            Contact1.Address = "na";

            Contact1.ImagePath = "";
            Contact1.DateOfBirth = new DateTime(1999, 11, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            if (Contact1.Save())
            {
                Console.WriteLine("Contact Updated successfully" );
            }
        }
        static void testDeleteData(int ID) {
            
            if (clsContact.IsContactExist(ID))
            {
                if (clsContact.DeleteContact(ID))
                {
                    Console.WriteLine("Contact Deleted successfully");
                }
                else
                {
                    Console.WriteLine("Contact Deleted failed");
                }
            }
            else
            {
                Console.WriteLine("Contact ID is not Found");
            }
            
        }
        static void ListAllContacts(clsContact Contact)
        {
            //List<clsContact> Contacts = Contact.ListContacts();
            //foreach (clsContact contact in Contacts) {
            //    Console.Write(contact.FirstName);
            //    Console.WriteLine($" {contact.LastName}");
            //}
            DataTable dt = clsContact.ListContacts();
            foreach(DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["FirstName"]} {row["LastName"]}");
            }
        }
        static void testComntactExist(int ID)
        {
            if (clsContact.IsContactExist(ID))
            {
                
                Console.WriteLine("Contact ID: "+ID+" is found");
            }
        }
        public static void testTiers() {
            Console.WriteLine(" BusinessLayer is calling...");
            clsContact.testTiers();
        }

        static void Main(string[] args)
        {
            //testFindContact(1);
            //testAddNewContact();
            //testUpdateData(1);
            //testDeleteData(44);
            //clsContact Contact=new clsContact();
            //ListAllContacts(Contact);
            //testComntactExist(1);

            testTiers();
            Console.ReadKey();
        }
    }
}
