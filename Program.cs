using System;
using System.Linq;

namespace Atividade2EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var context = new BancoContext();
            InitOperator(context);
            int menu = 0;
            int opcao = 1;
            for (; opcao != 0;)
            {

                if (menu == 0) menu = Menu(opcao);
                opcao = menu;
                if (menu == 1) menu = selcionarConta(opcao, context);
                else if (menu == 2) menu = abrirConta(context);
                if (opcao == 100) menu = 0;

            }
            Console.Clear();
            Console.WriteLine("Volte sempre!");
        }

        public static void InitOperator(BancoContext context)
        {
            if (!context.Bancos.Any())
            {
                
                var newBanco = new Banco() { Nome = "Banco Sol" };
                context.Add(newBanco);
                context.SaveChanges();

                if (!context.Agencias.Any())
                {
                    var newAgencia = new Agencia() { Numero = "01", Banco = newBanco };
                    context.Add(newAgencia);
                    context.SaveChanges();
                }


            }

        }

        public static int Menu(int opcao)
        {
            
            Console.Clear();
            Console.WriteLine("*** Menu Inicial ***");
            Console.WriteLine("1 - Para acessar conta");
            Console.WriteLine("2 - Para criar uma conta");
            Console.WriteLine("3 - Para sair");

            
            try
            {
                opcao = Int32.Parse(Console.ReadLine());
            }
            catch (Exception erro)
            {
                Console.Clear();
                erro.ToString();
                Console.WriteLine("Atenção, esta opção é inválida!");
                opcao = 10;
            }
            return opcao;
        }

        static int selcionarConta(int opcao, BancoContext context)
        {
             
            Conta conta = new Conta();
            bool access = false;
            for (; opcao != 0;)
            {
                Console.Clear();
                Console.WriteLine("*** Menu ***");
                Console.WriteLine("1 - Para Conta Corrente");
                Console.WriteLine("2 - Para Conta Poupança");
                Console.WriteLine("0 - Para Sair");

                try
                {
                    opcao = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    opcao = 10;
                }
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        conta = verificarContaCorrente(opcao, context);
                        if (conta == null)
                        {
                            access = false;
                        }
                        else if (conta != null)
                        {
                            access = true;
                        }
                        if (access == true) menuContaCorrente(opcao, context, conta);
                        break;

                    case 2:
                        Console.Clear();
                        conta = verificarContaPoupanca(opcao, context);
                        if (conta == null)
                        {
                            access = false;
                        }
                        else if (conta != null)
                        {
                            access = true;
                        }
                        if (access == true) menuContaPoupanca(opcao, context, conta);
                        break;

                    case 0: break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Atenção, está opção é inválida!");
                        break;
                }
            }

            return opcao;
        }

        static void menuContaCorrente(int opcao, BancoContext context, Conta conta)
        {
            Console.Clear();
            Console.WriteLine("Seja Bem-vindo(a)" + conta.Titular + " ao Banco Sol");
            for (; opcao != 0;)
            {
                
                Console.WriteLine("***Conta Corrente***");
                Console.WriteLine("Opções Disponíveis:");
                Console.WriteLine("1 - Para Sacar");
                Console.WriteLine("2 - Para Depositar");
                Console.WriteLine("3 - Para Consulta de Saldo");
                Console.WriteLine("4 - Para Atualização de Dados Cadastrais");
                Console.WriteLine("5 - Para Exclusão de Conta");
                Console.WriteLine("0 - Para sair");
                Console.WriteLine(" ");
                try
                {
                    opcao = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    opcao = 10;
                }
                Console.Clear();
                switch (opcao)
                {
                    case 1:
                        sacarCorrente(conta, context);
                        break;

                    case 2:
                        depositarCorrente(conta, context);
                        break;

                    case 3:
                        Saldo(conta);
                        break;

                    case 4:
                        AtualizarDados(conta, context, 1);
                        break;

                    case 5:
                        opcao = deletarConta(context, conta, 1);
                        break;

                    case 6:
                        opcao = 0;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Atenção, esta opção é inválida.");
                        Console.WriteLine("Por favor, escolha uma das opções disponíveis!");
                        Console.WriteLine(" ");
                        opcao = 1;
                        break;
                }
            }
        }

        static void menuContaPoupanca(int opcao, BancoContext context, Conta conta)
        {
            
            Console.Clear();
            Console.WriteLine("Seja bem-vindo(a) " + conta.Titular + " ao Banco Sol!");
            for (; opcao != 0;)
            {
                Console.Clear();
                Console.WriteLine(" ");
                Console.WriteLine("************ Conta Poupança ***************");
                Console.WriteLine("Escolha uma das opções disponíveis a seguir:");
                Console.WriteLine("1 - Para saque");
                Console.WriteLine("2 - Para depósito");
                Console.WriteLine("3 - Para visualizar saldo");
                Console.WriteLine("4 - Para atualização de Dados Cadastrais da conta");
                Console.WriteLine("5 - Para exclusão de conta");
                Console.WriteLine("0 - Para voltar ao Menu Inicial");
                Console.WriteLine(" ");

                
                try
                {
                    opcao = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    opcao = 10;
                }
                Console.Clear();
                switch (opcao)
                {
                    case 1:
                        sacarPoupanca(conta, context);
                        break;

                    case 2:
                        depositarPoupanca(conta, context);
                        break;

                    case 3:
                        Saldo(conta);
                        break;

                    case 4:
                        AtualizarDados(conta, context, 2);
                        break;

                    case 5:
                        opcao = deletarConta(context, conta, 2);
                        break;

                    case 6:
                        opcao = 0;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Atenção, esta opção é inválida!");
                        opcao = 1;
                        break;
                }
            }
        }
        public static int abrirConta(BancoContext context)
        {
            
            string cpf;
            int idade;
            string agencia;
            Agencia agenciaCliente = new Agencia();
            string nome;
            Console.WriteLine("Informe o CPF do titular: ");
            cpf = Console.ReadLine();

            
            try
            {
                var clienteCadastrado = context.Clientes.Where(b => b.Cpf == cpf).FirstOrDefault();
                if (clienteCadastrado != null)
                {
                    Console.WriteLine("Atenção, este CPF já foi cadastrado!");
                    return 0;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Tente novamente mais tarde!");
                return 0;

            }
            Console.Clear();
            Console.WriteLine("Informe o seu nome: ");
            nome = Console.ReadLine();
            Console.WriteLine("Informe a sua idade: ");
            
            try
            {
                idade = Int32.Parse(Console.ReadLine());
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("Atenção, idade incorreta!");
                return 0;
            }
            Console.WriteLine("Escolha uma das agências disponíveis: ");
            var bancos = context.Set<Banco>();
            foreach (var b in bancos)
            {
                if (b.Nome == "Banco Sol")
                    Console.WriteLine("--------" + b.Nome + "--------");
            }
            var agencias = context.Set<Agencia>();
            foreach (var a in agencias)
            {
                Console.WriteLine(a.Numero);
            }
            Console.WriteLine(" ");
            bool error = true;
            for (; error != false;)
            {
                Console.WriteLine("Informe a agência escolhida: ");
                agencia = Console.ReadLine();
                try
                {
                    var agenciaSelecionada = context.Agencias.Where(b => b.Numero == agencia).FirstOrDefault();
                    agenciaCliente = agenciaSelecionada;
                    error = false;
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    Console.Clear();
                    Console.WriteLine("Atenção, está agência não existe!");
                    error = true;
                }
            }
            int contaType = 0;
            for (; contaType != 1 && contaType != 2;)
            {
                Console.WriteLine("Selecione o tipo de conta escolhido: ");
                Console.WriteLine(" ");
                Console.WriteLine("1 - Conta Corrente");
                Console.WriteLine("2 - Conta Poupança");
                try
                {
                    contaType = Int32.Parse(Console.ReadLine());
                }
                catch (Exception erro)
                {
                    erro.ToString();
                    contaType = 100;
                }
                switch (contaType)
                {
                    case 1:
                        abrirContaCorrente(cpf, nome, idade, agenciaCliente, context);
                        break;

                    case 2:
                        abrirContaPoupanca(cpf, nome, idade, agenciaCliente, context);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Atenção, este tipo de conta não existe!");
                        Console.WriteLine(" ");
                        break;
                }
            }
            return 0;
        }

        public static void abrirContaCorrente(string cpf, string nome, int idade, Agencia agencia, BancoContext context)
        {
            try
            {
                var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };
                context.Add(newCliente);
                context.SaveChanges();
                decimal saldo = 0;
                var newConta = new Conta() { Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome };
                context.Add(newConta);
                context.SaveChanges();
                var newContaCorrente = new ContaCorrente() { Conta = newConta, Taxa = 0.10M };
                context.Add(newContaCorrente);
                context.SaveChanges();
                Console.Clear();
                Console.WriteLine("Cadastro de conta efetuado com sucesso");
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine(erro);
                Console.WriteLine("Atenção, não foi possível realizar esta ação!");

            }

        }

        public static void abrirContaPoupanca(string cpf, string nome, int idade, Agencia agencia, BancoContext context)
        {
            try
            {
                var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };
                context.Add(newCliente);
                context.SaveChanges();
                decimal saldo = 0;
                var newConta = new Conta() { Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome };
                context.Add(newConta);
                context.SaveChanges();
                decimal taxaJuros = 0;
                var newContaPoupanca = new ContaPoupanca() { Conta = newConta, TaxaJuros = taxaJuros };
                context.Add(newContaPoupanca);
                context.SaveChanges();
                Console.Clear();
                Console.WriteLine("Esta operação foi efetuada com sucesso!");

            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.Clear();
                Console.WriteLine("Atenção, erro ao realizar esta ação!");

            }

        }

        public static Conta verificarContaCorrente(int opcao, BancoContext context)
        {
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("Informe o nome do titular: ");
            nome = Console.ReadLine();
            Console.WriteLine("Informe o CPF do titular: ");
            cpf = Console.ReadLine();
            try
            {
                var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente).FirstOrDefault();
                var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta).FirstOrDefault();
                if (contaCorrente == null)
                {
                    Console.Clear();
                    Console.WriteLine("Atenção, erro ao tentar encontrar esta conta!");
                    return null;
                }
            }
            catch (Exception erro)
            {
                Console.Clear();
                erro.ToString();
                Console.WriteLine("Atenção, erro ao tentar encontrar esta conta!");
                return null;
            }
            return conta;
        }

        public static Conta verificarContaPoupanca(int opcao, BancoContext context)
        {
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("Informe o nome do titular: ");
            nome = Console.ReadLine();
            Console.WriteLine("Informe o CPF do titular: ");
            cpf = Console.ReadLine();
            try
            {
                var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente).FirstOrDefault();
                var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta).FirstOrDefault();

                if (contaPoupanca == null)
                {
                    Console.Clear();
                    Console.WriteLine("Atenção, erro ao tentar encontrar esta conta!");
                    return null;
                }
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("Atenção, erro ao tentar encontrar esta conta!");
                return null;
            }
            return conta;
        }

        static void sacarCorrente(Conta conta, BancoContext context)
        {
            Console.WriteLine("Informe o valor do saque: ");
            decimal saque;
            
            try
            {
                saque = Decimal.Parse(Console.ReadLine());
                conta.Sacar(saque, conta, context, 1);
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("Atenção, este valor é inválido!");
            }
        }

        static void depositarCorrente(Conta conta, BancoContext context)
        {
            Console.WriteLine("Informe o valor do depósito: ");
            decimal deposito;
            try
            {
                deposito = Decimal.Parse(Console.ReadLine());
                conta.Depositar(deposito, conta, context, 1);
            }
            catch (Exception e)
            {
                e.ToString();
                Console.WriteLine("Atenção, este valor é inválido!");
            }
        }

        static void sacarPoupanca(Conta conta, BancoContext context)
        {
            Console.WriteLine("Informe o valor do saque: ");
            decimal saque;
            try
            {
                saque = Decimal.Parse(Console.ReadLine());
                conta.Sacar(saque, conta, context, 2);
            }
            catch (Exception e)
            {
                e.ToString();
                Console.WriteLine("Atenção, este valor é inválido!");
            }
        }

        static void depositarPoupanca(Conta conta, BancoContext context)
        {
            Console.WriteLine("Informe o valor do depósito: ");
            decimal deposito;
            
            try
            {
                deposito = Decimal.Parse(Console.ReadLine());
                conta.Depositar(deposito, conta, context, 2);
            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.WriteLine("Atenção, este valor é inválido!");
            }
        }

        static void Saldo(Conta conta)
        {
            Console.Clear();
            Console.WriteLine("Saldo atual: " + conta.Saldo);
        }

        static void AtualizarDados(Conta conta, BancoContext context, int opcao)
        {
            Console.Clear();
            Console.WriteLine("****** Atualização de Dados Cadastrais *******");
            Console.WriteLine("Informe o nome do titular: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Informe o CPF do titular: ");
            string cpf = Console.ReadLine();
            if (opcao == 1)
            {
                try
                {
                    var clienteC = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta).FirstOrDefault();
                    clienteC.Atualizar(conta, clienteC, context);
                }
                catch (Exception erro)
                {
                    Console.Clear();
                    erro.ToString();
                    Console.WriteLine("Atenção, dados cadastrais incorretos!");

                }
            }
            else if (opcao == 2)
            {
                try
                {
                    var clienteP = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta).FirstOrDefault();
                    clienteP.Atualizar(conta, clienteP, context);
                }
                catch (Exception erro)
                {
                    Console.Clear();
                    erro.ToString();
                    Console.WriteLine("Atenção, dados cadastrais incorretos!");
                }
            }
        }

        static int deletarConta(BancoContext context, Conta conta, int opcao)
        {
            
            Cliente cliente = new Cliente();
            ContaCorrente contaC = new ContaCorrente();
            ContaPoupanca contaP = new ContaPoupanca();
            int option = 0;
            Console.WriteLine("****** Atualização de Dados Cadastrais *******");
            Console.WriteLine("Informe o nome do titular: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Informe o CPF do titular: ");
            string cpf = Console.ReadLine();
            try
            {
                if (opcao == 1)
                {
                    cliente = context.Set<Cliente>().Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    contaC = context.Set<ContaCorrente>().Where(b => b.Conta == conta).FirstOrDefault();
                }
                else if (opcao == 2)
                {
                    cliente = context.Set<Cliente>().Where(b => b.Cpf == cpf && b.Nome == nome).FirstOrDefault();
                    contaP = context.Set<ContaPoupanca>().Where(b => b.Conta == conta).FirstOrDefault();
                }
                for (; option != 2;)
                {
                    Console.Clear();
                    Console.WriteLine("Tem certeza que deseja fazer a exclusão desta conta?");
                    Console.WriteLine("1 - Para Sim");
                    Console.WriteLine("2 - Para Não");
                    try
                    {
                        option = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception erro)
                    {
                        erro.ToString();
                        option = 10;
                    }
                    switch (option)
                    {
                        case 1:
                            if (opcao == 1)
                            {
                                var solicitacao = context.Set<Solicitacao>();
                                foreach (var s in solicitacao)
                                {
                                    if (s.Conta == conta)
                                    {
                                        context.Remove(s);
                                    }
                                }
                                context.Remove(contaC);
                                context.Remove(conta);
                                context.Remove(cliente);
                                context.SaveChanges();
                            }
                            else if (opcao == 2)
                            {
                                var solicitacao = context.Set<Solicitacao>();
                                foreach (var s in solicitacao)
                                {
                                    if (s.Conta == conta)
                                    {
                                        context.Remove(s);
                                    }
                                }
                                context.Remove(contaP);
                                context.Remove(conta);
                                context.Remove(cliente);
                                context.SaveChanges();
                            }
                            Console.WriteLine("Operação efetuada com sucessso!");


                            break;

                        case 2:
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Atenção, esta opção é inválida!");
                            break;
                    }
                    if (option == 1) return 0;
                }

            }
            catch (Exception erro)
            {
                erro.ToString();
                Console.Clear();
                Console.WriteLine("Atenção, dados cadastrais incorretos!");
            }

            return 5;
        }
    }
}
