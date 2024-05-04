using Contraseñas;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Security.Policy;

namespace SecureKeyGen
{
    public partial class Form2 : Form{
        string connectionString = "Server=localhost;Database=SecureKeyGenData;Uid=root;Pwd=DAT4base/2000;";
        //private string connectionString = "Data Source=BAYRON\\SQLLEARNING;Initial Catalog=SecureKeyGenData;Integrated Security=True;TrustServerCertificate=True";
        
        public Form2()
        {
            InitializeComponent();
            CargarDatosComboBox();
            comboBox1.SelectedIndexChanged += Combo;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
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
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
        private void CargarDatosComboBox()
        {
            // Crear la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            //using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Definir la consulta SQL para obtener los datos
                    string query = "SELECT * FROM pssw ";

                    // Crear el adaptador de datos y el conjunto de datos
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    // Llenar el conjunto de datos con los datos de la base de datos
                    adapter.Fill(dataSet, "pssw");


                    // Agregar un espacio en blanco al principio de los datos
                    DataRow row = dataSet.Tables["pssw"].NewRow();
                    row["name"] = string.Empty; // Establecer el campo name como una cadena vacía
                    dataSet.Tables["pssw"].Rows.InsertAt(row, 0);

                    // Asignar los datos al ComboBox
                    comboBox1.DataSource = dataSet.Tables["pssw"];

                    comboBox1.DisplayMember = "name";
                    comboBox1.SelectedIndex = -1;
                    /*DataRowView selectedRow = (DataRowView)comboBox1.SelectedItem;
                    label3.Text = selectedRow["lbl"].ToString();*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos en el ComboBox: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión
                    connection.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener la URL del TextBox
            string url = textBox4.Text;

            // Verificar si la URL no está vacía
            if (!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    // Abrir la URL en el navegador predeterminado del sistema
                    Process.Start(url);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir la URL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese una URL válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Combo(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado en el ComboBox
            DataRowView selectedRow = (DataRowView)comboBox1.SelectedItem;

            // Verificar si el valor seleccionado no es nulo
            if (selectedRow != null)
            {

                // Obtener y mostrar el valor de la columna "lbl" en el Label
                textBox5.Text = selectedRow["id"].ToString();
                textBox2.Text = selectedRow["user"].ToString();
                textBox1.Text = selectedRow["pass"].ToString();
                textBox4.Text = selectedRow["url"].ToString();
                textBox3.Text = selectedRow["name"].ToString();

            }
            else
            {
                comboBox1.SelectedIndex = -1;
            }
            comboBox1.SelectedIndex = -1;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = textBox3.Text;

            // Verificar si se ha seleccionado un nombre
            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un nombre antes de continuar.");
                return;
            }

            // Crear la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Definir la consulta SQL para eliminar el registro de la tabla Forms
                    string query = @"DELETE FROM pssw WHERE name = @Name;";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    // (SqlConnection connection = new SqlConnection(connectionString))

                    // Asignar el nombre seleccionado como valor al parámetro
                    command.Parameters.AddWithValue("@Name", nombreSeleccionado);

                    // Ejecutar la consulta
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro eliminado exitosamente de la base de datos.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún registro con el nombre seleccionado en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el registro de la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            comboBox1.SelectedIndex = -1;
            textBox2.Text = "";
            textBox1.Text = "";
            textBox4.Text = "";
            CargarDatosComboBox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            // Obtener los valores de los labels
            string IdI =            textBox5.Text;
            string nombre =         textBox3.Text;
            string usuario =        textBox2.Text;
            string contrasena =     textBox1.Text;
            string url =            textBox4.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese un valor para el nombre  antes de modificar.");
                return;
            }

            // Verificar si se ha ingresado un nombre
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre antes de continuar.");
                return;
            }

            // Crear la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Definir la consulta SQL para modificar el registro en la tabla Forms

                    string query = "UPDATE pssw SET id= @ID, name= @Name, user = @User, pass = @Pass, url = @Url WHERE Id = @ID;";


                    MySqlCommand command = new MySqlCommand(query, connection);
                    //using (SqlConnection connection = new SqlConnection(connectionString))

                    // Asignar valores a los parámetros
                    command.Parameters.AddWithValue("@ID", IdI);
                    command.Parameters.AddWithValue("@Name", nombre);
                    command.Parameters.AddWithValue("@User", usuario);
                    command.Parameters.AddWithValue("@Pass", contrasena);
                    command.Parameters.AddWithValue("@Url", url);

                    // Ejecutar la consulta
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro modificado exitosamente en la base de datos.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún registro con el nombre especificado en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el registro en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            comboBox1.SelectedIndex = -1;
            textBox2.Text = "";
            textBox1.Text = "";
            textBox4.Text = "";
            CargarDatosComboBox();
        }
    }
}
