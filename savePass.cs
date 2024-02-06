using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Contraseñas
{
    public partial class savePass : Form
    {
        private string connectionString = "Data Source=BAYRON\\SQLLEARNING;Initial Catalog=SecureKenGenData;Integrated Security=True;TrustServerCertificate=True";
        private Creating formulario1;
        public savePass(Creating form1)
        {
            InitializeComponent();
            formulario1 = form1;
            label3.Text = form1.ObtenerTextoDeLabelEnForm1();
        }
        private void ManejarClickBoton(object sender, EventArgs e)
        {
            // Aquí se ejecutará cuando se haga clic en el botón
            GuardarDatos();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Creating f = new Creating();
            f.Show();
            this.Hide();
        }
        private void GuardarDatos()
        {
            // Obtén el texto del TextBox
            string texto = label3.Text;
            string texto2 = textBox1.Text;
            if (!string.IsNullOrEmpty(texto))
            {
                // Crea la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Abre la conexión
                        connection.Open();

                        // Define la consulta SQL para la inserción de datos
                        string query = "INSERT INTO Labels (lbl, pass) VALUES (@TextoLabel, @TextoTextBox)";

                        // Crea el comando SQL con parámetros
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Añade parámetros para prevenir la inyección de SQL
                            command.Parameters.AddWithValue("@TextoLabel", texto);
                            command.Parameters.AddWithValue("@TextoTextBox", texto2);
                            // Ejecuta la consulta
                            command.ExecuteNonQuery();

                            MessageBox.Show("Datos guardados correctamente.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar datos: " + ex.Message);
                    }
                    finally
                    {
                        // Cierra la conexión
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un valor en el TextBox.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GuardarDatos();
            Creating f = new Creating();
            f.Show();
            this.Hide();
        }
    }
}
