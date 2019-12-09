using System;
using LGroup.Adapter.Model;

///O padrao adapter (adaptador)
///Gof -> doFactory (nivel 4)
///Proposta desse padrao é integrar DLLs de terceiros dentro de 
///nosso sistema (baixada da net(paga ou gratuita), equipe de arquitetura,
///outra equipe da empresa
///
///alguns exemplos do dia-a-dia
///adaptacao de tipos de tomada (2 pinos, 3 pinos) = precisa de 1 adaptador
///adaptacao de idiomas (1 frances e 1 ingles) = precisa de 1 tradutor
///temos um sistema de emissao de boletos e temos que adpatar outras DLLs
///de emissao de boletos
namespace LGroup.Adapter.Business
{
    /// <summary>
    /// temos alguns sintomas classicos que foram catalogados que falam se o nosso codigo
    /// está bem feito ou nao. Eles se chamam code smells (codigos mal cheirosos)
    /// 1 - NUMEROS MAGICOS (Numeros perdidos no meio do codigo)
    /// 2 - TEXTOS FIXOS NO MEIO DO CODIGO (constante ou dentro do arquivo Resx)
    /// </summary>
    public sealed class BancoBrasilBusiness : Base.IBoletoBusiness
    {
        /// <summary>
        /// identificamos o code smell, fizemos o code refactoring 
        /// apos a melhoria (refatoraçao) ganhamos LEGIBILIDADE
        /// </summary>
        /// <param name="boleto_"></param>
        public void EmitirBoleto(BoletoModel boleto_)
        {
            const Decimal VALOR_MAXIMO_PERMITIDO = 10000;
            const string MENSAGEM_ERRO_VALOR_EXCEDIDO = "Valor Excedido";
            const string ERRO_DATA_EXCEDIDA = "Data Limite Excedida";

           if (boleto_.Valor >= VALOR_MAXIMO_PERMITIDO)
                throw new ApplicationException(MENSAGEM_ERRO_VALOR_EXCEDIDO);

            if (boleto_.DataVencimento > DateTime.Now)
                throw new ApplicationException(ERRO_DATA_EXCEDIDA);
        }
    }
}