using GestionPersonas.BLL;
using GestionPersonas.Entidades;
using System.Windows;

namespace GestionPersonas.UI.Registros
{
    /// <summary>
    /// Interaction logic for rGrupos.xaml
    /// </summary>
    public partial class rGrupos : Window
    {
        private Grupos grupo = new Grupos();

        public rGrupos()
        {
            InitializeComponent();
            this.DataContext = grupo;

            PersonaComboBox.ItemsSource = PersonasBLL.GetPersonas();
            PersonaComboBox.SelectedValuePath = "PersonaId";
            PersonaComboBox.DisplayMemberPath = "Nombres";
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = grupo;
        }

        private void Limpiar()
        {
            this.grupo = new Grupos();
            this.DataContext = grupo;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Grupos encontrado = GruposBLL.Buscar(grupo.GrupoId);

            if (encontrado == null)
            {
                Limpiar();
                MessageBox.Show("Grupo no existe en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                grupo = encontrado;
                Actualizar();
            }
        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            grupo.GrupoDetalle.Add(new GruposDetalle
            {
                GrupoId = grupo.GrupoId,
                Persona = (Personas)PersonaComboBox.SelectedItem,
                Orden = OrdenTextBox.Text
            });

            Actualizar();

            OrdenTextBox.Focus();
            OrdenTextBox.Clear();
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                Limpiar();
            }
            else
            {
                grupo.GrupoDetalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Actualizar();
            }
        }

        private bool ExisteBD()
        {
            Grupos esValido = GruposBLL.Buscar(grupo.GrupoId);

            return (esValido != null);
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (grupo.GrupoId == 0)
            {
                paso = GruposBLL.Guardar(grupo);
            }
            else
            {
                if (!ExisteBD())
                {
                    paso = GruposBLL.Guardar(grupo);
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
            Grupos existe = GruposBLL.Buscar(grupo.GrupoId);

            if (existe != null)
            {
                GruposBLL.Eliminar(grupo.GrupoId);
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("No existe el grupo en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}

