namespace GameMania.Menus;
using GameMania.Modelos;

internal class MenuCadastrarNovoJogo: Menu{
    public MenuCadastrarNovoJogo():base(titulo:"Cadastrar Novo Jogo"){

    }    
    public override void ExecutarOpcao(){
        Console.WriteLine("Cadastrar Um Novo Jogo | Digite -1 Para Cancelar: ");
        Console.Write("Titulo: ");
        string? aux = Console.ReadLine();
        aux = string.IsNullOrEmpty(aux) ? "" : aux;

        while((string.IsNullOrEmpty(aux) || jogoDAO?.JogoPorTitulo(aux)!=null) && aux != "-1"){
            Console.Write("Nome Invalido Ou Ja Cadastrado. Tente Novamente: ");
            aux = Console.ReadLine();
            aux = string.IsNullOrEmpty(aux) ? "" : aux;
        }
        
        if(aux != "-1"){
            Jogo? jogo = new(nome:aux);
            Console.Write("Genero: ");
            aux = Console.ReadLine();
            jogo.Genero = ForcedValidationString(aux);

            Console.Write("Studio: ");
            aux = Console.ReadLine();
            jogo.Studio = ForcedValidationString(aux);        

            Console.Write("Edicao: ");
            aux = Console.ReadLine();
            jogo.Edicao = ForcedValidationString(aux);

            Console.Write("Plataformas: ");
            aux = Console.ReadLine();
            jogo.Plataforma = ForcedValidationString(aux);

            Console.Write("Disponivel Para Avaliacao? (1 == Sim | 0 == Nao)");
            while (true){
                aux = Console.ReadLine();
                if (aux == "1" || aux == "Sim"){
                    jogo.Disponibilidade=true;
                    break;
                }else if (aux == "0" || aux == "Nao"){
                    jogo.Disponibilidade=false;
                    break;
                }else{
                    Console.Write("Tente Novamente: ");
                }
            }
            
            if(jogo.Disponibilidade == true){
                jogo.Notas = jogo.Notas==null? new List<int>() : jogo.Notas;
                jogo.Notas = ForcedValidationNotas(jogo.Notas);
            }

            jogo.Nome = string.IsNullOrEmpty(jogo.Nome)?"":jogo.Nome;
            jogoDAO?.SalvarJogo(jogo);    
        }else{
            Console.WriteLine("Cadastro Cancelado");
        }
    }
}