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
        private readonly string FIREBASE_API_ADDRESS = "api_address";
        private readonly FirebaseClient _firebaseClient;

        private ObservableCollection<Order> _oreders;

        public ObservableCollection<Order> Orders
        {
            get { return _oreders; }
            set { _oreders = value; OnPropertyChanged(); }
        }

        public Order SelectedOrder;

        public ICommand AcceptOrderCommand { get; set; }

        public MainPageViewModel()
        {
            _firebaseClient = new FirebaseClient(FIREBASE_API_ADDRESS);
            Orders = new ObservableCollection<Order>();
            AcceptOrderCommand = new Command(() => AcceptOrder());
            ListenerOrders();
        }

        private void AcceptOrder()
        {
            SelectedOrder.IdSeller = 1;
            _firebaseClient
                .Child("orders")
                .Child(SelectedOrder.KeyOrder)
                .PutAsync(SelectedOrder);
        }

        private void ListenerOrders()
        {
            _firebaseClient
                .Child("orders")
                .AsObservable<Order>()
                .Subscribe(order =>
                {
                    if (order.Object != null && order.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        if (order.Object.IdSeller == 0)
                            AddOrder(order.Key, order.Object);
                        else
                            RemoveOrder(order.Key);
                    }
                    else if (order.EventType == FirebaseEventType.Delete)
                    {
                        RemoveOrder(order.Key);
                    }
                });
        }

        private void AddOrder(string key, Order pedido)
        {
            Orders.Add(new Order()
            {
                KeyOrder = key,
                Client = pedido.Client,
                ProductName = pedido.ProductName,
                Price = pedido.Price
            });
        }

        private void RemoveOrder(string orderkey)
        {
            var order = Orders.FirstOrDefault(x => x.KeyOrder == orderkey);
            Orders.Remove(order);
        }
    }
}
