﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoritySignClient.Models
{
    public class AddUpdatePersonModel : Base.ModelBase
    {
        private bool _isCreate;

        public AddUpdatePersonModel(View.AuthoritySignDocumentsView item = null)
        {
            if (item == null)
            {
                Item = new View.AuthoritySignDocumentsView
                {
                    Customer = null,
                    AuthoritySignDocuments = new DataBase.DataBaseObjects.RefAuthoritySignDocuments()
                    {
                        DataBaseUserName = Utils.ConfigSet.Config.GetInstance().DataBaseUser
                    }
                };

                _isCreate = true;
            }
            else
                Item = item;
        }

        public string TitleText
        {
            get {
                if (_isCreate)
                    return "Добавить данные подписанта";
                else
                    return "Сохранить данные подписанта";
            }
        }

        public string SaveButtonText
        {
            get {
                if (_isCreate)
                    return "Добавить";
                else
                    return "Сохранить";
            }
        }

        public bool IsCreate => _isCreate;
        public View.AuthoritySignDocumentsView Item { get; set; }

        public string Surname
        {
            get {
                return Item?.AuthoritySignDocuments?.Surname;
            }

            set {
                Item.AuthoritySignDocuments.Surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public string Name
        {
            get {
                return Item?.AuthoritySignDocuments?.Name;
            }

            set {
                Item.AuthoritySignDocuments.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string PatronymicSurname
        {
            get {
                return Item?.AuthoritySignDocuments?.PatronymicSurname;
            }

            set {
                Item.AuthoritySignDocuments.PatronymicSurname = value;
                OnPropertyChanged("PatronymicSurname");
            }
        }

        public string Position
        {
            get {
                return Item?.AuthoritySignDocuments?.Position;
            }

            set {
                Item.AuthoritySignDocuments.Position = value;
                OnPropertyChanged("Position");
            }
        }

        public string Inn
        {
            get {
                return Item?.AuthoritySignDocuments?.Inn;
            }

            set {
                Item.AuthoritySignDocuments.Inn = value;
                OnPropertyChanged("Inn");
            }
        }

        public string Comment
        {
            get {
                return Item?.AuthoritySignDocuments?.Comment;
            }
            set {
                Item.AuthoritySignDocuments.Comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public string EmchdId
        {
            get {
                return Item?.AuthoritySignDocuments?.EmchdId;
            }

            set {
                Item.AuthoritySignDocuments.EmchdId = value;
                OnPropertyChanged("EmchdId");
            }
        }

        public DateTime? EmchdBeginDate
        {
            get {
                return Item?.AuthoritySignDocuments?.EmchdBeginDate;
            }

            set {
                Item.AuthoritySignDocuments.EmchdBeginDate = value;
                OnPropertyChanged("EmchdBeginDate");
            }
        }

        public DateTime? EmchdEndDate
        {
            get {
                return Item?.AuthoritySignDocuments?.EmchdEndDate;
            }

            set {
                Item.AuthoritySignDocuments.EmchdEndDate = value;
                OnPropertyChanged("EmchdEndDate");
            }
        }

        public DataBase.DataBaseObjects.RefCustomer SelectedCustomer
        {
            get {
                return Item?.Customer;
            }

            set {
                Item.Customer = value;

                if(value != null)
                    Item.AuthoritySignDocuments.IdCustomer = value.Id;

                OnPropertyChanged("Customer");
            }
        }

        public bool IsMainDefault
        {
            get {
                if (Item?.AuthoritySignDocuments != null)
                    return Item.AuthoritySignDocuments.IsMainDefault != 0;

                return false;
            }
            set {
                if (value)
                    Item.AuthoritySignDocuments.IsMainDefault = 1;
                else
                    Item.AuthoritySignDocuments.IsMainDefault = 0;

                OnPropertyChanged("IsMainDefault");
            }
        }

        public IEnumerable<DataBase.DataBaseObjects.RefCustomer> Customers { get; set; }
        public IEnumerable<View.AuthoritySignDocumentsView> AuthorizedPersons { get; set; }
    }
}
