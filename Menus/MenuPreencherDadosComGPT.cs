namespace GameMania.Menus;
using GameMania.Modelos;
using System;
using System.Text.Json;

internal class MenuPreencherDadosComGPT : Menu{
    public MenuPreencherDadosComGPT() : base(titulo: "Preenchendo Dados Com GPT"){

    }
    public override void ExecutarOpcao(){
        Console.WriteLine("Cadastrar Um Novo Jogo Usando ChatGPT | Digite -1 Para Cancelar: ");
        Console.Write("Titulo: ");
        string? aux = Console.ReadLine();
        aux = string.IsNullOrEmpty(aux) ? "" : aux;

        while ((string.IsNullOrEmpty(aux) || jogoDAO?.JogoPorTitulo(aux) != null) && aux != "-1"){
            Console.Write("Nome Invalido Ou Ja Cadastrado. Tente Novamente: ");
            aux = Console.ReadLine();
            aux = string.IsNullOrEmpty(aux) ? "" : aux;
        }
        if (aux != "-1"){
            string? jsonString = RequisitarAPI(titulo: aux);
            if (string.IsNullOrEmpty(jsonString)){
                return;
            }
            Jogo? jogo = JsonSerializer.Deserialize<Jogo>(jsonString);
            jogo = jogo == null ? new Jogo() : jogo;
            jogo.Nome = aux;
            jogo.Nome = string.IsNullOrEmpty(jogo.Nome) ? "" : jogo.Nome;
            jogoDAO?.SalvarJogo(jogo);
            jogo.ExibirFichaTecnica();
            Console.WriteLine("Jogo Adicionado Com Sucesso");
        }else{
            Console.WriteLine("Cadastro Cancelado");
        }
    }
    public virtual string RequisitarAPI(string titulo){
        try{
            Console.WriteLine("Requisitando...");
            var client = new OpenAI_API.OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_KEY"));
            var chat = client.Chat.CreateConversation();
            string prompt = $@"Para o jogo ""{titulo}"", preencha os campos adequadamente e retorne apenas o arquivo JSON preenchido. Descricao: Resuma o jogo de forma divertida e breve. Não repita informações de outros campos:
            {{
                ""Genero"": """",
                ""Studio"": """",
                ""Edicao"": """",
                ""Descricao"": """",
                ""Disponibilidade"": true || false,
                ""Plataformas"": """"
            }}";

            chat.AppendSystemMessage(prompt);
            string resposta = chat.GetResponseFromChatbotAsync().Result;
            //Console.WriteLine("Resposta do ChatGPT: " + resposta);
            Console.WriteLine("Feito!");
            return resposta;
        }catch (Exception){
            Console.WriteLine("Erro na API - Tarefa Cancelada - Pressione Qualquer Tecla");
            Console.ReadKey();
            return "";
        }
    }
}
