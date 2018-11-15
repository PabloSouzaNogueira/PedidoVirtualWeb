using PedidoVirtualWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.App_Start
{
    public class PedidoBase
    {
        public static List<PedidoItem> Carrinho { get; set; }

        //public static int Pedido { get; set; }
        public static string Usuario { get; set; }
        //public static List<int> Items { get; set; }
        //public static List<int> Quantidade { get; set; }

        //public static List<ItemQuantidade> Items { get; set; }
    }

    
}
