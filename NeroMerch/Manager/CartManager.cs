using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeroMerch.DATA;

namespace NeroMerch.Manager
{
    public class CartManager
    {
        private readonly Dictionary<int, int> _cart; //Inhalt des Speicherplatzes

        public Dictionary<int, int> Cart
        {
            get
            {
                return _cart ?? throw new Exception("_cart ist null");
            }
        }

        public CartManager(Dictionary<int, int> sessionCart)
        {
            if (sessionCart == null)
            {
                _cart = new Dictionary<int, int>();
            }
            else
            {
                _cart = sessionCart;
            }
        }

        public void Add(int id, int qty)
        {
            if (qty <= 0)
            {
                return;
            }

            int oldQty = 0;

            if (_cart.TryGetValue(id, out oldQty)) //Wenn der Eintrag schon vorhanden ist...
            {
                _cart[id] = oldQty + qty; //Rechne die neue Menge hinzu.
            }
            else //Ansonsten... 
            {
                _cart.Add(id, qty);
            }
        }

        public void Remove(int id)
        {
            _cart.Remove(id);
        }

        public void Remove(int id, int qty)
        {
            if (qty <= 0)
            {
                return;
            }

            int oldQty = 0;

            if (_cart.TryGetValue(id, out oldQty)) //Wenn der Eintrag schon vorhanden ist...
            {
                if (oldQty > 1) //...und mehr als ein Stück enthalten ist
                {
                    _cart[id] = oldQty - qty; //Ziehe die neue Menge ab.
                }
                else //Ansonsten
                {
                    _cart.Remove(id); //Entferne den Eintrag ganz
                }
            }
            else //Falls der Eintrag gar nicht existiert...
            {
                //könnte man hier eine schöne Fehlermeldung ausgeben
            }
        }

        public void ClearCart()
        {
            _cart.Clear();
        }

        public List<ProductInfo> GetProducts()
        {
            var productIds = _cart.Keys.ToArray();

            using (var db = new IN31_NeroMerch_WebshopEntities())
            {
                return db.ProductInfo
                    .Where(p => productIds.Contains(p.Id))
                    .ToList();
            }
        }

        public decimal GetTotal()
        {
            var dbProducts = GetProducts();

            var total = 0m;
            foreach (var p in dbProducts)
            {
                total += p.NetUnitPrice * _cart[p.Id];
            }
            return total;
        }
    }
}