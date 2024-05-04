using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using System.Data.SqlClient;
using SecureKeyGen;
namespace Contraseñas
{
    public partial class Form1 : Form{
        string connectionString = "Server=localhost;Database=SecureKeyGenData;Uid=root;Pwd=DAT4base/2000;";
        //private string connectionString = "Data Source=BAYRON\\SQLLEARNING;Initial Catalog=SecureKeyGenData;Integrated Security=True;TrustServerCertificate=True";
        private Random random = new Random();
        private System.Windows.Forms.CheckBox[] checkboxes;
        private Label miLabel;
        public Form1()
        {
            InitializeComponent();
            ////
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            button1.Click += new EventHandler(button4_Click);
            ////
            checkboxes = new System.Windows.Forms.CheckBox[] { checkBox3, checkBox2, checkBox1, checkBox4 };
            // Asignar el mismo controlador de eventos a los cuatro checkboxes
            foreach (System.Windows.Forms.CheckBox checkbox in checkboxes)
            {
                checkbox.CheckedChanged += CheckBox3_CheckedChanged;
            }
            checkboxes[0].Checked = true;
        }
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox changedCheckBox = (System.Windows.Forms.CheckBox)sender;

            // Verificar si al menos uno de los checkboxes está marcado
            if (TodosDesmarcados())
            {
                // Si ninguno está marcado, marcar el checkbox que desencadenó el evento
                changedCheckBox.Checked = true;
            }
        }
        private bool TodosDesmarcados()
        {
            foreach (System.Windows.Forms.CheckBox checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    return false; // Al menos uno está marcado
                }
            }
            return true; // Ninguno está marcado
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            string password = GenerarContrasena(trackBar1.Value, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked);
            textBox3.Text = $"{password}";
            label10.Text = trackBar1.Value.ToString();
            if (trackBar1.Value == 4){
                label6.Text = "Funciona solo para un PIN";
                label6.ForeColor = Color.Black;
            }else if (trackBar1.Value <= 7){
                label6.Text = "Poco Segura";
                label6.ForeColor = Color.Red;
                pictureBox1.Image = SecureKeyGen.Properties.Resources.Mala1;
            } else if (trackBar1.Value >= 8 && trackBar1.Value <= 11){
                label6.Text = "Segura";
                label6.ForeColor = Color.Orange;
                pictureBox1.Image = SecureKeyGen.Properties.Resources.Buena__1_;
            } else if (trackBar1.Value >= 12 && trackBar1.Value <= 50){
                label6.Text = "Fuerte";
                label6.ForeColor = Color.Green;
                pictureBox1.Image = SecureKeyGen.Properties.Resources.Fuerte1;
            }
        }
        private string GenerarContrasena(int longitud, bool incluirMayusculas, bool incluirMinusculas, bool incluirNumeros, bool incluirCaracteres)
        {
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";
            const string caracteresEspeciales = "!@#$%^&*()_-+=<>?";
            StringBuilder caracteresPermitidos = new StringBuilder();
            if (incluirMayusculas)
                caracteresPermitidos.Append(mayusculas);
            if (incluirMinusculas)
                caracteresPermitidos.Append(minusculas);
            if (incluirNumeros)
                caracteresPermitidos.Append(numeros);
            if (incluirCaracteres)
                caracteresPermitidos.Append(caracteresEspeciales);
            if (caracteresPermitidos.Length == 0)
            {
                MessageBox.Show("Debes seleccionar al menos una opción para generar la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < longitud; i++)
            {
                int index = random.Next(caracteresPermitidos.Length);
                sb.Append(caracteresPermitidos[index]);
            }
            return sb.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label4.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los labels
            string nombre =         textBox1.Text;
            string usuario =        textBox2.Text;
            string contrasena =     textBox3.Text;
            string url =            textBox4.Text;

            // Verificar si los valores de "name" y "pass" están vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese un valor para el nombre y la contraseña antes de guardar.");
                return;
            }

            // Crear la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            //using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Definir la consulta SQL para insertar los datos en la tabla Forms

                    string query = @"INSERT INTO pssw (name, user, pass, url) 
                     VALUES (@Name, @User, @Pass, @Url)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    //using (SqlConnection connection = new SqlConnection(connectionString))

                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Name", nombre);
                    command.Parameters.AddWithValue("@User", usuario);
                    command.Parameters.AddWithValue("@Pass", contrasena);
                    command.Parameters.AddWithValue("@Url", url);

                    // Ejecutar la consulta
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datos guardados exitosamente en la base de datos.");
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar los datos en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar los datos en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                //using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Intenta abrir la conexión
                    connection.Open();

                    // Si llega hasta aquí, la conexión fue exitosa
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
            Console.ReadLine();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string password = GenerarContrasena(trackBar1.Value, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked);
            textBox3.Text = $"{password}";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string password = GenerarContrasena(trackBar1.Value, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked);
            textBox3.Text = $"{password}";
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            string password = GenerarContrasena(trackBar1.Value, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked);
            textBox3.Text = $"{password}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string password = GenerarContrasena(trackBar1.Value, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked);
            textBox3.Text = $"{password}";
        }
    }
}
