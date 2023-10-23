namespace GameMania.Modelos;
public class Jogo {
    private string? nome;
    public string? Nome {
        get{ 
            return string.IsNullOrEmpty(nome)?"":nome;
        } 
        set{ 
            nome = string.IsNullOrEmpty(value)?"":value;
        }
    }
    private string? edicao;
    public string? Edicao {
        get{ 
            return string.IsNullOrEmpty(edicao)?"":edicao;
        } 
        set{ 
            edicao = string.IsNullOrEmpty(value)?"":value;
        }
    }
    private string? genero;
    public string? Genero {
        get{ 
            return string.IsNullOrEmpty(genero)?"":genero;
        } 
        set{ 
            genero = string.IsNullOrEmpty(value)?"":value;
        }
    }
    private string? studio;
    public string? Studio {
        get{ 
            return string.IsNullOrEmpty(studio)?"":studio;
        } 
        set{ 
            studio = string.IsNullOrEmpty(value)?"":value;
        }
    }
    private string? descricao;
    public string? Descricao {
        get{ 
            return string.IsNullOrEmpty(descricao)?$"{nome} - {Studio}":descricao;
        }
        set{ 
            descricao = string.IsNullOrEmpty(value)?"":value;
        }        
    }
    private bool disponibilidade;
    public bool Disponibilidade {
        get{ 
            return disponibilidade;
        } 
        set{ 
            disponibilidade = value;
        }
    }
    private string? plataforma;
    public string? Plataforma {
        get{ 
            return string.IsNullOrEmpty(plataforma)?"":plataforma;
        } 
        set{ 
            plataforma = string.IsNullOrEmpty(value)?"":value;
        }
    }
    private List<int> notas = new();
    public List<int>? Notas {
        get{ 
            return notas==null ? new List<int>{}:notas;
        } 
        set{ 
            notas = value==null ? new List<int>{}:value;
        }
    }
    public Jogo(string? nome="", string? edicao="",string? genero="", string? studio="",  string? descricao="", bool disponibilidade=false, string? plataforma="", List<int>? notas = null){
        Nome = nome;
        Edicao = edicao;
        Descricao = descricao;
        Disponibilidade = disponibilidade;
        Genero = genero;
        Studio = studio;
        Plataforma = plataforma;
        Notas = notas ?? new List<int>(){};
    }

    public float AvaliacaoMedia {
        get {
            Notas = (Notas==null)?new List<int>{0}:Notas;
            if (Notas.Count == 0) {
                return 0;
            }
            float soma = 0;
            foreach (int nota in Notas) {
                soma += nota;
            }
            return soma/Notas.Count;
        }    
    }
    public void ExibirFichaTecnica() {
        Console.WriteLine($"Título: {nome}");
        Console.WriteLine($"Gênero: {Genero}");
        Console.WriteLine($"Estúdio: {Studio}");    
        Console.WriteLine($"Edição: {Edicao}");
        Console.WriteLine($"Descricao: {Descricao}");
        Console.WriteLine($"Plataforma: {Plataforma}");
        Console.Write($"Notas: ");
        Notas = (Notas==null)?new List<int>{0}:Notas;
        foreach (int nota in Notas){
                Console.Write(nota + " ");
        }
        Console.WriteLine($"\nMedia: {AvaliacaoMedia:F2}");
        if(Disponibilidade) {
            Console.WriteLine("Disponível para avaliação.");
        } else {
            Console.WriteLine("Não disponível para avaliação.");
        }
    }

}



