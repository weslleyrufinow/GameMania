namespace GameMania.Dados;
using GameMania.Modelos;
using Microsoft.Data.Sqlite;

public class JogoSQLiteDAO : JogoDAO{

    private static JogoSQLiteDAO? instancia;

    public static JogoSQLiteDAO GetInstance(){
        if (instancia == null){
            instancia = new JogoSQLiteDAO();
        }
        return instancia;
    }    
    private string connectionString = $"Data Source=wrgamemania.db";

    public override void SalvarJogo(Jogo jogo){
            //Removendo Warnings
            jogo.Nome = string.IsNullOrEmpty(jogo.Nome)?"":jogo.Nome;
            jogo.Edicao = string.IsNullOrEmpty(jogo.Edicao)?"":jogo.Edicao;
            jogo.Genero = string.IsNullOrEmpty(jogo.Genero)?"":jogo.Genero;
            jogo.Studio = string.IsNullOrEmpty(jogo.Studio)?"":jogo.Studio;
            jogo.Plataforma = string.IsNullOrEmpty(jogo.Plataforma)?"":jogo.Plataforma;
        using (SqliteConnection connection = new SqliteConnection(connectionString)){
            connection.Open();
            using (SqliteTransaction transaction = connection.BeginTransaction()){
                try{
                    int i = 1;
                    Console.WriteLine("entrou ",i++);
                    using(SqliteCommand command = connection.CreateCommand()){
                        command.CommandText =
                        @"
                            INSERT INTO Jogo (Nome, Edicao, Descricao, Disponibilidade)
                            VALUES (@nome, @edicao, @descricao, @disponibilidade);          
                        ";
                        command.Parameters.AddWithValue("@nome", jogo.Nome.ToLower());
                        command.Parameters.AddWithValue("@edicao", jogo.Edicao.ToLower());
                        command.Parameters.AddWithValue("@descricao", jogo.Descricao);
                        command.Parameters.AddWithValue("@disponibilidade", jogo.Disponibilidade);
                        command.ExecuteNonQuery();
                    }
                    int jogoID;
                    Console.WriteLine("entrou ",i++);
                    using(SqliteCommand command = connection.CreateCommand()){
                        command.CommandText = "SELECT ID FROM Jogo WHERE Nome = @nome AND Edicao = @edicao;";
                        command.Parameters.AddWithValue("@nome", jogo.Nome.ToLower());
                        command.Parameters.AddWithValue("@edicao", jogo.Edicao.ToLower());
                        jogoID = Convert.ToInt32(command.ExecuteScalar());
                    }
                    Console.WriteLine("entrou ",i++);
                    using(SqliteCommand command = connection.CreateCommand()){
                        command.CommandText =
                        @"
                            INSERT INTO JogoGenero (JogoID, GeneroID)
                            VALUES 
                            (@jogoID, (SELECT ID FROM Genero WHERE Nome = @genero));
                        ";
                        command.Parameters.AddWithValue("@jogoID", jogoID);
                        command.Parameters.AddWithValue("@genero", jogo.Genero.ToLower());
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("entrou ",i++);
                    using(SqliteCommand command = connection.CreateCommand()){
                        command.CommandText =
                        @"
                            INSERT INTO JogoStudio (JogoID, StudioID)
                            VALUES 
                            (@jogoID, (SELECT ID FROM Studio WHERE Nome = @studio));
                        ";
                        command.Parameters.AddWithValue("@jogoID", jogoID);
                        command.Parameters.AddWithValue("@studio", jogo.Studio.ToLower());
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("entrou ",i++);
                    using(SqliteCommand command = connection.CreateCommand()){
                        command.CommandText =
                        @"
                            INSERT INTO JogoPlataforma (JogoID, PlataformaID)
                            VALUES 
                            (@jogoID, (SELECT ID FROM Plataforma WHERE Nome = @plataforma));
                        ";
                        command.Parameters.AddWithValue("@jogoID", jogoID);
                        command.Parameters.AddWithValue("@plataforma", jogo.Plataforma.ToLower());
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("entrou ",i++);
                    if(jogo.Notas == null){
                        jogo.Notas = new List<int>();
                    }                    
                    if(jogo.Disponibilidade == true && jogo.Notas.Count > 0){
                        using(SqliteCommand command = connection.CreateCommand()){

                            foreach (int nota in jogo.Notas){
                                command.Parameters.Clear(); //Evitar erro
                                command.CommandText = 
                                @"
                                    INSERT INTO JogoNota (JogoID, NotaID)
                                    VALUES 
                                    (@jogoID, (SELECT ID FROM Nota WHERE Nota = @nota));
                                ";
                                command.Parameters.AddWithValue("@jogoID", jogoID);
                                command.Parameters.AddWithValue("@nota", nota);
                                command.ExecuteNonQuery();
                            }
                        }
                        Console.WriteLine("entrou ",i++);
                    }
                    transaction.Commit();
                    Console.WriteLine("Jogo Adicionado Com Sucesso");                                                                    

                }catch (Exception){
                    transaction.Rollback();
                    Console.WriteLine("Erro ao cadastrar o jogo");
                }
            }
        }
    }

    public override List<Jogo> ExibirJogosCadastrados(){
        List<Jogo> jogos = new List<Jogo>();
        using (SqliteConnection connection = new SqliteConnection(connectionString)){
            connection.Open();
            using (SqliteTransaction transaction = connection.BeginTransaction()){
                try{
                    using(SqliteCommand command = connection.CreateCommand()){
                        command.CommandText =
                        @"
                            SELECT Jogo.Nome, jogo.Edicao
                            FROM Jogo;
                        ";
                        using (SqliteDataReader reader = command.ExecuteReader()){
                            while (reader.Read()){
                                Jogo jogo = new Jogo(
                                    nome:reader.GetString(0),
                                    edicao:reader.GetString(1)
                                );
                                jogos.Add(jogo);
                            }
                            
                        }                        
                    }
                }catch (Exception){
                    transaction.Rollback();
                    Console.WriteLine("Erro ao Exibir Jogo Cadastrado");
                }
            }
        }
        return jogos;
    }

    public override Jogo? JogoPorTitulo(string titulo){
        using (SqliteConnection connection = new SqliteConnection(connectionString)){
            connection.Open();
            using (SqliteTransaction transaction = connection.BeginTransaction()){
                try{
                    using (SqliteCommand command = connection.CreateCommand()){
                        command.CommandText =
                        @"
                            SELECT Jogo.ID, Jogo.Nome, jogo.Edicao, jogo.Descricao, Jogo.Disponibilidade
                            FROM Jogo
                            WHERE Jogo.Nome = @nome;
                        ";
                        command.Parameters.AddWithValue("@nome", titulo);

                        using (SqliteDataReader reader = command.ExecuteReader()){
                            if (reader.Read()){
                                Jogo jogo = new Jogo(
                                    nome: reader.IsDBNull(1) ? null : reader.GetString(1),
                                    edicao: reader.IsDBNull(2) ? null : reader.GetString(2),
                                    descricao: reader.IsDBNull(3) ? null : reader.GetString(3),
                                    disponibilidade: reader.GetBoolean(4)
                                );
                                return jogo;
                            }
                        }
                    }
                }
                catch (Exception){
                    transaction.Rollback();
                    Console.WriteLine("Erro ao Adquirir Jogo Por Título");
                }
            }
        }
        return null;
    }
}
