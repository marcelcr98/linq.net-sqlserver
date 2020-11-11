using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_App
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            // IntroToLinq();
            //DataSource();
            // Filtering();
            //Ordering();
            //Grouping();
            //Grouping2();
            //Joining();
            //introLambda();
            //datasourceLambda();
            //FilteringLambda();
            //OrderingLambda();
            //GroupingLambda();
            //Grouping2Lmabda();
            //JoiningLambda();
            Clientes2Pedidos();
            Pedidos5Years();
            PedidosPrecio();
            Proveedores2Productos();
            Console.Read();

        }

        static void IntroToLinq()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var pares = from c in numbers
                        where c % 2 == 0
                        select c;
            foreach (var item in pares)
            {
                Console.WriteLine("{0,1}", item);
            }

        

        }

        static void DataSource()
        {

            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Filtering()
        {

            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

        }

        static void Ordering()
        {


            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "London"
                                        orderby cust.NombreCompañia ascending
                                        select cust;
            foreach(var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }


        }

        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach (var a in queryCustomersByCity)
            {
                Console.WriteLine(a.Key);
                foreach (clientes c in a)
                {
                    Console.WriteLine(" {0}", c.NombreCompañia);
                }
            }
        }

        //Grouping2
        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {

            var innerJoinQuery =
                    from cust in context.clientes
                    join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                    select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach(var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
           

        }

        //FUNCIONES LAMBDA

        //introLambda

        static void introLambda()
        {
            int[] numbers = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
            var pares = numbers.Where(n => n % 2 == 0).ToList();
            foreach (int num in pares)
            {
                Console.Write(num + "\n");
            }
        }

        //datasourceLambda

        static void datasourceLambda()
        {
            var Allcustomers = context.clientes.ToList();
            foreach (var i in Allcustomers)
            {
                Console.WriteLine(i.NombreCompañia);
            }
        }



        //FilteringLambda

        static void FilteringLambda()
        {
            var clientes = context.clientes.Where(c => c.Ciudad == "Londres").ToList();
            foreach (var iten in clientes)
            {
                Console.WriteLine(iten.Ciudad);
            }
        }





        //OrderingLambda
        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes.Where(c => c.Ciudad == "Londres").OrderByDescending(j => j.NombreCompañia).ToList();

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }



        //GroupingLambda

        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var a in queryCustomersByCity)
            {
                Console.WriteLine(a.Key);
                foreach (clientes c in a)
                {
                    Console.WriteLine(" {0}", c.NombreCompañia);
                }
            }
        }

        //GroupingLambda2
        static void Grouping2Lmabda()
        {
            var customerQuery = context.clientes.GroupBy(c => c.Ciudad).Where(c => c.Key.Count() > 2)
                            .OrderBy(c => c.Key);

            foreach (var cq in customerQuery)
            {
                Console.WriteLine(cq.Key);
            }
        }

        //Joininglambda
        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes
                                .Join(context.Pedidos,
                                 c => c.idCliente,
                                    b => b.IdCliente,
                                (a, b) => new { a.NombreCompañia, b.PaisDestinatario }
                                     );

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine($"{ item.NombreCompañia} y destinatario : {item.PaisDestinatario}");
            }
        }






        //EJERCICIOS EXTRAS CON LAMBDA

        //Todos los pedidos de los ultimos 5 años
        static void Pedidos5Years()
        {

            var queryPedido = context.Pedidos.Where(c => c.FechaPedido > "1994-01-01").OrderByDescending(j => j.idPedido).ToList();

            foreach (var item in queryPedido)
            {
                Console.WriteLine(item.idPedido);
            }


        }
        //Clientes con mas de 2 pedidos

        static void Clientes2Pedidos()
        {
            var custQuery = from cust in context.Pedidos
                            group cust by cust.IdCliente into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        //Todos los pedidos precio*cantidad > 200
        static void PedidosPrecio()
        {
            var queryPedidos = context.detallesdepedidos.Where(c => c.preciounidad * c.cantidad > 200).OrderByDescending(j => j.idpedido).ToList();

            foreach (var item in queryPedidos)
            {
                Console.WriteLine(item.idpedido);
            }
        }

        //Proveedores que tenga mas de 2 productos
        static void Proveedores2Productos()
        {
            var queryPedidos = context.productos.Where(c => c.unidadesEnExistencia > 2).OrderByDescending(j => j.idProveedor).ToList();

            foreach (var item in queryPedidos)
            {
                Console.WriteLine(item.idProveedor);
            }
        }

    }
}
