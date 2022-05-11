using GestionPersonas.BLL;
using GestionPersonas.Entidades;
using System.Windows;

namespace GestionPersonas.UI.Registros
{
    /// <summary>
    /// Interaction logic for rAportes.xaml
    /// </summary>
    public partial class rAportes : Window
    {
        private Aportes aporte = new Aportes();

        public rAportes()
        {
            InitializeComponent();
            this.DataContext = aporte;

            PersonaComboBox.ItemsSource = PersonasBLL.GetPersonas();
            PersonaComboBox.SelectedValuePath = "PersonaId";
            PersonaComboBox.DisplayMemberPath = "Nombres";

            TipoAporteComboBox.ItemsSource = TiposAportesBLL.GetTiposAportes();
            TipoAporteComboBox.SelectedValuePath = "TipoAporteId";
            TipoAporteComboBox.DisplayMemberPath = "Descripcion";
            
            Limpiar();
            ValorTextBox.Text = "0.00";
            MontoTextBox.Text = "0.00";
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = aporte;
        }

        private void Limpiar()
        {
            this.aporte = new Aportes();
            this.DataContext = aporte;
        }

        private bool ExisteBD()
        {
            Aportes esValido = AportesBLL.Buscar(aporte.AporteId);

            return (esValido != null);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Aportes encontrado = AportesBLL.Buscar(aporte.AporteId);

            if (encontrado != null)
            {
                aporte = encontrado;
                Actualizar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Aporte no existe en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
      
            var Detalle = new AportesDetalle
            {
                TiposAporte = (TiposAportes)TipoAporteComboBox.SelectedItem,
                Valor = float.Parse(ValorTextBox.Text)
            };

            aporte.AporteDetalle.Add(Detalle);
            aporte.Monto += float.Parse(ValorTextBox.Text);

            Actualizar();

            ValorTextBox.Focus();
            ValorTextBox.Clear();
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                aporte.AporteDetalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                aporte.Monto -= float.Parse(MontoTextBox.Text);
                Actualizar();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (aporte.AporteId == 0)
            {
                paso = AportesBLL.Guardar(aporte);
            }
            else
            {
                if (ExisteBD())
                {
                    paso = AportesBLL.Guardar(aporte);
                }
                else
                {
                    MessageBox.Show("No existe en la base de datos", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Aportes existe = AportesBLL.Buscar(aporte.AporteId);

            if (existe == null)
            {
                MessageBox.Show("No existe el grupo en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                AportesBLL.Eliminar(aporte.AporteId);
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
    }
}
