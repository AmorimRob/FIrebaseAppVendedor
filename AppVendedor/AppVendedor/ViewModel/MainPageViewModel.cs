using AppVendedor.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppVendedor.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {

        private readonly FirebaseClient _firebaseClient;

        private ObservableCollection<Pedido> _pedidos;

        public ObservableCollection<Pedido> Pedidos
        {
            get { return _pedidos; }
            set { _pedidos = value; OnPropertyChanged(); }
        }

        public Pedido PedidoSelecionado;

        public ICommand AceitarPedidoCmd { get; set; }

        public MainPageViewModel()
        {

            Pedidos = new ObservableCollection<Pedido>();
            AceitarPedidoCmd = new Command(() => AceitarPedido());
            ListenerPedidos();
        }

        private void AceitarPedido()
        {
            PedidoSelecionado.IdVendedor = 1;
        }

        private void ListenerPedidos()
        {
           
        }

        private void AdicionarPedido(string key, Pedido pedido)
        {
            Pedidos.Add(new Pedido()
            {
                KeyPedido = key,
                Cliente = pedido.Cliente,
                Produto = pedido.Produto,
                Preco = pedido.Preco
            });
        }

        private void RemoverPedido(string pedidoKey)
        {
            var pedido = Pedidos.FirstOrDefault(x => x.KeyPedido == pedidoKey);
            Pedidos.Remove(pedido);
        }
    }
}
