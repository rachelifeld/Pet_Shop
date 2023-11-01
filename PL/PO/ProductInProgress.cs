using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ProductInProgress : INotifyPropertyChanged
    {
        private int id;
        private string? name;
        private double price;

        private eCondition prevStatus;

        public int Id { get => id; set => id = value; }
        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public double Price { get => price; set { price = value; NotifyPropertyChanged(nameof(Price)); } }
        public eCategory? Category { get; set; }
        public int? InStock { get; set; }
        public eCondition PrevStatus { get => prevStatus; set { prevStatus = value; NotifyPropertyChanged(nameof(PrevStatus)); } }
        public DateTime LastUpdateAt { get; set; }
        public int WillUpdateAt { get; set; }
        public DateTime StartChangeAt { get; internal set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            //if (PropertyChanged !=null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(info));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public override string ToString() => $@"
        id = {Id},
        name = {Name},
        price = {Price},
        category = {Category},
        {nameof(PrevStatus)} = {PrevStatus},
        {nameof(LastUpdateAt)} = {LastUpdateAt},
        {nameof(WillUpdateAt)} = {WillUpdateAt},
 
        in stock = {InStock}";
    }
}
