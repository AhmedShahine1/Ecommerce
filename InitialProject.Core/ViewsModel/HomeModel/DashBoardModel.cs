using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.ViewsModel.HomeModel
{
    public class DashBoardModel
    {
        public int Cities { get; set; }

        public int AllProviders { get; set; }

        public int AllProvidersWaitApproved { get; set; }

        public int Centers { get; set; }

        public int FreeAgents { get; set; }

        public int AllSections { get; set; }

        public int AllServices { get; set; }

        public int AllOrders { get; set; }

        public int AllFinishOrders { get; set; }

        public int AllComplains { get; set; }

        public double Profits { get; set; }

        public float TotalAmount { get; set; }

    }
}
