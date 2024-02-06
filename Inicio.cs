using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
namespace Contraseñas
{
    public partial class Inicio : Form
    {
        private string connectionString = "Data Source=BAYRON\\SQLLEARNING;Initial Catalog=SecureKenGenData;Integrated Security=True;TrustServerCertificate=True";
        private bool sesion;
        public Inicio()
        {
            InitializeComponent();
            CargarDatosComboBox();
            comboBox1.SelectedIndexChanged += Combo;
            button3.Click += BorrarDato;

           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Creating f = new Creating();
            f.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (label3.Text != "")
            {
                try
                {
                    Clipboard.SetText(label3.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al copiar al portapapeles: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No hay nada para copiar al portapapeles.");
            }
        }
        private void CargarDatosComboBox()
        {
            // Crear la conexión
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Definir la consulta SQL para obtener los datos
                    string query = "SELECT * FROM Labels ";

                    // Crear el adaptador de datos y el conjunto de datos
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    // Llenar el conjunto de datos con los datos de la base de datos
                    adapter.Fill(dataSet, "Labels");

                    // Asignar los datos al ComboBox
                    comboBox1.DataSource = dataSet.Tables["Labels"];

                    comboBox1.DisplayMember = "pass";
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
        private void Combo(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado en el ComboBox
            DataRowView selectedRow = (DataRowView)comboBox1.SelectedItem;

            // Verificar si el valor seleccionado no es nulo
            if (selectedRow != null)
            {
                // Obtener y mostrar el valor de la columna "lbl" en el Label
                label3.Text = selectedRow["lbl"].ToString();
                label3.ForeColor = Color.DarkOrange;
            }
            else
            {
                label3.Text = "";
            }
        }
        private void BorrarDato(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                // Obtener el valor seleccionado en el ComboBox
                string nombreSeleccionado = ((DataRowView)comboBox1.SelectedItem)["pass"].ToString();

                // Llamar al método para borrar el dato en la base de datos
                BorrarDatoEnBD(nombreSeleccionado);

                // Volver a cargar los datos en el ComboBox después de borrar
                CargarDatosComboBox();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un elemento antes de intentar borrar.");
            }
        }
        private void BorrarDatoEnBD(string nombre)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "delete from Labels where pass = @Nombre";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar parámetro para prevenir la inyección de SQL
                        command.Parameters.AddWithValue("@Nombre", nombre);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                        MessageBox.Show("Datos Eliminados correctamente.");
                        label3.Text = "";
                        comboBox1.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al borrar dato en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            bool sesion = false;
            login g = new login();
            g.Show();
            this.Hide();
        }
        public bool ObtenerSesionEnInicio()
        {
            return sesion;
        }

        private void Inicio_Load(login sesionLog)
        {
            bool sesion = sesionLog.ObtenerSesionEnLogin();
            if (sesion.Equals(false))
            {
                login f = new login();
                f.Show();
                this.Hide();
            }
        }

        private void Inicio_Load(login sesionLog)
        {

        }
    }
}
