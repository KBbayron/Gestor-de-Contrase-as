using Contraseñas;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Contraseñas{
    public partial class NewUser : Form
    {
        private string connectionString = "Data Source=BAYRON\\SQLLEARNING;Initial Catalog=SecureKenGenData;Integrated Security=True;TrustServerCertificate=True";

        public NewUser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GuardarDatos();
            
        }

        private void GuardarDatos()
        {
            // Obtén el texto del TextBox
            string texto = textBox1.Text;
            int texto2 = Convert.ToInt32(textBox2.Text);
            
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
                        string query = "INSERT INTO Enter (per, word) VALUES (@Text1, @Texto2)";

                        // Crea el comando SQL con parámetros
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Añade parámetros para prevenir la inyección de SQL
                            command.Parameters.AddWithValue("@Text1", texto);
                            command.Parameters.AddWithValue("@Texto2", texto2);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            login f = new login();
            f.Show();
            this.Hide();
        }
    }
}
