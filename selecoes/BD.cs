using MySql.Data.MySqlClient;
using System.Text.Json;

namespace selecoes
{
    public class BD
    {
        private static string StringConexao = Environment.GetEnvironmentVariable("MYSQL_URI")!;

        public static void InserirCadastro(Cadastro novoCadastro)
        {
            try
            {
                var mysqlConexao = new MySqlConnection(StringConexao);
                mysqlConexao.Open();
                var sql = "INSERT INTO cadastros(Cidade, turnos, data_termo_aceito) VALUES (@cidade, @turnos, @data_termo_aceito)";

                var mysqlComando = new MySqlCommand(sql, mysqlConexao);

                mysqlComando.Parameters.AddWithValue("@cidade", novoCadastro.Cidade);
                mysqlComando.Parameters.AddWithValue("@turnos", JsonSerializer.Serialize(novoCadastro.Turnos));
                mysqlComando.Parameters.AddWithValue("@data_termo_aceito", novoCadastro.DataTermoAceito);

                var linhasAfetadas = mysqlComando.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch
            {
                MessageBox.Show("Ocorreu um erro ao tentar inserir um novo cadastro.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Cadastro> RecuperarCadastros()
        {
            try
            {
                var mysqlConexao = new MySqlConnection(StringConexao);
                mysqlConexao.Open();

                var sql = "SELECT * from cadastros";

                var mysqlComando = new MySqlCommand(sql, mysqlConexao);

                var leitor = mysqlComando.ExecuteReader();

                List<Cadastro> cadastrosExternos = [];

                while (leitor.Read())
                {
                    var cadastroBanco = new Cadastro();

                    cadastroBanco.Cidade = leitor["cidade"].ToString()!;

                    cadastroBanco.Turnos = JsonSerializer.Deserialize<List<string>>(leitor["turnos"].ToString()!)!;

                    cadastroBanco.DataTermoAceito = Convert.ToDateTime(leitor["data_termo_aceito"]);

                    cadastrosExternos.Add(cadastroBanco);
                }
                return cadastrosExternos;
            }
            catch
            {
                MessageBox.Show("Não foi possível recuperar os cadastros salvos no banco de dados!", "Erro ao recuperar dados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return [];
            }
        }
    }
}
