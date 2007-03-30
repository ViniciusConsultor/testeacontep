using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Contacts
{
    // Contact.cs
    // The type of the items in the Contacts collection property 
    //in QuickContacts.    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Contact
    {
        private string nameValue;
        private string emailValue;
        private string phoneValue;
        private bool teste;


        public Contact()
            : this(String.Empty, String.Empty, String.Empty, false)
        {
        }

        public Contact(string name, string email, string phone, bool teste)
        {
            nameValue = name;
            emailValue = email;
            phoneValue = phone;
            this.teste = teste;
        }

        [
        Category("Behavior"),
        DefaultValue(""),
        Description("Name of contact"),
        NotifyParentProperty(true),
        ]
        public bool Teste
        {
            get
            {
                return teste;
            }
            set
            {
                teste = value;
            }
        }


        [
        Category("Behavior"),
        DefaultValue(""),
        Description("Name of contact"),
        NotifyParentProperty(true),
        ]
        public String Name
        {
            get
            {
                return nameValue;
            }
            set
            {
                nameValue = value;
            }
        }

        [
        Category("Behavior"),
        DefaultValue(""),
        Description("Email address of contact"),
        NotifyParentProperty(true)
        ]
        public String Email
        {
            get
            {
                return emailValue;
            }
            set
            {
                emailValue = value;
            }
        }

        [
        Category("Behavior"),
        DefaultValue(""),
        Description("Phone number of contact"),
        NotifyParentProperty(true)
        ]
        public String Phone
        {
            get
            {
                return phoneValue;
            }
            set
            {
                phoneValue = value;
            }
        }
    }
}

