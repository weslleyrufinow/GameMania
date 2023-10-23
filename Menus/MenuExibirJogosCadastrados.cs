namespace GameMania.Menus;

using System.Globalization;
using GameMania.Modelos;

internal class MenuExibirJogosCadastrados: Menu{
    public MenuExibirJogosCadastrados():base(titulo:"Exibir Jogos Cadastrados"){

    }    
    public override void ExecutarOpcao(){
        TextInfo textInfo = new CultureInfo("pt-BR").TextInfo;
        List<Jogo>? jogos = jogoDAO?.ExibirJogosCadastrados();
        if (jogos != null){
            foreach (Jogo jogo in jogos){
                jogo.Nome = (jogo.Nome==null)? "": jogo.Nome;
                jogo.Edicao = (jogo.Edicao==null)? "": jogo.Edicao;
                Console.WriteLine($"{textInfo.ToTitleCase(jogo.Nome)} {textInfo.ToTitleCase(jogo.Edicao)}");
            }
        }else{
            Console.WriteLine("Nenhum Jogo Disponível");
        }
    }
}