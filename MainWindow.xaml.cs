using GestionPersonas.UI.Registros;
using System.Windows;

namespace GestionPersonas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PersonaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rPersonas registro = new rPersonas();
            registro.Show();
        }

        private void RolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rRoles rol = new rRoles();
            rol.Show();
        }

        private void GrupoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rGrupos grupo = new rGrupos();
            grupo.Show();
        }

        private void TipoAporteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rTiposAportes tipo = new rTiposAportes();
            tipo.Show();
        }

        private void AporteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rAportes aporte = new rAportes();
            aporte.Show();
        }
    }
}
