using System.ComponentModel;

namespace selecoes;

public partial class Form1 : Form
{
    private List<string> listaCidade = ["Vitória", "Vila Velha", "Serra", "Cariacica", "Viana", "Guarapari", "Fundão"];
    private BindingList<Cadastro> listaCadastros = [];
    
    public Form1()
    {
        InitializeComponent();
        cmbCidades.DataSource = listaCidade;
        dgvCadastros.DataSource = listaCadastros;

        if (dgvCadastros.Columns.Contains("TurnosString"))
        {
            dgvCadastros.Columns["TurnosString"].HeaderText = "Turnos";
        }
        if (dgvCadastros.Columns.Contains("DataTermoAceito"))
        {
            dgvCadastros.Columns["DataTermoAceito"].HeaderText = "Aceito Em";
        }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        btnCadastrar.Enabled = chkTermosPoliticas.Checked;
    }

    private void btnCadastrar_Click(object sender, EventArgs e)
    {
        if (!chkMatutino.Checked && !chkVespertino.Checked && !chkNoturno.Checked)
        {
            MessageBox.Show("Você precisa selecionar algum turno!", "Turnos não selecionados!", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
            
        }

        if (!chkTermosPoliticas.Checked)
        {
            MessageBox.Show("Aceite os termos e políticas para continuar!", "Termo e Políticas");
            return;
        }

        var novoCadastro = new Cadastro();
        novoCadastro.Cidade = cmbCidades.Text;
        if(chkMatutino.Checked) novoCadastro.Turnos.Add("Matutino");
        if(chkVespertino.Checked) novoCadastro.Turnos.Add("Vespertio");
        if(chkNoturno.Checked) novoCadastro.Turnos.Add("Noturno");
        novoCadastro.DataTermoAceito = DateTime.Now;
        listaCadastros.Add(novoCadastro);
    }
}

public class Cadastro
{
    public string Cidade { get; set; }
    public List<string> Turnos { get; set; } = [];
    public DateTime DataTermoAceito { get; set; }
    public string TurnosString {
        get 
        {
            return string.Join(",", Turnos);
        }
    }
}