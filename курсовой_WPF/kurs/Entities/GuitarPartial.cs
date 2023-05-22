using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.Entities
{
    public partial class guitar
    {
        public string DiscountText
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "";
                else
                    return $"* скидка {Discount}%";
            }
        }
        public string TotalCost
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return $"{CostWithDiscount:N2} рублей";
                else
                    return $" {CostWithDiscount:N2} рублей" ;
            }
        }
        public double CostWithDiscount
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return (double)Cost;
                }
                else
                {
                    var costWithDiscount = (double)Cost - (Discount * 0.01 * (double)Cost);
                    return costWithDiscount.Value;
                }
            }
        }
        public Visibility DiscountVisibility
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public string BackColor
        {
            get
            {
                    return "#332F2C";
            }
        }
        public string DiscountColor
        {
            get
            {
                if (Discount != 0 || Discount == null)
                    return "#FFD700";
                else
                    return "#FFFFFF";
            }
        }
        public string GuitarName
        {
            get
            {
                string guitarName = Name;
                return guitarName;
            }
        }
        public string GuitarWarehouse
        {
            get
            {
                string totalAmount = "";
               
                var total = App.Context.guitar_warehouse.Where(c => c.id_guitar == id_guitar).ToList();
                foreach (var item in total)
                {
                    totalAmount += item.amount;
                }
                return $" На складе осталось {totalAmount} таких гитар";
            }
        }
        public string AdminControlsVisibility
        {
            get
            {
                if (App.CurrentUser.id_role == 1)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        public string UserControlsVisibility
        {
            get
            {
                if (App.CurrentUser.id_role == 3)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
    }   
}
