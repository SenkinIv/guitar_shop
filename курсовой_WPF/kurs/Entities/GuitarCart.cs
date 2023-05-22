using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace kurs.Entities
{
    public partial class users_guitar   
    {
        public string GuitarName
        {
            get
            {
                string guitarName = guitar.Name;
                return guitarName;
            }
        }
        public string DiscountText
        {
            get
            {
                if (guitar.Discount == 0 || guitar.Discount == null)
                    return "";
                else
                    return $"* скидка {guitar.Discount}%";
            }
        }
        public string TotalCost
        {
            get
            {
                if (guitar.Discount == 0 || guitar.Discount == null)
                    return $"{guitar.Cost:N2} рублей";
                else
                    return $" {CostWithDiscount:N2} рублей";
            }
        }
       
        public double CostWithDiscount
        {
            get
            {
                if (guitar.Discount == 0 || guitar.Discount == null)
                {
                    return (double)guitar.Cost;
                }
                else
                {
                    var cosrWithDiscount = (double)guitar.Cost - (guitar.Discount * 0.01 * (double)guitar.Cost);
                    return cosrWithDiscount.Value;
                }
            }
        }
        public Visibility DiscountVisibility
        {
            get
            {
                if (guitar.Discount == 0 || guitar.Discount == null)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public string DiscountColor
        {
            get
            {
                if (guitar.Discount != 0 || guitar.Discount == null)
                    return "#FFD700";
                else
                    return "#FFFFFF";
            }
        }
        public string BackColor
        {
            get
            {
                return "#332F2C";
            }
        }
    }
}
