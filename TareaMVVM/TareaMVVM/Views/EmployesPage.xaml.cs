using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TareaMVVM.Models;
using TareaMVVM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TareaMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployesPage : ContentPage
    {
        public EmployesPage(Employees employee)
        {
            InitializeComponent();
            BindingContext = new EmployeesViewModel(this, employee);
        }
    }
}