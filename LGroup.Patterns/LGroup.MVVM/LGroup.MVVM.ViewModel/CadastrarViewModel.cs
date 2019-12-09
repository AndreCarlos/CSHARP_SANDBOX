using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

///para enviar as notificações (sinais) para tela herdamos da interface abaixo
using System.ComponentModel;
using LGroup.MVVM.Model;

///Desde 2005 o que tem de mais top pra desenvolvimento desktop é o wpf (windows
///presentation fundation). Ele veio pra ser o sucessor do WF e acabou virando uma
///alternatia 
///Projeto cinza - windows forms
///Projeto com mais efeitos visuais - WPF
///Ele é um windows forms com suporte avançado pra áudio, vídeo, imagem, animações
///Junção de windows forms + Photoshop + Flash
///O desenho de telas é feito utilizando a linguagem XAML. Derivada do XML pra desenho
///de telas (tipo HTML)


///O padrao de projeto feito exclusivamente para o XAML é o padrao MVVM (não é GoF).
///É um padrao da Microsoft = 2005
///Model - armazenamento de dados
///View - Telas
///ViewModel - a programação da tela (como se fosse um controller)
///Pra cada view, um ViewModel


namespace LGroup.MVVM.ViewModel
{
    /// <summary>
    /// O padrao MVVM é exclusiva para tecnologias baseadas em xaml
    /// WPF, XBAP (WPF PARA WEB), SILVERLIGHT, WINDOWS PHONE (NATIVO)
    /// XNA (XBOX), XAMARIM FOMS (ANDROID, IOS) e 
    /// UWP (Universal Windows Plataform), um código único (windows, web, mobile, tablet, relógio,
    /// IoT e Game)
    /// 
    /// </summary>
    public sealed class CadastrarViewModel : INotifyPropertyChanged
    {
        public CadastrarViewModel()
        {
            ///fizemos as amarrações de propriedades vs metodo
            ClickLimpar = new DispararBotao(Limpar);
            ClickCadastrar = new DispararBotao(Cadastrar);
        }

        private string _nome;
        public string Nome 
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;

                ///sempre que a propriedade for alterada, temos que avisar a tela
                ///que o conteudo da propriedade foi alterado temos que mandar um sinal
                ///
                PropertyChanged(this, new PropertyChangedEventArgs("Nome"));
            } 
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private string _telefone;
        public string  Telefone 
        {
            get
            {
                return _telefone;
            }
            set
            {
                _telefone = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Telefone"));
            }
        }

        private DateTime? _dataNascimento;
        public DateTime? DataNascimento 
        { 
             get
            {
                return _dataNascimento;
            }
            set
            {
                _dataNascimento = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DataNascimento"));
            }
        }


        /// <summary>
        /// Os ICommand serve para pegar os eventos de tela
        /// Evento Click
        /// Commands são açoes, eventos
        /// botao -> clickLimpar -> void Limpar()
        /// </summary>
        public ICommand ClickLimpar  { get; set; }

        public ICommand ClickCadastrar { get; set; }


        /// <summary>
        /// Só da pra fazer binding em propriedades
        /// nao funciona binding em métodos
        /// </summary>
        public void Limpar()
        {
            ///Quando estamos no viewmodel e queremos mandar algo para tela
            ///ou alterar o conteudo dos campos, um simples limpar campos, não basta
            ///só limpar, temos que enviar um sinal (notificação) pra tela
            ///avisando a tela que o conteudo do campo foi alterado
            ///
            Nome = string.Empty;
            Email = string.Empty;
            Telefone = string.Empty;
            DataNascimento = null;
        }

        public void Cadastrar()
        {
            ///Finge que cadastrou
            ///os dados que estao dentro da viewmodel tem que passar para o model
            ///quando vem e vai pra tela é VIEWMODEL
            ///quando vem e vai para tabela é MODEL
            ///
            ///MVVMCROSS, EXCALIBUR - FRAMEWORK PARA MVVM
            var novoAmigo = new AmigoModel();

            novoAmigo.Nome = Nome;
            novoAmigo.Email = Email;
            novoAmigo.Telefone = Telefone;
            novoAmigo.DataNascimento = DataNascimento;

            Limpar();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

///Aqui no XAML temos 3 tecnicas especificas de 
///1 - DATA BINDING (SINCRONIZACAO, Amarraçao de dados)
///sincronizaçao de telas com ViewModels, campos de tels com campos do ViewModel
///2 - COMMAND (Disparar Clicks, Eventos)
///Quando o usuário clicar nos botões, vamos capturar os clicks utilizando COMANDOS == COMMANDS