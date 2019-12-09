using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LGroup.MVVM.ViewModel
{
    /// <summary>
    /// Criamos uma classe para redirecionar o click dos botoes 
    /// A propriedade chama o método
    /// </summary>
    public sealed class DispararBotao : ICommand
    {
        /// <summary>
        /// Para receber um metodo (endereço de memoria) de um metodo atualizar, cadastrar e deletar
        /// Para uma classe chamar metodos de outra classe sem ter que dar um NEW, criamos um ponteiro de 
        /// memória (DELEGATE), uma referencia para um determindado METODO
        /// O delegate do tipo aciton recebe metodos que nao tenham parametros de entrada
        /// e que sejam VOID
        /// </summary>
        /// 
        private Action _metodo;
        public DispararBotao(Action metodo_)
        {
            _metodo = metodo_;
        }

        /// <summary>
        /// o comando abaixo serve para habilitar ou desabilitar os botoes
        /// enabled ou desabled
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// É aqui que programamos os botoes 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            ///acionamos os metdos que chegraram como parametro de entrada
            ///limpar ou cadastrar ou qualquer outro metodo que seja VOID 
            _metodo();
        }
    }
}
