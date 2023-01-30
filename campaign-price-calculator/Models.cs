using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CPC.Models
{


    public class Product
    {
        public string EAN { get; set; } = "";
        public int Price { get; set; } = 100;
    }
    public class Cart
    {
        public List<CartItem> cartItems { get; set; } = new List<CartItem>();
        public int CalculatePrice()
        {
            return cartItems.Sum(p => p.Price);
        }
    }
    public class CartItem : Product
    {
    }

    //Base class for new campaign
    public class Campaign
    {
        public string name { get; set; } = "";
        //Base calculation for campaign price
        public virtual int CalculatePrice(Cart cart)
        {
            return cart.cartItems.Sum(p => p.Price);
        }
    }
    public class VolumeCampaign : Campaign
    {
        public Product campaignProduct { get; set; } = new Product();
        public int minimumQuantity { get; set; } = 2;
        public int Price { get; set; } = 0;
        //Price calculation for Volume Campaign
        public override int CalculatePrice(Cart cart)
        {
            var units = cart.cartItems.Where(p => p.EAN == campaignProduct.EAN).ToList();
            var otherunits = cart.cartItems.Where(p => p.EAN != campaignProduct.EAN).ToList();
            var otherprice = otherunits.Sum(o => o.Price);
            var rem = units.Count % minimumQuantity;
            return ((units.Count / minimumQuantity) * Price) + (rem * campaignProduct.Price) + otherprice;
        }
    }
    public class ComboCampaign : Campaign
    {
        public List<Product> campaignItems = new List<Product>();
        public int Price { get; set; } = 0;
        //Price calculation for Combo Campaign
        public override int CalculatePrice(Cart cart)
        {
            var campaignUnits = cart.cartItems.Where(u => campaignItems.Any(i => i.EAN == u.EAN)).ToList();
            var otherunits = cart.cartItems.Where(c => !campaignItems.Any(x => x.EAN == c.EAN)).ToList();
            var otherprice = otherunits.Sum(o => o.Price);
            var rem = campaignUnits.Count % 2;
            var remprice = campaignUnits.Any() ? campaignUnits.Last().Price : 0;
            return ((campaignUnits.Count / 2) * Price) + (rem * remprice) + otherprice;
        }
    }
}
