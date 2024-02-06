using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Contraseñas
{
    public partial class Creating : Form{
        private string connectionString = "Data Source=BAYRON\\SQLLEARNING;Initial Catalog=SecureKenGenData;Integrated Security=True;TrustServerCertificate=True";
        private Random random = new Random();
        private System.Windows.Forms.CheckBox[] checkboxes;
        private Label miLabel;
        public Creating()
        {
            InitializeComponent();
            ////
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            button1.Click += new EventHandler(button1_Click);
            ////
            checkboxes = new System.Windows.Forms.CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4 };
            // Asignar el mismo controlador de eventos a los cuatro checkboxes
            foreach (System.Windows.Forms.CheckBox checkbox in checkboxes)
            {
                checkbox.CheckedChanged += CheckBox1_CheckedChanged;
            }
            checkboxes[0].Checked = true;
        }
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
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
            label4.Text = $"{password}";
            label5.Text = trackBar1.Value.ToString();
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
        private void button1_Click(object sender, EventArgs e)
        {
            string password = GenerarContrasena(trackBar1.Value, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked);
            label4.Text = $"{password}";
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
            savePass f = new savePass(this);
            f.Show();
            this.Hide();
        }
        public string ObtenerTextoDeLabelEnForm1()
        {
            return label4.Text;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
            Inicio f = new Inicio();
            f.Show();
            this.Hide();
        }
    }
}
