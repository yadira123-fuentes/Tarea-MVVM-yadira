using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TareaMVVM.Config;
using TareaMVVM.Models;
using Xamarin.Forms;

namespace TareaMVVM.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {

        public Command SaveCommand { get; }


        int idEmployee;
        string nombre;
        string apellido;
        string direccion;
        string edad;
        string cargo;
        string titlePicker;

        ObservableCollection<Cargos> listCargos;
        Cargos cargoSeleccionado;
        public int IdEmployee { get => idEmployee; set { SetProperty(ref idEmployee, value); } }
        public string Nombre { get => nombre; set { SetProperty(ref nombre, value); } }
        public string Apellido { get => apellido; set { SetProperty(ref apellido, value); } }

        public string Edad { get => edad; set { SetProperty(ref edad, value); } }
        public string Direccion { get => direccion; set { SetProperty(ref direccion, value); } }
        public string Cargo { get => cargo; set { SetProperty(ref cargo, value); } }

        public ObservableCollection<Cargos> ListCargos { get => listCargos; set { SetProperty(ref listCargos, value); } }

        public Cargos CargoSeleccionado { get => cargoSeleccionado; set { SetProperty(ref cargoSeleccionado, value); } }

        public string TitlePicker { get => titlePicker; set { SetProperty( ref titlePicker,value); } }

        Page Page;
        Employees Employee;
        public EmployeesViewModel(Page page, Employees employee)
        {
            
             Page = page;

             Employee = employee;
           
            LoadData();
            SaveCommand = new Command(async()=> {
                if (Validate())
                {
                    CargarDatos();
                    UserDialogs.Instance.ShowLoading("Guardando");
                    int respuesta = await DataBase.CurrentDB.SaveEmployee(Employee);
                    if (respuesta == Constants.SUCCESS)
                    {
                        Constants.WasChange = true; //Variable bandera para determinar si se realizo un cambio
                        await Page.DisplayAlert("Información", "Guardado con éxito.", "Aceptar");
                        await Page.Navigation.PopAsync();
                    }
                    else
                        await Page.DisplayAlert("Información", "Error al guardar.", "Aceptar");
                    UserDialogs.Instance.HideLoading();
                }
                else
                    await Page.DisplayAlert("Advertensia", "Debe llenar todos los campos.", "Aceptar");
            });
        }

         void LoadData()
        {

            ListCargos = new ObservableCollection<Cargos>(Cargos.GetCargos());
            if (Employee.IdEmployee>0)
            {
                Title = "Actualizar empleado";
                Nombre = Employee.Nombre;
                Apellido = Employee.Apellido;
                Edad = Employee.Edad;
                Direccion = Employee.Direccion;
                CargoSeleccionado = new Cargos {Cargo= Employee.Cargo };
                TitlePicker = Employee.Cargo;


            }
          else
            {
                Title = "Agregar empleado";
                TitlePicker = "Seleccione un cargo";
            }
        }
       
        bool Validate()
        {
            if (CargoSeleccionado != null && !string.IsNullOrEmpty(Nombre)
                && !string.IsNullOrEmpty(Apellido) && !string.IsNullOrEmpty(Edad)&&
                !string.IsNullOrEmpty(Direccion))
                return true;
            else
                return false;
        }
        void CargarDatos()
        {
            Employee.Nombre = Nombre;
            Employee.Apellido = Apellido;
            Employee.Direccion = Direccion;
            Employee.Edad = Edad;
            Employee.Cargo = CargoSeleccionado.Cargo;
        }
    }
    
}
