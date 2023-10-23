namespace GameMania.Dados;
using GameMania.Modelos;

public class JogoMemDAO : JogoDAO{
    private Dictionary<string,Jogo> jogos = new(){
        ["FarCry"] = new Jogo("FarCry", "Acao/Aventura", "Ubisoft", "3", "3edicao", true, "AllPlatform", notas: new List<int> { 9, 8, 8 }),
        ["GTA"] = new Jogo("GTA", "Acao/Aventura", "RockStar", "5", "5edicao", true, "AllPlatform", notas: new List<int> { 9, 8, 9 }),
        ["Skyrim"] = new Jogo("Skyrim", "RPG", "Bethesda", "5", "5edicao", false, "AllPlatform", notas: new List<int> { 10, 10, 10 }),
        ["Valorant"] = new Jogo("Valorant", "FPS", "Riot", "1", "1edicao", false, "AllPlatform", notas: new List<int> { 0, 0, 0 })
    };  
    private static JogoMemDAO? instancia;

    public static JogoMemDAO GetInstance(){
        if (instancia == null){
            instancia = new JogoMemDAO();
        }
        return instancia;
    }

    public override void SalvarJogo(Jogo jogo){
        jogo.Nome = string.IsNullOrEmpty(jogo.Nome)?"":jogo.Nome;
        jogos[jogo.Nome] = jogo;
        Console.WriteLine("Jogo Adicionado Com Sucesso");
    }

    public override List<Jogo> ExibirJogosCadastrados(){
        return jogos.Values.ToList();
    }
    public override Jogo? JogoPorTitulo(string nome){
        if (jogos.ContainsKey(nome)){
            return jogos[nome];
        }else{
            return null;
        }        
    }

}
