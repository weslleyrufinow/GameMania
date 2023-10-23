namespace GameMania.Menus;
using GameMania.Modelos;

internal class MenuAvaliarJogoCadastrado: Menu{
    public MenuAvaliarJogoCadastrado():base(titulo:"Avaliar Jogo Cadastrado"){

    }    
    public override void ExecutarOpcao(){
        Console.Write("Insira o Titulo do Jogo a Ser Avaliado | Digite -1 Para Cancelar: ");

        string? aux = Console.ReadLine();
        aux = string.IsNullOrEmpty(aux) ? "": aux;
        while((jogoDAO?.JogoPorTitulo(aux) == null) && aux != "-1"){
            Console.Write("Nome Invalido ou Nao Cadastrado. Tente Novamente: ");
            aux = Console.ReadLine();
            aux = string.IsNullOrEmpty(aux) ? "": aux;
        }

        if(aux != "-1"){
            Jogo? jogo = jogoDAO?.JogoPorTitulo(aux);
            if(jogo?.Disponibilidade == false || jogo == null){
                Console.WriteLine("Jogo Indisponivel Para Avaliacao.");
                return;
            }else{
                jogo.Notas = jogo.Notas==null? new List<int>() : jogo.Notas;
                jogo.Notas = ForcedValidationNotas(jogo.Notas); 
            }
            jogoDAO?.SalvarJogo(jogo);    
        }
    }
}