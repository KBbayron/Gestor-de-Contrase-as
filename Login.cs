using Contraseñas;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Contraseñas
{
    
    public partial class login : Form 
    {
        private string connectionString = "Data Source=BAYRON\\SQLLEARNING;Initial Catalog=SecureKenGenData;Integrated Security=True;TrustServerCertificate=True";
        public bool sesionLog= false;
         public login()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewUser f = new NewUser();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text;

            // Validar que el pin ingresado sea un número entero antes de intentar convertirlo
            if (int.TryParse(textBox2.Text, out int pin))
            {
                // Utilizar una conexión SQL para buscar el usuario
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta SQL para verificar si existe el usuario con el pin proporcionado
                    string query = "SELECT COUNT(*) FROM Enter WHERE per = @usuario AND word = @pint";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parámetros para evitar SQL injection
                        command.Parameters.AddWithValue("@usuario", usuario);
                        command.Parameters.AddWithValue("@pinT", pin);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            bool sesionLog = true;
                            Inicio f = new Inicio();
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuario no encontrado. Verifica el nombre de usuario y el pin.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("El pin debe ser un número de 4 digitos.");
            }
        }

        public bool ObtenerSesionEnLogin()
        {
            return sesionLog;
        }

        private void login_Load(Inicio sesion)
        {
            
        }
    }
}
